using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Scriptable Objects/Game Data")]
public class GameDataSO : ScriptableObject
{
    public long currentBits,
                totalBits,
                bitsPerSecond,
                clickPower;
    
    public int totalClicks;

    public void OnClick()
    {

    }
                
}