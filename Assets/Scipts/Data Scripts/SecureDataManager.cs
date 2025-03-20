using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SecureDataManager
{
    /// <summary>
    /// Saves encrypted data to a file and stores its hash for integrity checking.
    /// </summary>
    public static void SaveEncryptedData(string filename, string data, string encryptionKey)
    {
        if (string.IsNullOrEmpty(encryptionKey))
        {
            Debug.LogWarning("Encryption key is null or empty!");
            return;
        }

        string encryptedData = Encrypt(data, encryptionKey);
        string hash = ComputeSHA256(encryptedData); // Compute SHA-256 hash

        string filePath = Application.persistentDataPath + "/" + filename;
        File.WriteAllText(filePath, encryptedData + "\n" + hash);
    }

    /// <summary>
    /// Loads and decrypts data, verifying integrity with SHA-256.
    /// </summary>
    public static string LoadEncryptedData(string filename, string encryptionKey)
    {
        if (string.IsNullOrEmpty(encryptionKey))
        {
            Debug.LogWarning("Encryption key is null or empty!");
            return null;
        }

        string filePath = Application.persistentDataPath + "/" + filename;
        if (!File.Exists(filePath)) return null;

        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length < 2) return null; // Invalid file format

        string encryptedData = lines[0];
        string storedHash = lines[1];

        // Verify integrity
        string computedHash = ComputeSHA256(encryptedData);
        if (storedHash != computedHash)
        {
            Debug.LogWarning("Data integrity check failed! The file might be corrupted.");
            return null;
        }

        return Decrypt(encryptedData, encryptionKey);
    }

    /// <summary>
    /// Encrypts text using AES.
    /// </summary>
    public static string Encrypt(string plainText, string encryptionKey)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = new byte[16]; // Using a zero IV (consider a random IV per session for more security)

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }

    /// <summary>
    /// Decrypts text using AES.
    /// </summary>
    public static string Decrypt(string encryptedText, string encryptionKey)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = new byte[16];

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Computes the SHA-256 hash of a given input string.
    /// </summary>
    private static string ComputeSHA256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
