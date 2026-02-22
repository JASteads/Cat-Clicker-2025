using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public static UIScreenManager Instance; // Singleton behavior

    [SerializeField] List<GameObject> screens;
    [SerializeField] GameObject mainScreen;

    void Awake()
    {
        Instance = this;
        ShowScreen(0);
    }

    public void ShowScreen(int id)
    {
        for (int i = 0; i < screens.Count; i++)
        {
            screens[i].SetActive(i == id);
        }
    }
}
