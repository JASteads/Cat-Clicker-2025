using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] List<UpgradeButton> upgradeButtons;

    void Start()
    {
        ConfigButtons();
    }

    void OnEnable()
    {
        // Subscribe events here
    }

    void OnDisable()
    {
        // Unsubscribe events here
    }

    void LateUpdate()
    {
        UpdateShop();
    }

    void UpdateShop()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            UpdateButton(i);
        }
    }

    void UpdateButton(int id)
    {
        UpgradeButton b = upgradeButtons[id];
        bool isPurchased = GameDataManager.gameData.upgradesPool[id];

        b.button.interactable = !isPurchased && CanAfford(b);
        b.uName.text = isPurchased ? 
            b.data.upgradeName + " (Bought)" : b.data.upgradeName;
    }

    void ConfigButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            int num = i;
            UpgradeButton b = upgradeButtons[num];

            b.button.onClick.AddListener(() => TryBuy(num));
            b.uPrice.text = Utilities.ToBitNotation(b.data.price);
        }
    }

    bool TryBuy(int id)
    {
        Debug.Log($"Boop!\nID: {id}");

        UpgradeButton b = upgradeButtons[id];

        // Continue only when purchase is validated
        if (GameDataManager.gameData.upgradesPool[id] || !CanAfford(b))
            return false;

        // Simple upgrade mechanism for testing
        GameData data = GameDataManager.gameData;

        data.spammablesData[id].multiplier *= 2;
        data.baseData.currentBits -= b.data.price;
        data.upgradesPool[id] = true;

        EventBus.GoBitsAdded(-b.data.price);
        EventBus.GoSpamBPSChange();
        EventBus.GoUpgradePurchase();

        return true;
    }

    bool CanAfford(UpgradeButton b)
    {
        double currentBits = GameDataManager.gameData.baseData.currentBits;

        return currentBits >= b.data.price;
    }
}

[Serializable]
public struct UpgradeButton
{
    [SerializeField] public Button button;
    [SerializeField] public UpgradeData data;
    [SerializeField] public TextMeshProUGUI uName,
                                            uDesc,
                                            uPrice;
}