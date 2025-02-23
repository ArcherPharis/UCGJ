using UnityEngine;

public class TPCPlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    public float movementSpeed = 5;
    public float rotationSpeed = 10;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizonalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.linearVelocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 TargetDirection = Vector3.zero;
        TargetDirection = cameraObject.forward * inputManager.verticalInput;
        TargetDirection = TargetDirection + cameraObject.right * inputManager.horizonalInput;
        TargetDirection.Normalize();
        TargetDirection.y = 0;

        if(TargetDirection == Vector3.zero)
        {
            TargetDirection = transform.forward;
        }

        Quaternion TargetRotation = Quaternion.LookRotation(TargetDirection);
        Quaternion PlayerRotation = Quaternion.Slerp(transform.rotation, TargetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = PlayerRotation;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
}
