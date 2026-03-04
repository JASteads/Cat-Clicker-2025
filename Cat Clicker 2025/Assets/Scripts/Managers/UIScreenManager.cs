using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{
    public static UIScreenManager Instance; // Singleton behavior

    [SerializeField] List<GameObject> screens;
    [SerializeField] GameObject mainScreen;
    [SerializeField] Image dimmingScreen;

    public Stack<Transform> interfaceStack;

    void Awake()
    {
        Instance = this;
        interfaceStack = new Stack<Transform>();
        ShowScreen(0);
    }

    void OnEnable()
    {
        EventBus.OnInterfaceFocus += FocusInterface;
    }

    void OnDisable()
    {
        EventBus.OnInterfaceFocus -= FocusInterface;
    }

    public void ShowScreen(int id)
    {
        for (int i = 0; i < screens.Count; i++)
        {
            screens[i].SetActive(i == id);
        }
    }

    void FocusInterface(Transform ui, bool isActive)
    {
        Transform dimTF = dimmingScreen.transform;

        if (isActive)
        {
            interfaceStack.Push(ui);

            // Place dimming screen behind the target ui
            dimTF.SetParent(ui.parent, false);
            dimTF.SetAsLastSibling();
            dimmingScreen.gameObject.SetActive(true);

            // Place UI on top of dimming screen
            ui.SetAsLastSibling();
        }
        else
        {
            interfaceStack.Pop();

            if (interfaceStack.Count == 0)
            {
                dimmingScreen.gameObject.SetActive(false);
            }
            else
            {
                // Move dimming screen to the screen that was beneath
                dimTF.SetParent(interfaceStack.Peek().parent);
                dimTF.SetAsLastSibling();
                interfaceStack.Peek().SetAsLastSibling();
            }
        }

        ui.gameObject.SetActive(isActive);
    }
}
