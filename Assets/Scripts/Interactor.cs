using UnityEngine;

interface IInteractableObject
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform InteractorOrigin;
    [SerializeField] float InteractRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptInteract()
    {
        Ray ray = new Ray(InteractorOrigin.position, InteractorOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit HitInfo, InteractRange))
        {
            if (HitInfo.collider.gameObject.TryGetComponent(out IInteractableObject InteractedObj))
            {
                InteractedObj.Interact();
            }
        }
    }
}
