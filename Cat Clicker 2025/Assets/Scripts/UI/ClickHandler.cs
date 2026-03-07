using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] Button bigButton;
    [SerializeField] TextMeshProUGUI bitCount;
    [SerializeField] TextMeshProUGUI bpsRate;

    void Start()
    {
        bigButton = gameObject.GetComponent<Button>();   // Find the button and make it the big button
        bigButton.onClick.AddListener(OnClick);
    }

    public void StartSystem(GameData data)
    {
        GameDataManager.gameData = data;
    }

    void OnEnable()
    {
        EventBus.OnBigButtonClick += HandleClick;
    }

    void OnDisable()
    {
        EventBus.OnBigButtonClick -= HandleClick;
    }

    void LateUpdate()
    {
        GameData gameData = GameDataManager.gameData;

        bitCount.text = "Bits : " +
            $"{Utilities.ToBitNotation(gameData.baseData.currentBits)}";
        bpsRate.text = "Bits per second : " +
            $"{Utilities.ToBitNotation(gameData.GetTotalBPS())}";
    }

    void OnClick()
    {
        EventBus.GoBigButtonClick();
    }
    
    void HandleClick()
    {
        GameData data = GameDataManager.gameData;

        double gains = 
            data.baseData.clickPower * data.baseData.clickPowerMulti
            * (1 + data.baseData.bpsToClickPowerMulti);

        GameDataManager.gameData.baseData.currentBits += gains;
        EventBus.GoBitsAdded(gains);
    }
}
