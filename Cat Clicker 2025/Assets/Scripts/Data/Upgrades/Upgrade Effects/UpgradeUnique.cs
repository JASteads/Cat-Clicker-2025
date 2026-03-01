using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpgradeUnique",
    menuName = "Scriptable Objects/Upgrade/Effect/Unique")]
public class UpgradeUnique : UpgradeEffect
{
    public UnityEvent e; // Arbitrary event that can be defined elsewhere

    public override void Apply()
    {
        e.Invoke();
    }

    public override string GetEffectType()
    {
        return "Unique";
    }
}
