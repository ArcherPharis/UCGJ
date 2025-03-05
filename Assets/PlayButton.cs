using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera targetCamera;
    public float transitionSpeed = 2f;

    private bool isTransitioning = false;
    private float transitionProgress = 0f;
    private Vector3 startPosition;
    private Quaternion startRotation;

    [SerializeField] Button PlayButt;
    [SerializeField] Button QuitButton;
    [SerializeField] Button CreditsButton;
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject TitleLogo;

    public Image image1; 
    public Image image2;
    public Image image3;
    public Image blackScreen;
    public Image whiteScreen;
    public float fadeDuration = 2f;
    public float whitefadeDuration = 3f;
    public UnityEvent onFadeComplete;

    DialogueUI DGUI;
    [SerializeField] GameObject Creds;
    [SerializeField] GameObject Creds2;


    private void Awake()
    {
        HideUI();
        StartCoroutine(FadeSequence());
        DGUI = FindFirstObjectByType<DialogueUI>();
    }

    private IEnumerator FadeSequence()
    {
      

        // Fade in and out for image 1
        yield return StartCoroutine(FadeIn(image1));
        yield return StartCoroutine(FadeOut(image1));

        // Fade in and out for image 2
        yield return StartCoroutine(FadeIn(image2));
        yield return StartCoroutine(FadeOut(image2));

        yield return StartCoroutine(FadeIn(image3));
        yield return StartCoroutine(FadeOut(image3));

        yield return StartCoroutine(FadeOut(blackScreen));

        // Call the function after both fades are done
        OnFadesComplete();
    }

    private IEnumerator WhiteFadeSequence()
    {

        yield return StartCoroutine(WhiteFadeIn(whiteScreen));
        yield return StartCoroutine(WhiteFadeOut(whiteScreen));
    }

    private IEnumerator FadeIn(Image image)
    {
        image.enabled = true;

        float timeElapsed = 0f;
        Color startColor = image.color;
        startColor.a = 0; 
        image.color = startColor;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            startColor.a = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            image.color = startColor;
            yield return null;
        }

        startColor.a = 1;
        image.color = startColor;
    }

    private IEnumerator FadeOut(Image image)
    {
        float timeElapsed = 0f;
        Color startColor = image.color;

        // Fade out over time
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            startColor.a = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
            image.color = startColor;
            yield return null;
        }
        startColor.a = 0;
        image.color = startColor;

        image.enabled = false;

    }

    private IEnumerator WhiteFadeIn(Image image)
    {
        image.enabled = true;

        float timeElapsed = 0f;
        Color startColor = image.color;
        startColor.a = 0;
        image.color = startColor;

        while (timeElapsed < whitefadeDuration)
        {
            timeElapsed += Time.deltaTime;
            startColor.a = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            image.color = startColor;
            yield return null;
        }

        startColor.a = 1;
        image.color = startColor;
    }

    private IEnumerator WhiteFadeOut(Image image)
    {
        float timeElapsed = 0f;
        Color startColor = image.color;

        // Fade out over time
        while (timeElapsed < whitefadeDuration)
        {
            timeElapsed += Time.deltaTime;
            startColor.a = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
            image.color = startColor;
            yield return null;
        }
        startColor.a = 0;
        image.color = startColor;

        image.enabled = false;

    }

    public void OnFadesComplete()
    {
        ShowUI();

    }


    private void Update()
    {
        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;

            mainCamera.transform.position = Vector3.Lerp(startPosition, targetCamera.transform.position, transitionProgress);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetCamera.transform.rotation, transitionProgress);

            if (transitionProgress >= 1f)
            {
                isTransitioning = false;
                mainCamera.enabled = false;
                targetCamera.enabled = true;
                DGUI.PlayInitialDialogue();
            }
        }
    }

    public void OnButtonClick()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            transitionProgress = 0f;
            startPosition = mainCamera.transform.position;
            startRotation = mainCamera.transform.rotation;
            HideUI();
            targetCamera.enabled = true;
            StartCoroutine(WhiteFadeSequence());
        }
    }

    public void OnBackButtonClicked()
    {
        ShowUI();
        BackButton.SetActive(false);
        Creds.SetActive(false);
        Creds2.SetActive(false);

    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }


    public void OnCredits()
    {
        HideUI();
        Creds.SetActive(true);
        Creds2.SetActive(true);
        BackButton.SetActive(true);
    }
    void HideUI()
    {
        PlayButt.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        TitleLogo.SetActive(false);

    }

    void ShowUI()
    {
        PlayButt.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        TitleLogo.SetActive(true);
    }
}

