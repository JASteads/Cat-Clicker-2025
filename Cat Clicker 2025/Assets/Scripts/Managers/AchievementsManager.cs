public class AchievementsManager // INCOMPLETE
{
    public AchievementsManager()
    {

    }

    public void OnUnlock(AchievementData a)
    {
        GameData data = GameDataManager.gameData;
        int type = (int)a.effect.GetAchievementType();

        a.isUnlocked = true;
        data.achivementsPool.Add(a.achievementName);
        data.skillpoints[type] += a.skillPoints;
        EventBus.GoAchievement(a);
    }
}
