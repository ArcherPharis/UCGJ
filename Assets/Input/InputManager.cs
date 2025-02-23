using UnityEngine;

public class InputManager : MonoBehaviour
{
    TPCPlayerControls TPCplayerControls;
    AnimationManager animationManager;
    CameraManager cameraManager;
    TPCPlayerLocomotion playerLocomotion;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizonalInput;
    public float moveAmount;

    public bool shiftInput;

    private void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
        cameraManager = FindFirstObjectByType<CameraManager>();
        playerLocomotion = GetComponent<TPCPlayerLocomotion>();
    }
    private void OnEnable()
    {
        if(TPCplayerControls == null)
        {
            TPCplayerControls = new TPCPlayerControls();
            TPCplayerControls.PlayerMovement.Movement.performed += i 
                => movementInput = i.ReadValue<Vector2>();

            TPCplayerControls.PlayerMovement.Camera.performed += i => 
            cameraInput = i.ReadValue<Vector2>();

            TPCplayerControls.PlayerActions.Shift.performed += i => shiftInput = true;
            TPCplayerControls.PlayerActions.Shift.canceled += i => shiftInput = false;
        }
        TPCplayerControls.Enable();
    }

    private void OnDisable()
    {
        TPCplayerControls.Disable();
    }
    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizonalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizonalInput) + Mathf.Abs(verticalInput));
        animationManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintInput()
    {
        if(shiftInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }
}
