using UnityEngine;

public class DevInteractable : MonoBehaviour, IInteractableObject
{
    public void Interact()
    {
        Debug.Log("Hello, I have been interacted with!");
        Destroy(this);
    }
}
