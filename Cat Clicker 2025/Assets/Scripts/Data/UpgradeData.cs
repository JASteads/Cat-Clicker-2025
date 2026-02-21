using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "CatClicker/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public System.Action OnPurchase;

    [Header("Configuration")]
    [SerializeField] public string upgradeName;
    [SerializeField] public string desc;
    [SerializeField] public double price;

    public void UpdatePrice(double price)
    {
        this.price = price;
    }
}
