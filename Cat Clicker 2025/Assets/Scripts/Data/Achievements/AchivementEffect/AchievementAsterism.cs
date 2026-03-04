using UnityEngine;

[CreateAssetMenu(fileName = "AchievementAsterism", menuName = "CatClicker/Achievement/Effect/Asterism")]
public class AchievementAsterism : AchievementEffect // Minigame-specific achievements -- Not implemented yet
{
    public override bool CheckCondition()
    {
        return false;
    }

    public override AchievementType GetAchievementType()
    {
        return AchievementType.ASTERISM;
    }
}
