using System;
using System.Collections.Generic;

public static class EventBus
{
    public static event Action OnBigButtonClick;
    public static event Action<double> OnBitsAdded;
    public static event Action OnGameTick;
    public static event Action OnSpamBPSChange;
    public static event Action<SpammableData> OnSpammablePurchase;
    public static event Action OnUpgradePurchase;
    public static event Action<string> OnAchievement;

    public static void GoBigButtonClick()
    {
        OnBigButtonClick?.Invoke();
    }

    public static void GoBitsAdded(double amount)
    {
        OnBitsAdded?.Invoke(amount); 
    }
    
    public static void GoGameTick()
    {
        OnGameTick?.Invoke();
    }

    public static void GoSpammablePurchase(SpammableData s)
    {
        OnSpammablePurchase?.Invoke(s);
    }

    public static void GoUpgradePurchase()
    {
        OnUpgradePurchase?.Invoke();
    }

    public static void GoAchievement(string name)
    {
        OnAchievement?.Invoke(name);
    }

    public static void GoSpamBPSChange()
    {
        OnSpamBPSChange?.Invoke();
    }
}
