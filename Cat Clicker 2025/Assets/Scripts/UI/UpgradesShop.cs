using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] List<UpgradeData> upgradeData; // The data to populate the shop with
    [SerializeField] public List<UpgradeButton> upgradeButtons; // Reflects which upgrades to show in the shop

    [Header("Configuration")]
    [SerializeField] GameObject buttonTemplate;
    [SerializeField] Transform shopBody;

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
        foreach (var b in upgradeButtons)
        {
            UpdateButton(b);
        }
    }

    void UpdateButton(UpgradeButton b)
    {
        b.button.interactable = CanAfford(b);
    }

    void AddButton(UpgradeData data)
    {
        GameObject newButtonObj = Instantiate(buttonTemplate, shopBody, false);
        Transform nameObject = newButtonObj.transform.Find("Name");

        newButtonObj.name = $"Upgrade : {data.upgradeName}";

        UpgradeButton newButton = new UpgradeButton
        {
            button = newButtonObj.GetComponent<Button>(),
            data = data,
            uName = nameObject.GetComponent<TextMeshProUGUI>(),
            uDesc = nameObject.Find("Description").GetComponent<TextMeshProUGUI>(),
            uPrice = newButtonObj.transform.Find("Price").GetComponent<TextMeshProUGUI>(),
        };

        newButton.button.onClick.AddListener(() => TryBuy(newButton));
        newButton.uName.text = data.upgradeName;
        newButton.uDesc.text = data.desc;
        newButton.uPrice.text = 
             Utilities.ToBitNotation(newButton.data.GetPrice());

        upgradeButtons.Add(newButton);
    }

    void RemoveButton(UpgradeButton b)
    {
        upgradeButtons.Remove(b);
        Destroy(b.button.gameObject);
    }

    void ConfigButtons()
    {
        if (!ValidateButtonTemplate()) return;

        // Add button functionality
        foreach (var data in upgradeData)
        {
            if (!IsPurchased(data.upgradeName))
            {
                AddButton(data);
            }
        }
    }

    bool TryBuy(UpgradeButton b)
    {
        string upgradeName = b.data.upgradeName;

        Debug.Log($"Boop!\nUpgrade Name: {upgradeName}");

        double price = b.data.GetPrice();

        // Continue only when purchase is validated
        if (IsPurchased(upgradeName) || !CanAfford(b))
            return false;

        GameData data = GameDataManager.gameData;
        data.upgradesPool.Add(upgradeName);
        data.baseData.currentBits -= price;
        b.data.effect.Apply();

        EventBus.GoBitsAdded(-price);
        EventBus.GoUpgradePurchase();

        RemoveButton(b);

        return true;
    }

    bool CanAfford(UpgradeButton b)
    {
        double currentBits = GameDataManager.gameData.baseData.currentBits;

        return currentBits >= b.data.GetPrice();
    }

    bool IsPurchased(string upgradeName)
    {
        return GameDataManager.gameData.upgradesPool.Contains(upgradeName);
    }

    bool ValidateButtonTemplate()
    {
        // Test template and Button object
        if (buttonTemplate == null)
        {
            Debug.LogError("Upgrade shop does not have an assigned button template");
            return false;
        }
        else if (!buttonTemplate.TryGetComponent<Button>(out _))
        {
            Debug.LogError("Upgrade shop has a button template," +
                "but the template does not have a Button component");
            return false;
        }

        // Test Name object
        Transform nameObject = buttonTemplate.transform.Find("Name");
        if (nameObject == null)
        {
            Debug.LogError("Upgrade shop button template has a button object," +
                "but it does not have a Name object");
            return false;
        }
        else if (!nameObject.TryGetComponent<TextMeshProUGUI>(out _))
        {
            Debug.LogError("Upgrade shop button template has a Name object," +
                " but it does not have a TextMeshProUGUI component");
            return false;
        }

        // Test Desc object
        Transform descObject = nameObject.Find("Description");
        if (descObject == null)
        {
            Debug.LogError("Upgrade shop button template has a Name object," +
                " but the Name does not have a Description child object");
            return false;
        }
        else if (!descObject.TryGetComponent<TextMeshProUGUI>(out _))
        {
            Debug.LogError("Upgrade shop button template has a proper Desc" +
                " object, but it does not have a TextMeshProUGUI component");
            return false; 
        }

        // Test Price object
        Transform priceObject = buttonTemplate.transform.Find("Price");
        if (priceObject == null)
        {
            Debug.LogError("Upgrade shop button template does not " +
                "have a Price object");
            return false;
        }
        else if (!priceObject.TryGetComponent<TextMeshProUGUI>(out _))
        {
            Debug.LogError("Upgrade shop button template has a Price object," +
                " but it does not have a TextMeshProUGUI component");
            return false; 
        }

        return true;
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