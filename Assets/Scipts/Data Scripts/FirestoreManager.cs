using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public static class FirestoreManager
{
    private static FirebaseFirestore _firestore;
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
            Debug.Log("Firebase initialized successfully.");
        }
        else
        {
            Debug.LogError("Firebase dependencies not available: " + status);
        }
    }

    /// <summary>
    /// Authenticate with Firebase using Google Play Games ID token.
    /// </summary>
    public static async void AuthenticateWithFirebase(string idToken)
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        try
        {
            await auth.SignInWithCredentialAsync(credential);
            _userId = auth.CurrentUser.UserId;
            _encryptionKey = GenerateEncryptionKey(_userId);  // Generate the encryption key
            Debug.Log("Firebase authentication successful. User ID: " + _userId);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Firebase authentication failed: " + ex.Message);
        }
    }

    /// <summary>
    /// Generates an encryption key using PBKDF2 from the user ID.
    /// </summary>
    private static string GenerateEncryptionKey(string userId)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(userId, 16, 10000))
        {
            return Convert.ToBase64String(deriveBytes.GetBytes(32));
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

        DocumentReference docRef = _firestore.Collection("game_progress").Document(_userId);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            // Load progress from Firestore
            string encryptedData = snapshot.GetValue<string>("data");
            string jsonData = SecureDataManager.Decrypt(encryptedData, _encryptionKey);
            GameProgress cloudProgress = JsonUtility.FromJson<GameProgress>(jsonData);

            Debug.Log("Loaded progress from Firestore.");

            // Merge cloud and local progress (higher high score, total coins sum)
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

    /// <summary>
    /// Saves progress to Firestore.
    /// </summary>
    private static async Task SaveProgressToCloud(string jsonData)
    {
        if (_firestore == null || string.IsNullOrEmpty(_userId)) return;

        string encryptedData = SecureDataManager.Encrypt(jsonData, _encryptionKey);
        DocumentReference docRef = _firestore.Collection("game_progress").Document(_userId);
        await docRef.SetAsync(new { data = encryptedData });

        Debug.Log("Game progress synced with Firestore.");
    }

    /// <summary>
    /// Retrieves a specific field value from any document in any Firestore collection.
    /// </summary>
    /// <param name="collectionId">The Firestore collection name.</param>
    /// <param name="documentId">The Firestore document ID.</param>
    /// <param name="fieldName">The name of the field to retrieve.</param>
    /// <returns>The value of the field, or default(T) if the field does not exist.</returns>
    public static async Task<T> GetFieldValue<T>(string collectionId, string documentId, string fieldName)
    {
        if (_firestore == null)
        {
            Debug.LogWarning("Firestore not initialized.");
            return default;
        }

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
}
