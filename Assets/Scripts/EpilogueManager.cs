using System.Collections;
using UnityEngine;

public class EpilogueManager : MonoBehaviour
{
    DialogueUI DUI;
    [SerializeField] DialogueObject FinalDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DUI = FindFirstObjectByType<DialogueUI>();
        DUI.ShowDialogue(FinalDialogue);

        StartCoroutine(DelayedFunctionCall());
        QualitySettings.SetQualityLevel(2);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator DelayedFunctionCall()
    {
        yield return new WaitForSeconds(25f);
        OnEpilogueComplete();
    }

    private void OnEpilogueComplete()
    {
        LevelTransitioner levelTransitioner = FindFirstObjectByType<LevelTransitioner>();
        levelTransitioner.StartFadein(0);
    }

}
