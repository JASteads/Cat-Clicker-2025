using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesBpsToClickPower",
    menuName = "CatClicker/Upgrade/Effect/Bps To Click Power")]
public class UpgradesBpsToClickPower : UpgradeEffect
{
    public double perecent;

    public override void Apply()
    {
        GameDataManager.gameData.baseData.bpsToClickPowerMulti += perecent;
    }

    public override string GetEffectType()
    {
        return "BPS To Click Power";
    }
}
