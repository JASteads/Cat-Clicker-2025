using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button achievementsButton;
    [SerializeField] Button quitButton;

    [SerializeField] AchievementsUI achievementsUI;

    void Start()
    {
        resumeButton.onClick.AddListener(
            () => EventBus.GoInterfaceFocus(transform, false));

        achievementsButton.onClick.AddListener(
            () => EventBus.GoInterfaceFocus(achievementsUI.transform, true));

        quitButton.onClick.AddListener(GameDataManager.QuitGame);
    }
}
