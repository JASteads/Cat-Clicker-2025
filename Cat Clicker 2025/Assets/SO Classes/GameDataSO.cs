using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Scriptable Objects/Game Data")]
public class GameDataSO : ScriptableObject
{
    public double currentBits,
                  totalBits,
                  bitsPerSecond,
                  clickPower;
    
    public int totalClicks;

    public void OnClick()
    {
        currentBits += clickPower;
        totalBits += clickPower;
        ++totalClicks;
    }
                
}