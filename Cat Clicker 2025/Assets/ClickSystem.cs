using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickSystem : MonoBehaviour
{
    [SerializeField] GameDataSO gameData;
    [SerializeField] Button bigButton;

    long clickPower = 1; // Hard-coded for testing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bigButton = gameObject.GetComponent<Button>();
        bigButton.onClick.AddListener(gameData.OnClick);
    }
}
