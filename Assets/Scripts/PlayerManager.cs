using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    TPCPlayerLocomotion playerLocomotion;
    CameraManager cameraManager;

    public bool shouldFollowTarget;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<TPCPlayerLocomotion>();
        cameraManager = FindFirstObjectByType<CameraManager>();

    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        if(shouldFollowTarget)
        cameraManager.FollowTarget();
    }
}
