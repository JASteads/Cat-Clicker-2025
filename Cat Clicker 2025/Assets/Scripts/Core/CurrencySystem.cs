public class CurrencySystem
{
    public GameData gameData;
    
    public CurrencySystem(GameData data)
    {
        gameData = data;
        EventBus.OnGameTick += Tick;
    }

    public void AddBits(double amount)
    {
        gameData.baseData.currentBits += amount;
    }

    public void ChangeBPS(double newBPS)
    {
        gameData.baseData.bps = newBPS;
    }

    public void Tick()
    {
        double bpsRate = gameData.baseData.bps * UnityEngine.Time.deltaTime;
        AddBits(bpsRate);
        EventBus.GoBitsAdded(bpsRate);
    }

    public void StopSystem()
    {
        EventBus.OnGameTick -= Tick;
    }
}
