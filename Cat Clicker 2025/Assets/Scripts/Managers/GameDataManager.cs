using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] GameData gameDataLocal; // Localized version for internal control
    [SerializeField] ClickHandler clickHandler;
    [SerializeField] CurrencySystem currencySystem;
    [SerializeField] SpammablesShop spammablesShop;
    [SerializeField] float autosaveTimer = 0;

    public static GameData gameData;
    void Awake()
    {
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
            SaveSystem.SaveGame(gameDataLocal);
        }
    }
}
