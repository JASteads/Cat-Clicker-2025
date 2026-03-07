using System.Collections.Generic;
using System.Diagnostics;

public class AchievementsManager
{
    public AchievementsDatabase database;

    public void StartSystem(AchievementsDatabase database)
    {
        this.database = database;

        //List<AchievementData> unlocked = database.list.FindAll(
        //    (a) => GameDataManager.gameData.achivementsPool
        //    .Contains(a.achievementName));

        foreach (var a in database.list)
        {
            a.isUnlocked = GameDataManager.gameData.achivementsPool
                .Contains(a.achievementName);
        }

        EventBus.OnSpecialistMilestone += CheckInnovation;
        EventBus.OnSlowUpdate += CheckInfinity;
        EventBus.OnForceAchievement += TryForceUnlock;
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

    void TryForceUnlock(string name)
    {
        AchievementData target = 
            database.list.Find(a => a.achievementName == name);

        if (target == null)
        {
            UnityEngine.Debug.LogError($"{name} is not a name for an existing achievement");
        }
        else
        {
            UnlockAchievement(target);
        }

    }

    public void StopSystem()
    {
        EventBus.OnSpecialistMilestone -= CheckInnovation;
        EventBus.OnSlowUpdate -= CheckInfinity;
        EventBus.OnForceAchievement -= TryForceUnlock;
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
