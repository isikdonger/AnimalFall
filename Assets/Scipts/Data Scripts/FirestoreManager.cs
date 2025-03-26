using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class FirestoreManager
{
    private static FirebaseFirestore _firestore;
    private static FirebaseAuth _auth;
    private static string _userId;  // Google Play User ID
    private static string _encryptionKey;  // Derived from User ID

    /// <summary>
    /// Initializes Firebase & Google Play authentication.
    /// </summary>
    public static async Task Initialize()
    {
        DependencyStatus status = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (status == DependencyStatus.Available)
        {
            _firestore = FirebaseFirestore.DefaultInstance;
            _auth = FirebaseAuth.DefaultInstance;
            Debug.Log("Firebase initialized successfully.");
        }
        else
        {
            Debug.LogError("Firebase dependencies not available: " + status);
        }
    }

    /// <summary>
    /// Authenticate with Firebase using Google Play Games.
    /// </summary>
    public static Task<FirebaseUser> AuthenticateFirebase()
    {
        var tcs = new TaskCompletionSource<FirebaseUser>();

        // Request auth code using callback
        PlayGamesPlatform.Instance.RequestServerSideAccess(
            /* forceRefreshToken= */ false,
            async authCode =>
            {
                if (string.IsNullOrEmpty(authCode))
                {
                    Debug.LogError("Failed to retrieve auth code.");
                    tcs.SetException(new Exception("Failed to get auth code."));
                    return;
                }

                Debug.Log("Auth Code Received: " + authCode);

                try
                {
                    // Convert auth code to Firebase credential
                    Credential credential = PlayGamesAuthProvider.GetCredential(authCode);

                    // Sign in to Firebase asynchronously
                    FirebaseUser newUser = await _auth.SignInWithCredentialAsync(credential);
                    Debug.Log($"Firebase Sign-In Successful! User: {newUser.DisplayName}");

                    _encryptionKey = GenerateEncryptionKey(newUser.UserId);

                    tcs.SetResult(newUser);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Firebase Sign-In Failed: {e.Message}");
                    tcs.SetException(e);
                }
            }
        );

        return tcs.Task;
    }

    /// <summary>
    /// Generates an encryption key using PBKDF2 from the user ID.
    /// </summary>
    private static string GenerateEncryptionKey(string userId)
    {
        byte[] salt = Encoding.UTF8.GetBytes(userId); // Use user ID as salt
        using (var deriveBytes = new Rfc2898DeriveBytes(userId, salt, 100000)) // Increase iteration count
        {
            return Convert.ToBase64String(deriveBytes.GetBytes(32)); // 256-bit key
        }
    }

    /// <summary>
    /// Gets the encryption key (generated during authentication).
    /// </summary>
    public static string GetEncryptionKey()
    {
        if (string.IsNullOrEmpty(_encryptionKey))
        {
            Debug.LogError("Encryption key not generated. Ensure authentication is complete.");
            return null;
        }
        return _encryptionKey;
    }

    /// <summary>
    /// Synchronizes local progress with Firestore.
    /// </summary>
    public static async Task SyncWithCloud()
    {
        if (_firestore == null || string.IsNullOrEmpty(_userId))
        {
            Debug.LogWarning("Firestore not initialized or user ID missing.");
            return;
        }

        try
        {
            DocumentReference docRef = _firestore.Collection("game_progress").Document(_userId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                string encryptedData = snapshot.GetValue<string>("data");
                string jsonData = SecureDataManager.Decrypt(encryptedData, _encryptionKey);
                GameProgress cloudProgress = JsonUtility.FromJson<GameProgress>(jsonData);

                Debug.Log("Loaded progress from Firestore.");

                GameProgress localProgress = LocalBackupManager.LoadProgress();
                GameProgress mergedProgress = new GameProgress
                {
                    highScore = Mathf.Max(localProgress.highScore, cloudProgress.highScore),
                    coinSpent = localProgress.coinSpent + cloudProgress.coinSpent
                };

                LocalBackupManager.SaveProgress(mergedProgress);  // Save merged data locally
                await SaveProgressToCloud(JsonUtility.ToJson(mergedProgress));  // Save merged data to Firestore
            }
            else
            {
                Debug.Log("No cloud save found. Uploading local progress.");
                string localJson = JsonUtility.ToJson(LocalBackupManager.LoadProgress());
                if (!string.IsNullOrEmpty(localJson))
                {
                    await SaveProgressToCloud(localJson);  // Upload local progress if it exists
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error syncing with Firestore: " + ex.Message);
        }
    }

    /// <summary>
    /// Saves progress to Firestore.
    /// </summary>
    private static async Task SaveProgressToCloud(string jsonData)
    {
        if (_firestore == null || string.IsNullOrEmpty(_userId)) return;

        try
        {
            string encryptedData = SecureDataManager.Encrypt(jsonData, _encryptionKey);
            DocumentReference docRef = _firestore.Collection("game_progress").Document(_userId);
            await docRef.SetAsync(new { data = encryptedData });

            Debug.Log("Game progress synced with Firestore.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving progress to Firestore: " + ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific field value from any document in any Firestore collection.
    /// </summary>
    public static async Task<T> GetFieldValue<T>(string collectionId, string documentId, string fieldName)
    {
        if (_firestore == null)
        {
            Debug.LogWarning("Firestore not initialized.");
            return default;
        }

        try
        {
            DocumentReference docRef = _firestore.Collection(collectionId).Document(documentId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists && snapshot.ContainsField(fieldName))
            {
                return snapshot.GetValue<T>(fieldName);
            }
            else
            {
                Debug.LogWarning($"Field '{fieldName}' does not exist in document '{documentId}' in collection '{collectionId}'.");
                return default;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving field '{fieldName}' from Firestore: {ex.Message}");
            return default;
        }
    }
}
