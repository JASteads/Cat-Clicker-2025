using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameDataSO gameData;
    [SerializeField] ClickSystem clickSystem;
    [SerializeField] BuildingShop buildingShop;
    [SerializeField] Canvas canvas;

    [SerializeField] float autosaveTimer = 0;
    void Start()
    {
        canvas = Instantiate(canvas.gameObject)
            .GetComponent<Canvas>();
        clickSystem = Instantiate(clickSystem.gameObject)
            .GetComponent<ClickSystem>();
        buildingShop = Instantiate(buildingShop.gameObject)
            .GetComponent<BuildingShop>();

        canvas.worldCamera = Camera.main;

        clickSystem.transform.SetParent(canvas.transform, false);
        clickSystem.GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(200, 150);

        buildingShop.transform.SetParent(canvas.transform, false);
        buildingShop.GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(0, 100);
    }

    private void FixedUpdate()
    {
        double bitGain = gameData.bitsPerSecond * Time.deltaTime;

        gameData.currentBits += bitGain;
        gameData.totalBits += bitGain;

        // Autosave
        if (autosaveTimer < 30)
            autosaveTimer += Time.deltaTime;
        else
        {
            autosaveTimer = 0;
            FileManager.SaveGame(gameData);
        }
    }
}
