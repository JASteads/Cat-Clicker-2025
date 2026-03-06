using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameData", menuName = "CatClicker/BaseGameData")]
public class BaseGameData : ScriptableObject
{
    [SerializeField] public List<SpammableData> spammables;
}
