using UnityEngine;

[CreateAssetMenu(fileName = "AchievementInfinity", menuName = "CatClicker/Achievement/Effect/Infinity")]
public class AchievementInfinity : AchievementEffect // Stat-specific achievements
{
    public SpammableSaveData target;
    public InfnityTracking tracker;
    public int targetValue;

    public override bool CheckCondition()
    {
        GameData data = GameDataManager.gameData;

        switch (tracker)
        {
            case InfnityTracking.OWNED:
                return target.owned >= targetValue;
            case InfnityTracking.TOTAL_BITS:
                return data.baseData.totalBits >= targetValue;
            case InfnityTracking.TOTAL_BPS:
                return data.GetTotalBPS() >= targetValue;
            case InfnityTracking.UPGRADE_COUNT:
                return data.upgradesPool.Count >= targetValue;
            default:
                return false;
        }
    }

    public override AchievementType GetAchievementType()
    {
        return AchievementType.INFINITY;
    }
}

public enum InfnityTracking
{
    OWNED,
    TOTAL_BITS,
    TOTAL_BPS,
    UPGRADE_COUNT
}