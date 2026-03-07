using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartScreenManager : MonoBehaviour
{
    const float HALF_PI = Mathf.PI / 2;

    public float screenAppearTime = 1.5f; // Measured in seconds
    public float titleCardWaitTime = 0.5f; // When the card drops
    public float titleCardAppearTime = 1.5f; // How quickly the card drops
    public float promptFlashDuration = 0.75f; // Speed that prompt appears and disappears
    public Vector2 titleCardPos = new Vector2(0, -450f);

    public RectTransform titleCard;
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI versionText;
    public Image dimmingScreen;

    public PlayerInput playerInput;
    InputAction interactInput;

    void Awake()
    {
        versionText.text = $"Version {Application.version}";
        interactInput = playerInput.actions.FindAction("Interact", true);
        playerInput.DeactivateInput();
        DoIntroAnimation();
    }

    void Update()
    {
        if (interactInput.WasPressedThisFrame())
        {
            playerInput.DeactivateInput();
            StartCoroutine(StartGameRoutine());
        }
    }

    [ContextMenu("Do Intro")]
    void DoIntroAnimation()
    {
        // Full reset
        StopAllCoroutines();

        promptText.gameObject.SetActive(false);
        StartCoroutine(ScreenAppearRoutine(screenAppearTime, false));
        DoTitleCardDrop();
    }

    [ContextMenu("Do Fade Out")]
    void DoFadeOutEffect()
    {
        StartCoroutine(ScreenAppearRoutine(screenAppearTime, true));
    }

    [ContextMenu("Do Title Card Drop")]
    void DoTitleCardDrop()
    {
        StartCoroutine(
            TitleCardDropRoutine(titleCardWaitTime, titleCardAppearTime));
    }

    [ContextMenu("Start Base Game")]
    void BeginBaseGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator ScreenAppearRoutine(float duration, bool inReverse)
    {
        float time = 0f;

        dimmingScreen.gameObject.SetActive(true);

        if (!inReverse)
        {
            yield return new WaitForSeconds(0.5f);
        }

        while (time < duration)
        {
            float waveMultiplier = time / duration;
            float t = inReverse ?
                Mathf.Cos(waveMultiplier * HALF_PI) :
                Mathf.Sin(waveMultiplier * HALF_PI);

            time += Time.deltaTime;
            dimmingScreen.color = Color.Lerp(Color.black, Color.clear, t);
            yield return null;
        }

        if (!inReverse)
        {
            dimmingScreen.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TitleCardDropRoutine(float waitDuration, float appearDuration)
    {
        float time = 0f;

        titleCard.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(waitDuration);

        while (time < appearDuration)
        {
            float sineMultiplier = time / appearDuration;

            time += Time.deltaTime;
            titleCard.anchoredPosition = Vector2.Lerp(
                Vector2.zero, titleCardPos,
                Mathf.Sin(sineMultiplier * HALF_PI));
            yield return null;
        }

        titleCard.anchoredPosition = titleCardPos;

        yield return StartCoroutine(PromptFlashingEffect(promptFlashDuration));
    }

    IEnumerator PromptFlashingEffect(float flashDuration)
    {
        float time = 0f;
        promptText.gameObject.SetActive(true);

        playerInput.ActivateInput();

        while (true)
        {
            time += Time.deltaTime;

            if (time >= flashDuration)
            {
                time = 0f;
                promptText.enabled = !promptText.isActiveAndEnabled;
            }

            yield return null;
        }
    }

    public IEnumerator StartGameRoutine()
    {
        yield return StartCoroutine(
            ScreenAppearRoutine(screenAppearTime, true));

        GameDataManager.StartBaseGame();
    }
}
