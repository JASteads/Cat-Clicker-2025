using UnityEngine;

public class GameStart
{
    [RuntimeInitializeOnLoadMethod]
    static void StartGame()
    {
        Debug.Log("Game started" +
            $"\nPersistent Data Path :{Application.persistentDataPath}");
    }
}
