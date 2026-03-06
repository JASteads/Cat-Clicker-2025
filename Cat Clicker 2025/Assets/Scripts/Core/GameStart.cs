using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart
{
    [RuntimeInitializeOnLoadMethod]
    static void StartGame()
    {
        Debug.Log("Game started" +
            $"\nPersistent Data Path :{Application.persistentDataPath}");
#if !UNITY_EDITOR
        SceneManager.LoadScene("Start Screen", LoadSceneMode.Single);
#endif
    }
}
