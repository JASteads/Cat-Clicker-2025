using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSimpleBPS", 
    menuName = "CatClicker/Upgrade/Effect/Simple CP Multiplier")]
public class UpgradeSimpleClickPower : UpgradeEffect
{
    public float multiplier;

    public override void Apply()
    {
        GameDataManager.gameData.baseData.clickPowerMulti *= multiplier;
    }

    public override string GetEffectType()
    {
        return "Simple CP Multiplier";
    }
}