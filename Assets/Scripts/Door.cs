using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractableObject
{
    [SerializeField] private int LevelIndexToLoad = 0;
    [SerializeField] DialogueObject DialogueToSay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        LevelTransitioner levelTransitioner = FindFirstObjectByType<LevelTransitioner>();
        levelTransitioner.StartFadein(LevelIndexToLoad);
        DialogueUI DUI = FindFirstObjectByType<DialogueUI>();
        DUI.ShowDialogue(DialogueToSay);
    }

    public void SetDoorLoadIndex(int value)
    {
        LevelIndexToLoad = value;
    }
}
