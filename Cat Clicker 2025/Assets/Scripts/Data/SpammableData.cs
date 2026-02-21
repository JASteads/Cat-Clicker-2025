using UnityEngine;

[CreateAssetMenu(fileName = "SpammableData", menuName = "CatClicker/Spammable Data")]
public class SpammableData : ScriptableObject
{
    const float COST_EXPONENT = 1.15f;

    [Header("Settings")]
    public string unitName = "Unit";
    [SerializeField] public double basePrice = 1;
    [SerializeField] public double baseBPS = 0.1;

    public double GetCost(int amount, int owned)
    {
        float r = COST_EXPONENT;
        double rOwned = Mathf.Pow(r, owned);

        // Using geometric series sum formula to avoid excess calculation
        return basePrice * rOwned * (1 - Mathf.Pow(r, amount)) / (1 - r);
    }

    public double GetRawBPS(int owned)
    {
        return baseBPS * owned;
    }    
}