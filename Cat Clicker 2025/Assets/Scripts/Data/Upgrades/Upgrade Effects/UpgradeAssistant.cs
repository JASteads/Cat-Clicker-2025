using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeAssistant", menuName = "Scriptable Objects/Upgrade/Effect/Assistant")]
public class UpgradeAssistant : UpgradeEffect
{
    public override void Apply()
    {
        // Add assistant
    }

    public override string GetEffectType()
    {
        return "Assistant";
    }
}
