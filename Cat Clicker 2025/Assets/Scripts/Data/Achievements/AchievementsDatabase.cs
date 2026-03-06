using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsDatabase", menuName = "CatClicker/Achievement/Database")]
public class AchievementsDatabase : ScriptableObject
{
    public List<AchievementData> list;
}
