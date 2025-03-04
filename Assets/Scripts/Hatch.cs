using UnityEngine;

public class Hatch : MonoBehaviour, IFlashlightable
{
    [SerializeField] Animator animator;
    [SerializeField] Door door;
    int hatchStatus;

    private void Awake()
    {
        door = GetComponent<Door>();
        hatchStatus = Animator.StringToHash("Open");

    }

    public void OnFlashlightHit()
    {
        animator.SetBool(hatchStatus, true);
    }

}
