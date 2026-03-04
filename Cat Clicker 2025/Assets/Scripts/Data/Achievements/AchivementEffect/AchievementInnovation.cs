using UnityEngine;

[CreateAssetMenu(fileName = "AchievementInnovation", menuName = "CatClicker/Achievement/Effect/Innovation")]
public class AchievementInnovation : AchievementEffect // Specialist-specific achievements
{
    public SpecialistSaveData target;
    public int targetLevel;

    public override bool CheckCondition()
    {
        return target.level >= targetLevel;
    }

    public override AchievementType GetAchievementType()
    {
        return AchievementType.INNOVATION;
    }
}
