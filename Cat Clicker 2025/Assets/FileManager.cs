using UnityEngine;

public static class FileManager
{
    const string SAVE_KEY = "Profile A";
    const int SPICY_SAUCE = 052852;

    public static void SaveGame(GameDataSO gameData)
    {
        string raw = JsonUtility.ToJson(gameData),
               result = Encrypt(raw, SPICY_SAUCE);

        PlayerPrefs.SetString(SAVE_KEY, result);
    }

    public static bool LoadGame(GameDataSO gameData)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY)) return false;

        string encryptedSave = PlayerPrefs.GetString(SAVE_KEY),
               decryptedSave = Encrypt(encryptedSave, SPICY_SAUCE);

        JsonUtility.FromJsonOverwrite(decryptedSave, gameData);

        return true;
    }

    static string Encrypt(string data, int key)
    {
        char[] result = data.ToCharArray();
        for (int i = 0; i < result.Length; i++)
            result[i] = (char)(result[i] ^ key);

        return new string(result);
    }
}
