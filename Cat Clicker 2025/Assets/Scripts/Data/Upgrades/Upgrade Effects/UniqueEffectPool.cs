using UnityEngine;

public class UniqueEffectPool : MonoBehaviour
{
    public void FeverishFanfare()
    {
        GameDataManager.gameData.feverData.holdTime += 5;
    }

    public void WinTheGame()
    {
        EventBus.GoForceAchievement("You're Winner !");
        EventBus.GoGameWin();
    }
}
