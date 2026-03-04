using System;

public static class EventBus
{
    // Common Events
    public static event Action OnBigButtonClick;
    public static event Action<double> OnBitsAdded;
    public static event Action OnGameTick;
    public static event Action OnSpamBPSChange;
    public static event Action<AchievementData> OnAchievement;
    public static event Action<UnityEngine.Transform, bool> OnInterfaceFocus;

    // Shop Events
    public static event Action<SpammableData> OnSpammablePurchase;
    public static event Action OnUpgradePurchase;

    // Specialists Events
    public static event Action OnSpecialistPurchase;
    public static event Action<int, int> OnSpecialistMilestone;

    // Fever Events
    public static event Action OnFeverTimeStart;
    public static event Action OnFeverTimeEnd;

    // Common Invocations
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

    public static void GoAchievement(AchievementData a)
    {
        OnAchievement?.Invoke(a);
    }

    public static void GoSpamBPSChange()
    {
        OnSpamBPSChange?.Invoke();
    }

    public static void GoInterfaceFocus(UnityEngine.Transform obj, bool isActive)
    {
        OnInterfaceFocus?.Invoke(obj, isActive);
    }


    // Shop Invocations
    public static void GoSpammablePurchase(SpammableData s)
    {
        OnSpammablePurchase?.Invoke(s);
    }

    public static void GoUpgradePurchase()
    {
        OnUpgradePurchase?.Invoke();
    }


    // Specialist Invocations
    public static void GoSpecialistPurchase()
    {
        OnSpecialistPurchase?.Invoke();
    }

    public static void GoSpecialistMilestone(int id, int level)
    {
        OnSpecialistMilestone?.Invoke(id, level);
    }


    // Fever Invocations
    public static void GoFeverTimeStart()
    {
        OnFeverTimeStart?.Invoke();
    }

    public static void GoFeverTimeEnd()
    {
        OnFeverTimeEnd?.Invoke();
    }
}
