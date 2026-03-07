using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeverSystemUI : MonoBehaviour
{
    [Header("Graphic Elements")]
    [SerializeField] TextMeshProUGUI feverTimeText;
    [SerializeField] public Image feverBar;
    [SerializeField] public Image feverMeter;
    [SerializeField] public Color feverMeterColor;

    [Header("Color Settings")]
    [SerializeField] public Color feverColorLow;
    [SerializeField] public Color feverColorHigh;
    [SerializeField] public Color feverColorMax;
    [SerializeField] public Color feverBarColor;

    [Header("System")]
    public FeverSystem feverSystem;
    [SerializeField] public float percentCharge;

    void Awake()
    {
        feverSystem = new FeverSystem();
        feverMeterColor = feverBar.color;
        DisplayFeverTimeText(false);
    }

    void OnEnable()
    {
        EventBus.OnBigButtonClick += feverSystem.AddCharge;
        EventBus.OnFeverTimeStart += StartFeverTime;
        EventBus.OnFeverTimeEnd += EndFeverTime;
    }

    void OnDisable()
    {
        EventBus.OnBigButtonClick -= feverSystem.AddCharge;
        EventBus.OnFeverTimeStart -= StartFeverTime;
        EventBus.OnFeverTimeEnd -= EndFeverTime;
    }

    void Update()
    {
        GameData data = GameDataManager.gameData;

        feverSystem.UpdateSystem();

        percentCharge = feverSystem.charge / data.feverData.maxCharge;
        feverMeter.rectTransform.localScale = new Vector2(1, percentCharge);

        if (!feverSystem.isFeverTime)
        {
            // Color wheel-style blend for fever meter charge
            Color.RGBToHSV(
                feverColorLow, out float h1, out float s1, out float v1);
            Color.RGBToHSV(
                feverColorHigh, out float h2, out float s2, out float v2);

            float h = 
                Mathf.LerpAngle(h1 * 360f, h2 * 360f, percentCharge) / 360f;
            float s = Mathf.Lerp(s1, s2, percentCharge);
            float v = Mathf.Lerp(v1, v2, percentCharge);

            feverMeter.color = Color.HSVToRGB(h, s, v);
        }
        else
        {
            float timeLeftPercent = 
                feverSystem.holdTimeLeft / data.feverData.holdTime;

            feverTimeText.alpha = timeLeftPercent;
            feverTimeText.rectTransform.localScale = 
                Vector2.Lerp(Vector2.one * 0.75f, Vector2.one, timeLeftPercent);
        }
    }

    void StartFeverTime()
    {
        SetFeverColorToMax();
        DisplayFeverTimeText(true);
    }

    void EndFeverTime()
    {
        SetFeverColorToNormal();
        DisplayFeverTimeText(false);
    }

    void DisplayFeverTimeText(bool isActive)
    {
        if (feverTimeText != null)
        {
            feverTimeText.gameObject.SetActive(isActive);
        }
    }

    void SetFeverColorToMax()
    {
        feverMeter.color = feverColorMax;
        feverBar.color = feverColorLow;
    }

    void SetFeverColorToNormal()
    {
        feverMeter.color = feverColorHigh;
        feverBar.color = feverBarColor;
    }
}
    