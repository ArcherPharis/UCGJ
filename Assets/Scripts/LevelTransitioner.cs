using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelTransitioner : MonoBehaviour
{
    public Image blackScreen;
    public float fadeDuration = 6f;
    public UnityEvent onFadeComplete;
    public int IndexToLoad = 0;

    public void StartFadein(int index)
    {
        StartCoroutine(FadeSequence());
        IndexToLoad = index;
    }

    private IEnumerator FadeSequence()
    {
        yield return StartCoroutine(FadeIn(blackScreen));
        //yield return StartCoroutine(FadeOut(blackScreen));

        OnFadeComplete();
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

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(IndexToLoad);
    }
}
