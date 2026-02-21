using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpammablesShop : MonoBehaviour
{
    [SerializeField] public SpammableButton[] spammableButtons;
    [SerializeField] public List<SpammableData> spammables;
    [SerializeField] public int activeBuyAmount;

    void Start()
    {
        // Ensure game has data loaded in
        if (GameDataManager.gameData == null) return; // gameData.baseData is a struct and thus cannot be null

        activeBuyAmount = 1;
        ConfigButtons();
        UpdateShop(1);
    }

    void OnEnable()
    {
        EventBus.OnBitsAdded += UpdateShop;
    }

    void OnDisable()
    {
        EventBus.OnBitsAdded -= UpdateShop;
    }

    void ConfigButtons()
    {

        int loopLen = Mathf.Min(spammableButtons.Length, spammables.Count);

        for (int i = 0; i < loopLen; i++)
        {
            int num = i;
            SpammableButton b = spammableButtons[num];

            b.nameText.text = spammables[num].unitName;
            b.button.onClick.AddListener(
                () => TryBuy(num, activeBuyAmount));
        }
    }

    bool TryBuy(int id, int amount)
    {
        Debug.Log($"Boop!\nID: {id}");

        // Continue only when purchase is validated
        if (!CanAfford(id, amount)) return false;

        // Complete and signal purchase
        SpammableSaveData[] data = GameDataManager.gameData.spammablesData;
        int owned = data[id].owned;

        Debug.Log($"Can buy!\nCost: {spammables[id].GetCost(amount, owned)}");

        GameDataManager.gameData.baseData.currentBits -=
            spammables[id].GetCost(amount, owned);
        EventBus.GoSpammablePurchase(spammables);
        data[id].owned += amount;
        UpdateButton(id, amount);

        return true;
    }

    bool CanAfford(int id, int amount)
    {
        GameData gameData = GameDataManager.gameData;

        // Don't check if invalid id
        if (spammables.Count < id) return false;

        SpammableData s = spammables[id];
        int owned = gameData.spammablesData[id].owned;

        return gameData.baseData.currentBits >= s.GetCost(amount, owned);
    }

    void UpdateShop(double amount)
    {
        int loopLen = Mathf.Min(spammableButtons.Length, spammables.Count);

        for (int i = 0; i < loopLen; i++)
        {
            UpdateButton(i, 1);
        }
    }

    // Amount is subject to change, such as if holding a key changes how many can be bought at once
    void UpdateButton(int id, int amount)
    {
        SpammableButton b = spammableButtons[id]; // Shorthand for readability
        SpammableData s = spammables[id];
        int owned = GameDataManager.gameData.spammablesData[id].owned;

        b.ownedText.text = Utilities.ToBitNotation(owned, 3);
        b.priceText.text = 
            Utilities.ToBitNotation(s.GetCost(amount, owned), 3);
        b.button.interactable = CanAfford(id, amount);
    }
}

[Serializable]
public struct SpammableButton
{
    [SerializeField] public Button button;
    [SerializeField] public TMPro.TextMeshProUGUI nameText,
                                                  ownedText,
                                                  priceText;
}