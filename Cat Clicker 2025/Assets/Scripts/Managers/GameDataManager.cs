using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance; // Singleton behavior

    [SerializeField] GameData gameDataLocal; // Localized version for internal 
    public static GameData gameData;

    // Keep achievements stuff persistent
    [SerializeField] public AchievementsDatabase achievementsDatabase;
    public AchievementsManager achievementsManager;

    void Awake()
    {
        DontDestroyOnLoad(this);

        // Singleton activation
        if (Instance == null || Instance.gameObject == null)
        {
            Instance = null;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Attempt load of game data
        if (gameDataLocal == null)
        {
            Debug.LogWarning("gameDataLocal was null. Creating a new one ..");
            gameDataLocal = new GameData();
        }

        if (!SaveSystem.LoadGame(gameDataLocal))
        {
            Debug.Log("No existing save file. Creating a new one ..");
            SaveSystem.SaveGame(gameDataLocal);
        }
        gameData = gameDataLocal;

        // Initialize achievements system
        achievementsManager = new AchievementsManager();
        achievementsManager.StartSystem(achievementsDatabase);
    }

    void OnDestroy()
    {
        achievementsManager?.StopSystem();
    }

    public static void StartBaseGame()
    {
        SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
    }

    public static void QuitGame()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


    // Hard resets the game's data
    [ContextMenu("Format Save (Hard Reset)")]
    public void HardReset()
    {
        gameDataLocal = new GameData();
        Debug.Log(gameData.ToString());
        gameData = gameDataLocal;
        SaveSystem.SaveGame(gameDataLocal);
        SceneManager.LoadScene("Main Game", LoadSceneMode.Single);
    }

    public static void SaveAndQuitGame()
    {
        SaveSystem.SaveGame(gameData);
        QuitGame();
    }
}
