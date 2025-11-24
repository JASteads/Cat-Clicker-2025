using Unity.VisualScripting;
using UnityEditor;
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
        // REPLACE WITH PROPER KEY LOADING LATER. THIS IS JUST TO TEST SO STUFF
        gameData = AssetDatabase.LoadAssetAtPath(
            "Assets/GameDataSave.asset", typeof(GameDataSO)) as GameDataSO;

        if (gameData == null)
        {
            Debug.Log("No save file found. Creating one ...");

            gameData = ScriptableObject.CreateInstance<GameDataSO>();
            clickSystem.gameData = gameData;
            buildingShop.gameData = gameData;

            AssetDatabase.CreateAsset(gameData, "Assets/GameDataSave.asset");
            AssetDatabase.SaveAssets();
        }

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

    void FixedUpdate()
    {
        double bitGain = gameData.bitsPerSecond * Time.deltaTime;

        gameData.currentBits += bitGain;
        gameData.totalBits += bitGain;

        // Autosave
        if (autosaveTimer < 30)
            autosaveTimer += Time.unscaledDeltaTime;
        else
        {
            autosaveTimer = 0;
            FileManager.SaveGame(gameData);
        }
    }
}
