using UnityEngine;

public interface IUpgradeEffect
{
    void Apply(); // Change parameters if need be
    string GetEffectType();
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "CatClicker/Upgrade/Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string desc;
    [SerializeField] double price;
    [SerializeField] public UpgradeEffect effect;

    // Making this a function for when things like discounts are implemented
    public double GetPrice()
    {
        return price;
    }
}

public abstract class UpgradeEffect : ScriptableObject, IUpgradeEffect
{
    public int targetUnitID;

    public abstract void Apply();
    public abstract string GetEffectType();
}