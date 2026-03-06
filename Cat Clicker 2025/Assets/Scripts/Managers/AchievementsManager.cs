using System.Collections.Generic;

public class AchievementsManager
{
    public AchievementsDatabase database;

    public void StartSystem(AchievementsDatabase database)
    {
        this.database = database;

        List<AchievementData> unlocked = database.list.FindAll(
            (a) => GameDataManager.gameData.achivementsPool
            .Contains(a.achievementName));

        // Set all achievements to show that they're unlocked based on save
        foreach (var a in unlocked)
        {
            a.isUnlocked = true;
        }

        EventBus.OnSpecialistMilestone += CheckInnovation;
        EventBus.OnSlowUpdate += CheckInfinity;
    }

    void CheckInfinity()
    {
        CheckAchievementType(AchievementType.INFINITY);   
    }

    void CheckInnovation(int id, int level)
    {
        CheckAchievementType(AchievementType.INNOVATION);
    }

    void CheckAsterism()
    {
        CheckAchievementType(AchievementType.ASTERISM);
    }

    void CheckAchievementType(AchievementType type)
    {
        if (database == null) return;

        foreach (var a in database.list)
        {
            // These are the only ones that need to be checked
            // on a regular basis (for now)
            if (!a.isUnlocked
                && a.effect.GetAchievementType() == type
                && a.effect.CheckCondition())
            {
                UnlockAchievement(a);
            }
        }
    }

    public void StopSystem()
    {
        EventBus.OnSpecialistMilestone -= CheckInnovation;
        EventBus.OnSlowUpdate -= CheckInfinity;
    }

    public void UnlockAchievement(AchievementData a)
    {
        GameData data = GameDataManager.gameData;
        int type = (int)a.effect.GetAchievementType();

        a.isUnlocked = true;
        data.achivementsPool.Add(a.achievementName);
        data.skillpoints[type] += a.skillPoints;
        EventBus.GoAchievement(a);

        UnityEngine.Debug.Log($"Achievement Unlocked: {a.achievementName}");
    }
}
