using UnityEngine;
using TMPro;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Assign your TextMeshPro component
    public float blinkInterval = 0.5f;  // Time between blinks
    public bool startBlinkingOnStart = true; // Whether to start blinking immediately

    private Coroutine blinkCoroutine;
    private bool isBlinking = false;

    private void Start()
    {
        if (startBlinkingOnStart)
        {
            StartBlinking();
        }
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            blinkCoroutine = StartCoroutine(BlinkText());
            isBlinking = true;
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            StopCoroutine(blinkCoroutine);
            isBlinking = false;
            SetTextAlpha(1f); // Ensure the text is fully visible when stopping
        }
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            SetTextAlpha(0f); // Make text invisible
            yield return new WaitForSeconds(blinkInterval);

            SetTextAlpha(1f); // Make text visible
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void SetTextAlpha(float alpha)
    {
        if (textMeshPro != null)
        {
            Color color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }
    }
}