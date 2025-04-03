using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.RemoteConfig;
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
    private static byte[] _encryptionKey;  // Derived from User ID

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
            false,
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

                    _userId = newUser.UserId; // Google Play User ID
                    _encryptionKey = await GenerateEncryptionKey(_userId);

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

    /*public static async Task<bool> AuthenticateFirebase()
    {
        if (_auth == null)
        {
            Debug.LogError("Firebase Auth not initialized!");
            return false;
        }

        try
        {
            var signInTask = _auth.SignInWithEmailAndPasswordAsync("isikdonger04@gmail.com", "ISIKdonger$04");
            var timeoutTask = Task.Delay(5000); // 5 second timeout

            if (await Task.WhenAny(signInTask, timeoutTask) == timeoutTask)
            {
                Debug.LogError("Login timed out");
                return false;
            }

            _userId = _auth.CurrentUser.UserId;
            _encryptionKey = await GenerateEncryptionKey(_userId);

            Debug.Log($"Dev login successful! User ID: {_userId}");
            return true;
        }
        catch (FirebaseException fbEx)
        {
            Debug.LogError($"Firebase error: {fbEx.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Login failed: {ex.Message}");
            return false;
        }
    }*/

    private static async Task<byte[]> GetSaltAsync()
    {
        // 1. Initialize Firebase if needed
        if (FirebaseRemoteConfig.DefaultInstance == null)
        {
            await FirebaseApp.CheckAndFixDependenciesAsync();
        }

        // 2. Fetch Remote Config
        await FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync();

        // 3. Get salt value
        string saltString = FirebaseRemoteConfig.DefaultInstance
            .GetValue("encryption_salt").StringValue;

        return Encoding.UTF8.GetBytes(saltString);
    }

    /// <summary>
    /// Generates an encryption key using PBKDF2 from the user ID.
    /// </summary>
    private static async Task<byte[]> GenerateEncryptionKey(string userId)
    {
        byte[] salt = await GetSaltAsync();
        using (var deriveBytes = new Rfc2898DeriveBytes(
            password: userId,
            salt: salt,
            iterations: 600000, // Increased for security
            hashAlgorithm: HashAlgorithmName.SHA256))
        {
            return deriveBytes.GetBytes(32); // 32 bytes = 256-bit key
        }
    }

    /// <summary>
    /// Gets the encryption key (generated during authentication).
    /// </summary>
    public static byte[] GetEncryptionKey()
    {
        if (_encryptionKey == null || _encryptionKey.Length != 32)
        {
            Debug.LogError("Encryption key invalid. Re-authenticate.");
            return null;
        }
        return _encryptionKey;
    }

    /// <summary>
    /// Synchronizes local progress with Firestore, handling first-time users and merge conflicts.
    /// Returns true if sync succeeded.
    /// </summary>
    public static async Task<bool> SyncWithCloud()
    {
        // Validate dependencies
        if (_firestore == null || string.IsNullOrEmpty(_userId))
        {
            Debug.LogWarning("Firestore not initialized or user ID missing.");
            return false;
        }

        try
        {
            DocumentReference docRef = _firestore.Collection("gameProgress").Document(_userId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Load local progress (always exists, even if default)
            GameProgress localProgress = LocalBackupManager.LoadProgress() ?? new GameProgress();

            // CASE 1: First-time user (no cloud save)
            if (!snapshot.Exists)
            {
                Debug.Log("First-time user detected. Uploading initial progress.");
                await SaveProgressToCloud(JsonUtility.ToJson(localProgress));
                return true;
            }

            // CASE 2: Existing user (merge cloud + local)
            string encryptedData = snapshot.GetValue<string>("data");
            string jsonData = SecureDataManager.Decrypt(encryptedData, _encryptionKey);
            GameProgress cloudProgress = JsonUtility.FromJson<GameProgress>(jsonData);

            // Merge strategy (customize per game needs)
            GameProgress mergedProgress = new GameProgress()
            {
                // Competitive stats (take maximum)
                highScore = Mathf.Max(localProgress.highScore, cloudProgress.highScore),

                // Counters (take maximum to prevent inflation)
                breakCount = Mathf.Max(localProgress.breakCount, cloudProgress.breakCount),
                spikeDeathCount = Mathf.Max(localProgress.spikeDeathCount, cloudProgress.spikeDeathCount),
                coinSpent = Mathf.Max(localProgress.coinSpent, cloudProgress.coinSpent),
                winCount = Mathf.Max(localProgress.winCount, cloudProgress.winCount),
                lossCount = Mathf.Max(localProgress.lossCount, cloudProgress.lossCount),

                // TEMPORARY: Prioritize local characters
                usedCharacters = new List<string>(localProgress.usedCharacters)
            };

            // Save merged data everywhere
            LocalBackupManager.SaveProgress(mergedProgress);
            await SaveProgressToCloud(JsonUtility.ToJson(mergedProgress));

            Debug.Log("Cloud sync completed successfully.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Sync failed: {ex.Message}\n{ex.StackTrace}");
            return false;
        }
    }

    /// <summary>
    /// Encrypts and saves progress to Firestore.
    /// </summary>
    private static async Task SaveProgressToCloud(string jsonData)
    {
        if (_firestore == null || string.IsNullOrEmpty(_userId))
            throw new ArgumentNullException("Firestore not initialized");

        try
        {
            string encryptedData = SecureDataManager.Encrypt(jsonData, _encryptionKey);
            await _firestore.Collection("gameProgress")
                .Document(_userId)
                .SetAsync(new
                {
                    data = encryptedData,
                    lastUpdated = FieldValue.ServerTimestamp // Audit field
                }, SetOptions.MergeFields("data", "lastUpdated")); // Preserves other fields if they exist
        }
        catch (Exception ex)
        {
            Debug.LogError($"Cloud save failed: {ex.Message}");
            throw; // Re-throw for callers to handle
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
