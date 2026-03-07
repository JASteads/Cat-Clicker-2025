using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeList", menuName = "CatClicker/Upgrade/Database")]
public class UpgradeDatabase : ScriptableObject
{
    public List<UpgradeData> list;
}
