using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "CatClicker/Achievement/Data")]
public class AchievementData : ScriptableObject
{
    public string achievementName;
    public string desc;
    public int skillPoints;
    public bool isUnlocked = false;
    [SerializeField] public AchievementEffect effect;
}
public interface IAchievementEffect
{
    bool CheckCondition();
    AchievementType GetAchievementType();
}

public abstract class AchievementEffect : ScriptableObject, IAchievementEffect
{
    public int targetUnitID;

    public abstract bool CheckCondition();
    public abstract AchievementType GetAchievementType();
}

public enum AchievementType
{
    INNOVATION,
    INFINITY,
    ASTERISM,
    ANOMALY
}