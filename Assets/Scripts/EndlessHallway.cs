using UnityEngine;

public class EndlessHallway : MonoBehaviour
{
    [SerializeField] BoxCollider ResetBox;
    [SerializeField] private Transform PlayerRestartPoint;

    public void EnableResetBox()
    {
        ResetBox.enabled = true;
    }

    public void DisableResetBox()
    {
        ResetBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InputManager>())
        {
            other.gameObject.transform.position = PlayerRestartPoint.position;
            other.gameObject.transform.rotation = PlayerRestartPoint.rotation;
        }
    }

}
