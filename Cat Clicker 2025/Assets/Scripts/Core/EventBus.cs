using System;
using UnityEngine;

public static class EventBus
{
    public static event Action OnBigButtonClick;
    public static event Action<double> OnBitsAdded;
    public static event Action OnGameTick;
    public static event Action<double> OnBPSChange;
    public static event Action<string, int> OnSpammablePurchase;
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

    public static void GoSpammablePurchase(string name, int amount)
    {
        OnSpammablePurchase?.Invoke(name, amount);
    }

    public static void GoAchievement(string name)
    {
        OnAchievement?.Invoke(name);
    }

    public static void GoBPSChange(double newBPS)
    {
        OnBPSChange?.Invoke(newBPS);
    }
}
