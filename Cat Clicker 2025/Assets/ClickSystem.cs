using UnityEngine;
using UnityEngine.UI;

public class ClickSystem : MonoBehaviour
{
    public GameDataSO gameData;
    [SerializeField] Button bigButton;
    [SerializeField] Text bitCount;
    [SerializeField] Text bpsRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bigButton = gameObject.GetComponent<Button>();      // Find the button and make it the big button
        bigButton.onClick.AddListener(gameData.OnClick);    // Make the button have click mechanics

        bpsRate = transform.GetChild(1).GetComponent<Text>();           // Ugly way of getting the BPS text
        bitCount = bpsRate.transform.GetChild(0).GetComponent<Text>();  // Ugly way of getting the Bits text
    }

    void Update()
    {
        bitCount.text = $"Bits : {gameData.currentBits:0}";
        bpsRate.text = $"Bits per second : {gameData.bitsPerSecond:0.##}";
    }
}
