using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed = 100f;

    public Coroutine RunTypeText(string TextToType, TMP_Text Text)
    {
        return StartCoroutine(TypeText(TextToType, Text));
    }

    private IEnumerator TypeText(string TextToType, TMP_Text Text)
    {
        Text.text = string.Empty;

        float ElapsedTime = 0;
        int CharacterIndex = 0;

        while (CharacterIndex < TextToType.Length)
        {
            ElapsedTime += Time.deltaTime * typeWriterSpeed;
            CharacterIndex = Mathf.FloorToInt(ElapsedTime);
            CharacterIndex = Mathf.Clamp(CharacterIndex, 0, TextToType.Length);

            Text.text = TextToType.Substring(0, CharacterIndex);

            yield return null;
        }
        Text.text = TextToType;
    }
}
