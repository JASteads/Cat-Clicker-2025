using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialistShop : MonoBehaviour
{
    [SerializeField] int activeSpecialistID = -1; // Default value to prevent data update OnEnable before the interface is ready

    [SerializeField] public List<SpecialistData> specialists;

    [Header("Buttons")]
    [SerializeField] public Button levelUpButton;

    [Header("Text Fields")]
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI specialistName;
    [SerializeField] public TextMeshProUGUI specialistTitle;
    [SerializeField] public TextMeshProUGUI specialistDescription;
    [SerializeField] public TextMeshProUGUI levelUpCostText;

    void Start()
    {
        if (specialists == null)
        {
            return;
        }

        ShowSpecialist(0);
        UpdateLevelUpButton();
        levelUpButton.onClick.AddListener(() => TryLevelUp());
    }
    void OnEnable()
    {
        EventBus.OnBitsAdded += OnBitsUpdate;
        if (activeSpecialistID != -1)
        {
            UpdateSpecialistNumbers();
        }
    }

    void OnDisable()
    {
        EventBus.OnBitsAdded -= OnBitsUpdate;
    }

    void OnBitsUpdate(double amount)
    {
        UpdateLevelUpButton();
    }

    void UpdateLevelUpButton()
    {
        bool canAfford = CanAfford(activeSpecialistID);
        bool isNotMaxLevel = GameDataManager.gameData
             .specialistData[activeSpecialistID].level < 
             specialists[activeSpecialistID].maxLevel;

        levelUpButton.interactable = canAfford && isNotMaxLevel;
    }

    void ShowSpecialist(int id)
    {
        SpecialistData s = specialists[id];
        activeSpecialistID = id; // This sets which specialist is currently active

        // Update texts
        specialistName.text = s.specialistName;
        specialistTitle.text = $"| {s.specialistTitle}";
        specialistDescription.text = s.specialistDescription;

        UpdateSpecialistNumbers();
    }

    void UpdateSpecialistNumbers()
    {
        SpecialistData s = specialists[activeSpecialistID];
        int level = GameDataManager.gameData
            .specialistData[activeSpecialistID].level;
        string price = Utilities.ToBitNotation(level == 0 ? 
            s.basePrice : s.GetPrice(level));

        levelText.text = $"Lv. {level}";
        levelUpCostText.text = level < s.maxLevel ? 
            $"<b>Price: </b>\n{price}" : "\n<b>MAXED OUT!!</b>";
    }

    void TryLevelUp()
    {
        if (!CanAfford(activeSpecialistID)) return; // Make sure level up is valid

        GameData data = GameDataManager.gameData;
        SpecialistData s = specialists[activeSpecialistID];
        int level = data.specialistData[activeSpecialistID].level;

        if (level >= s.maxLevel) return; // Do not attempt to level past max
        bool isLevelZero = level == 0;

        double price = isLevelZero ? s.basePrice : s.GetPrice(level);

        // Perform level up
        if (isLevelZero)
        {
            Debug.Log("Celo has been purchased");
            data.specialistData[activeSpecialistID].isOwned = true; // Mark that the specialist is owned on first buy
        }
        ++data.specialistData[activeSpecialistID].level;
        ++level;
        data.specialistData[activeSpecialistID].bpsMulti *= s.bpsMultiPerLevel;

        data.baseData.currentBits -= price; // Deduct level up cost
        EventBus.GoBitsAdded(-price);
        EventBus.GoSpamBPSChange();

        // Signal a milestone every 5 levels
        if (data.specialistData[activeSpecialistID].level % 5 == 0)
        {
            EventBus.GoSpecialistMilestone(activeSpecialistID, level);
        }

        UpdateSpecialistNumbers();
    }

    bool CanAfford(int id)
    {
        // Don't check if invalid id
        if (specialists.Count < id) return false;

        GameData gameData = GameDataManager.gameData;
        SpecialistData s = specialists[id];
        bool isLevelZero = gameData.specialistData[id].level == 0;
        
        double price = isLevelZero? s.basePrice : s.GetPrice(
            GameDataManager.gameData.specialistData[id].level);

        return gameData.baseData.currentBits >= price;
    }
}
