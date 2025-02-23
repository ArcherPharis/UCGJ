using UnityEngine;

public class InputManager : MonoBehaviour
{
    TPCPlayerControls TPCplayerControls;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizonalInput;
    private void OnEnable()
    {
        if(TPCplayerControls == null)
        {
            TPCplayerControls = new TPCPlayerControls();
            TPCplayerControls.PlayerMovement.Movement.performed += i 
                => movementInput = i.ReadValue<Vector2>();

        }
        TPCplayerControls.Enable();
    }

    private void OnDisable()
    {
        TPCplayerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizonalInput = movementInput.x;
    }

}
