using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "GameDataSO", menuName = "Scriptable Objects/Game Data")]
public class GameDataSO : ScriptableObject
{
    public double currentBits,
                  totalBits,
                  bitsPerSecond,
                  clickPower;
    
    public int totalClicks;
    public List<SpammableData> unitsData;
    public List<SpammableData> buildingsData;

    void Awake()
    {
        currentBits = totalBits = bitsPerSecond = totalClicks = 0;
        clickPower = 1;

        unitsData = new List<SpammableData>();
        buildingsData = new List<SpammableData>();

        unitsData.Add(new SpammableData("Bit Cat", 10));
        FileManager.SaveGame(this);
    }

    public void OnClick()
    {
        currentBits += clickPower;
        totalBits += clickPower;
        ++totalClicks;
    }
                
}