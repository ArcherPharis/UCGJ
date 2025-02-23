using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    TPCPlayerLocomotion playerLocomotion;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<TPCPlayerLocomotion>();

    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }
}
