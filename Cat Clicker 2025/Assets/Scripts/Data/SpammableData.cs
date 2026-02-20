public class SpammableData
{
    public string name;

    public readonly int basePrice;
    public int count;
    public double totalBitProd;
    
    // AssistantData assistant;

    public SpammableData(string name, int basePrice)
    {
        this.name = name;
        this.basePrice = basePrice;
        count = 0;
        totalBitProd = 0;
    }
}

public class AssistantData
{

}
