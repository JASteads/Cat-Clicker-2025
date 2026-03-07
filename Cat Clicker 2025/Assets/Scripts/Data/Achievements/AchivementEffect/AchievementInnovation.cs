using UnityEngine;

[CreateAssetMenu(fileName = "AchievementInnovation", menuName = "CatClicker/Achievement/Effect/Innovation")]
public class AchievementInnovation : AchievementEffect // Specialist-specific achievements
{
    public int targetID;
    public int targetLevel;

    public override bool CheckCondition()
    {
        GameData data = GameDataManager.gameData;
        return data.specialistData[targetID].level >= targetLevel;
    }

    public override AchievementType GetAchievementType()
    {
        return AchievementType.INNOVATION;
    }
}
