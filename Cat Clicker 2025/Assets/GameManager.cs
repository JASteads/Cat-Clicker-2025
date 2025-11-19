using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameDataSO gameData;
    [SerializeField] ClickSystem clickSystem;
    [SerializeField] Canvas canvas;

    void Start()
    {
        canvas = Instantiate(canvas.gameObject)
            .GetComponent<Canvas>();
        clickSystem = Instantiate(clickSystem.gameObject)
            .GetComponent<ClickSystem>();

        clickSystem.transform.SetParent(canvas.transform, false);
        clickSystem.transform.localPosition = new Vector2(-610, 150);
    }
}
