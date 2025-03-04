using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text text;
    public DialogueObject testDialogue;
    private TypewriterEffect typewriterEffect;

    private void Awake()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject DialogueObj)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(GoThroughDialogue(DialogueObj));
    }

    private IEnumerator GoThroughDialogue(DialogueObject Obj)
    {
        foreach(string dialogue in Obj.Dialogue)
        {
            yield return typewriterEffect.RunTypeText(dialogue, text);
            yield return new WaitForSeconds(3);
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        text.text = string.Empty;
    }

    public void PlayInitialDialogue()
    {
        ShowDialogue(testDialogue);
    }

}
