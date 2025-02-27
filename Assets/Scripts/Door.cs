using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractableObject
{
    [SerializeField] private int LevelIndexToLoad = 0;

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
        SceneManager.LoadScene(LevelIndexToLoad);
    }
}
