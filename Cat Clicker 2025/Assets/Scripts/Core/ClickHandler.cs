using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] public GameData gameData;
    [SerializeField] Button bigButton;
    [SerializeField] TextMeshProUGUI bitCount;
    [SerializeField] TextMeshProUGUI bpsRate;

    void Start()
    {
        bigButton = gameObject.GetComponent<Button>();   // Find the button and make it the big button
        bigButton.onClick.AddListener(OnClick);
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
        bitCount.text = "Bits : " +
            $"{Utilities.ToBitNotation(gameData.baseData.currentBits)}";
        bpsRate.text = "Bits per second : " +
            $"{Utilities.ToBitNotation(gameData.baseData.bps)}";
    }

    void OnClick()
    {
        EventBus.GoBigButtonClick();
    }
    
    void HandleClick()
    {
        double gains = gameData.baseData.clickPower;
        gameData.baseData.currentBits += gains;

        EventBus.GoBitsAdded(gains);
    }
}
