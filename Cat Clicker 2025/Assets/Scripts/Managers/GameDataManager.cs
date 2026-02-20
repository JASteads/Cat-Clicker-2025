using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] ClickHandler clickHandler;
    [SerializeField] CurrencySystem currencySystem;
    [SerializeField] BuildingShop buildingShop;
    [SerializeField] Canvas canvas;
    [SerializeField] float autosaveTimer = 0;

    void Start()
    {
        // Configuring object-specific fields
        currencySystem = new CurrencySystem(gameData);
        clickHandler.gameData = gameData;
    }

    void Awake()
    {
        if (!SaveSystem.LoadGame(gameData))
        {
            gameData = new GameData();
            SaveSystem.SaveGame(gameData);
        }
    }

    void OnDestroy()
    {
        currencySystem.StopSystem();
    }

    void FixedUpdate()
    {
        EventBus.GoGameTick();

        // Autosave
        if (autosaveTimer < 30)
            autosaveTimer += Time.unscaledDeltaTime;
        else
        {
            autosaveTimer = 0;
            Debug.Log($"Showing Game Data ..\n{gameData.ToString()}");
            SaveSystem.SaveGame(gameData);
        }
    }
}
