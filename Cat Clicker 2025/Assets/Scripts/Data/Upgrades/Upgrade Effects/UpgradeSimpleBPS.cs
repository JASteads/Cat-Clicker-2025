using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSimpleBPS", menuName = "CatClicker/Upgrade/Effect/Simple BPS")]
public class UpgradeSimpleBPS : UpgradeEffect
{
    public float multiplier;

    public override void Apply()
    {
        SpammableSaveData[] spamData = GameDataManager.gameData.spammablesData;
        spamData[targetUnitID].multiplier *= multiplier;
        EventBus.GoSpamBPSChange();
    }

    public override string GetEffectType()
    {
        return "Simple BPS";
    }
}