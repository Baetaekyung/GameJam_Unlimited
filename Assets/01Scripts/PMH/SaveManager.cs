using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static readonly string SavePath = Application.persistentDataPath;

    #region Save

    public static void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        string fullPath = Path.Combine(SavePath, fileName);

        File.WriteAllText(fullPath, json);
        Debug.Log($"Data saved to {fullPath}");
    }

    public static void SaveEncrypted<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        string encryptedJson = EncryptionUtility.Encrypt(json);
        string fullPath = Path.Combine(SavePath, fileName);

        File.WriteAllText(fullPath, encryptedJson);
        Debug.Log($"Encrypted data saved to {fullPath}");
    }

    #endregion

    #region Load

    public static T Load<T>(string fileName)
    {
        string fullPath = Path.Combine(SavePath, fileName);

        if (!File.Exists(fullPath))
        {
            Debug.Log($"File not found: {fullPath}");
            return default;
        }

        string json = File.ReadAllText(fullPath);
        return JsonUtility.FromJson<T>(json);
    }

    public static T LoadEncrypted<T>(string fileName)
    {
        string fullPath = Path.Combine(SavePath, fileName);

        if (!File.Exists(fullPath))
        {
            Debug.Log($"File not found: {fullPath}");
            return default;
        }

        string encryptedJson = File.ReadAllText(fullPath);
        string decryptedJson = EncryptionUtility.Decrypt(encryptedJson);

        return JsonUtility.FromJson<T>(decryptedJson);
    }

    #endregion

    public static bool Exist(string fileName)
    {
        string fullPath = Path.Combine(SavePath, fileName);

        return File.Exists(fullPath);
    }

    public static void Delete(string fileName)
    {
        string fullPath = Path.Combine(SavePath, fileName);

        if (Exist(fileName))
        {
            File.Delete(fullPath);
            Debug.Log($"File {fullPath} deleted");
        }
        else
        {
            Debug.LogWarning($"File {fullPath} does not exist");
        }
    }
}