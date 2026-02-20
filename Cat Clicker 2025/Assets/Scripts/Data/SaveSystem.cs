using System.IO;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    readonly static string savesFolder =
        Application.persistentDataPath + "/Saves/";
    const string SAVE_KEY = "ProfileA"; // Test save key
    const int SPICY_SAUCE = 052852;
    const bool ENCRYPT_DATA = false; // SET THIS TO TRUE ON RELEASE
    const bool PREFS_METHOD = false;
    public static string GetFilePath() => savesFolder + SAVE_KEY + ".nimmy";

    public static void SaveGame(GameData gameData)
    {
        try
        {
            // Package save data, optionally encrypt -- make JSON readable when encrypt is off
            string raw = JsonUtility.ToJson(gameData, !ENCRYPT_DATA);
            string result = ENCRYPT_DATA ? Obfuscate(raw, SPICY_SAUCE) : raw;

            // Save the resulting data
            if (PREFS_METHOD)
            {
                // PlayerPrefs method -- Safer but difficult to track
                PlayerPrefs.SetString(SAVE_KEY, result);
            }
            else
            {
                // Direct path method

                // Adds save folder to directory if not yet present
                Directory.CreateDirectory(savesFolder);

                byte[] bytes = Encoding.UTF8.GetBytes(result);
                File.WriteAllBytes(GetFilePath(), bytes);
            }

            Debug.Log($"{SAVE_KEY} saved.");
        }
        catch (System.Exception e)
        {
            Debug.Log("Something went wrong with the file save .." +
                $"\n{e.Message}\n{e.StackTrace}");
        }
    }

    public static bool LoadGame(GameData gameData)
    {
        try
        {
            string rawLoad;

            if (PREFS_METHOD)
            {
                // PlayerPrefs method -- Safer but difficult to track
                if (!PlayerPrefs.HasKey(SAVE_KEY)) return false;
                rawLoad = PlayerPrefs.GetString(SAVE_KEY);
            }
            else
            {
                // Direct path method
                if (!File.Exists(GetFilePath())) return false;

                byte[] bytes = File.ReadAllBytes(GetFilePath());
                rawLoad = Encoding.UTF8.GetString(bytes);
            }

            string result = ENCRYPT_DATA ?
                Obfuscate(rawLoad, SPICY_SAUCE) : rawLoad;
            JsonUtility.FromJsonOverwrite(result, gameData);

            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Something went wrong with the file load .." +
                $"\n{e.Message}\n{e.StackTrace}");
            return false;
        }
    }

    // Ambiguous name as a small layer of secrecy
    static string Obfuscate(string data, int key)
    {
        const int CHAR_LEN = 256; // Cleaner XOR encoding
        char[] result = data.ToCharArray();
        for (int i = 0; i < result.Length; i++)
            result[i] = (char)(result[i] ^ (key % CHAR_LEN));

        return new string(result);
    }
}
