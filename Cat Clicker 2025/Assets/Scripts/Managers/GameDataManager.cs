using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance; // Singleton behavior

    // Data Elements
    [Header("Data")]
    [SerializeField] GameData gameDataLocal; // Localized version for internal control
    [SerializeField] public List<SpammableData> spammables;

    // System Elements
    [Header("Systems")]
    [SerializeField] ClickHandler clickHandler;
    [SerializeField] SpammablesShop spammablesShop;
    [SerializeField] UpgradesShop upgradesShop;
    [SerializeField] FeverSystem feverSystem;
    [SerializeField] AchievementsUI achievementsUI;
    CurrencySystem currencySystem;

    // UI Elements
    [Header("UI Elements")]
    [SerializeField] Button specialistsScreenButton;
    [SerializeField] Button specialistsBackButton;
    [SerializeField] Button upgradesShopButton;

    // Misc Elements
    [Header("Miscellaneous")]
    [SerializeField] float autosaveTimer = 0;

    public static GameData gameData;
    void Awake()
    {
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

        // Configuring object-specific fields
        currencySystem = new CurrencySystem();
        currencySystem.spammables = spammables;
        spammablesShop.spammables = spammables;
        specialistsScreenButton.onClick.AddListener(
            () => UIScreenManager.Instance.ShowScreen(1));
        specialistsBackButton.onClick.AddListener(
            () => UIScreenManager.Instance.ShowScreen(0));
        upgradesShopButton.onClick.AddListener(
            () => upgradesShop.gameObject.SetActive(
                !upgradesShop.gameObject.activeSelf));
        upgradesShop.gameObject.SetActive(false);
    }


    void OnDestroy()
    {
        currencySystem?.StopSystem();
    }

    void Update()
    {
        EventBus.GoGameTick();

        // Autosave
        if (autosaveTimer < 30)
            autosaveTimer += Time.unscaledDeltaTime;
        else
        {
            autosaveTimer = 0;
            Debug.Log($"Showing Game Data ..\n{gameData.ToString()}");
            SaveSystem.SaveGame(gameDataLocal);
        }
    }

    [ContextMenu("Recalculate BPS")]
    void ForceRecalculateBPS()
    {
        currencySystem.RecalculateBPS();
    }
}
