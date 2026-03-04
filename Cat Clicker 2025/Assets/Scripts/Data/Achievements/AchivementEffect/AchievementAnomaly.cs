using UnityEngine;

[CreateAssetMenu(fileName = "AchievementAnomaly", menuName = "CatClicker/Achievement/Effect/Anomaly")]
public class AchievementAnomaly : AchievementEffect // Secret achievements and easter eggs
{
    public override bool CheckCondition()
    {
        return false;
    }

    public override AchievementType GetAchievementType()
    {
        return AchievementType.ANOMALY;
    }
}
