using UnityEngine;
using UnityEngine.UI;

public class BaseGame : MonoBehaviour
{
    [SerializeField] BaseGameData baseData;

    // System Elements
    [Header("Systems")]
    [SerializeField] public ClickHandler clickHandler;
    [SerializeField] public SpammablesShop spammablesShop;
    [SerializeField] public UpgradesShop upgradesShop;
    [SerializeField] public FeverSystem feverSystem;
    [SerializeField] public OptionsUI optionsUI;
    public CurrencySystem currencySystem;

    // UI Elements
    [Header("UI Elements")]
    [SerializeField] public Button optionsButton;
    [SerializeField] public Button upgradesShopButton;
    [SerializeField] public Button specialistsScreenButton;
    [SerializeField] public Button specialistsBackButton;

    // Timers
    [Header("Timers")]
    [SerializeField] public float autosaveTimer = 0;
    [SerializeField] public float slowUpdateTimer = 0;
    public const int SLOW_UPDATE_TIME = 1;

    void Awake()
    {
        // Configuring object-specific fields

        currencySystem = new CurrencySystem();
        currencySystem.spammables = baseData.spammables;
        spammablesShop.spammables = baseData.spammables;

        // Assign functionality to buttons
        specialistsScreenButton.onClick.AddListener(
            () => UIScreenManager.Instance.ShowScreen(1));
        specialistsBackButton.onClick.AddListener(
            () => UIScreenManager.Instance.ShowScreen(0));
        upgradesShopButton.onClick.AddListener(
            () => upgradesShop.gameObject.SetActive(
                !upgradesShop.gameObject.activeSelf));
        optionsButton.onClick.AddListener(
            () => EventBus.GoInterfaceFocus(optionsUI.transform, true));
    }

    void OnEnable()
    {
        EventBus.OnGameWin += DoTheCelebration;
    }
    void OnDisable()
    {
        EventBus.OnGameWin -= DoTheCelebration;
    }

    void Update()
    {
        EventBus.GoGameTick();

        // Perform slow updates
        if (slowUpdateTimer < SLOW_UPDATE_TIME)
        {
            slowUpdateTimer += Time.unscaledDeltaTime;
        }
        else
        {
            EventBus.GoSlowUpdate();
            slowUpdateTimer = 0;
        }

        // Autosave
        if (autosaveTimer < 30)
            autosaveTimer += Time.unscaledDeltaTime;
        else
        {
            autosaveTimer = 0;
            Debug.Log($"Showing Game Data ..\n{GameDataManager.gameData.ToString()}");
            SaveSystem.SaveGame(GameDataManager.gameData);
        }
    }

    public void OnDestroy()
    {
        currencySystem?.StopSystem();
    }

    [ContextMenu("Recalculate BPS")]
    void ForceRecalculateBPS()
    {
        currencySystem.RecalculateBPS();
    }

    void DoTheCelebration()
    {
        Debug.Log("Yay!");
        GameDataManager.SaveAndQuitGame();
    }
}
