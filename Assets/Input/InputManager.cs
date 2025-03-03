using UnityEngine;

public class InputManager : MonoBehaviour
{
    TPCPlayerControls TPCplayerControls;
    AnimationManager animationManager;
    CameraManager cameraManager;
    TPCPlayerLocomotion playerLocomotion;
    Interactor interactor;
    Flashlight flashlight;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizonalInput;
    public float moveAmount;

    public bool shiftInput;
    public bool interactInput;
    public bool flashlightInput = false;
    public bool flashlightOn = false;

    [Header("Normal Map Scrolling")]
    [SerializeField] private Material material; // Assign the material in the inspector
    [SerializeField] private float scrollRate = 0.1f; // Adjust scrolling speed
    [SerializeField] private bool invertScroll = false; // Flip scroll direction
    private Vector2 normalMapOffset; // Track the offset

    private void Awake()
    {
        animationManager = GetComponent<AnimationManager>();
        cameraManager = FindFirstObjectByType<CameraManager>();
        playerLocomotion = GetComponent<TPCPlayerLocomotion>();
        interactor = GetComponent<Interactor>();
        flashlight = GetComponent<Flashlight>();
    }

    private void OnEnable()
    {
        if (TPCplayerControls == null)
        {
            TPCplayerControls = new TPCPlayerControls();
            TPCplayerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            TPCplayerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            TPCplayerControls.PlayerActions.Shift.performed += i => shiftInput = true;
            TPCplayerControls.PlayerActions.Shift.canceled += i => shiftInput = false;

            TPCplayerControls.PlayerActions.Interact.performed += i => interactInput = true;
            TPCplayerControls.PlayerActions.Interact.canceled += i => interactInput = false;

            TPCplayerControls.PlayerActions.Flashlight.performed += i => flashlightInput = true;
            TPCplayerControls.PlayerActions.Flashlight.canceled += i => flashlightInput = false;
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
        UpdateNormalMapOffset();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleInteractInput();
        HandleFlashlightInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizonalInput = movementInput.x;

        cameraInputY = cameraInput.y;

        cameraInputX = Mathf.Min(cameraInput.x, 0);

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizonalInput) + Mathf.Abs(verticalInput));
        animationManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintInput()
    {
        playerLocomotion.isSprinting = shiftInput && moveAmount > 0.5f;
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            interactor.AttemptInteract();
        }
    }

    private void HandleFlashlightInput()
    {
        if (flashlightInput)
        {
            flashlightOn = true;
            animationManager.SetEquipFlashlight(true);
        }
        else
        {
            flashlightOn = false;
            animationManager.SetEquipFlashlight(false);
        }

        if (flashlightOn)
        {
            flashlight.CastFlashlightRay();

        }
    }

    public void DisablePlayerInput()
    {
        if (TPCplayerControls != null)
        {
            TPCplayerControls.Disable();
            horizonalInput = 0;
            verticalInput = 0;
            movementInput = Vector2.zero;
        }
    }

    public void EnablePlayerInput()
    {
        if (TPCplayerControls != null)
        {
            TPCplayerControls.Enable();
        }
    }

    private void UpdateNormalMapOffset()
    {
    }
}
