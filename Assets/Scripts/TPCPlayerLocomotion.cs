using UnityEngine;

public class TPCPlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    public bool isSprinting = false;

    [Header("Movement Speeds")]
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
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

        if(isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }



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
