using UnityEngine;

[CreateAssetMenu(fileName = "SpecialistData", menuName = "CatClicker/Specialist Data")]
public class SpecialistData : ScriptableObject
{
    public string specialistName = "Specialist"; // Default name
    public string specialistTitle = "The Specialist"; // A nickname, basically
    public string specialistDescription = "It's very special";
    public double basePrice = 4311;
    public double levelUpBasePrice = 9001; // Should be more expensive than base
    public float priceExponent = 1.2f;
    public int maxLevel = 30;
    public double bpsMultiPerLevel = 1.1; // +10% per level sounds fun

    public double GetPrice(int level)
    {
        return levelUpBasePrice * Mathf.Pow(priceExponent, level);
    }

    public double GetMulti(int level)
    {
        return level * bpsMultiPerLevel;
    }
}
