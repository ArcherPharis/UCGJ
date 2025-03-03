using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class CameraManager : MonoBehaviour
{
    public Transform cameraPivot;
    public Transform cameraTransform;

    public float cameraLookSpeed = 0.2f;
    public float cameraPivotSpeed = 0.2f;

    public float lookAngle; // Left/Right
    public float pivotAngle; // Up/Down
    public float minimumVerticalLook = -35;
    public float maximumVerticalLook = 35;
    public float minimumHorizontalLook = 0;
    public float maximumHorizontalLook = 35;
    public bool bShouldMoveCamera = true;
    public bool useCameraBoom = false;

    InputManager inputManager;

    [SerializeField] Transform targetTransform;
    private Vector3 initialOffset;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    float cameraFollowSpeed = 0.2f;

    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        if(targetTransform)
        {
            initialOffset = transform.position - targetTransform.position;

        }
        if (cameraTransform != null && cameraPivot != null)
        {
            cameraTransform.SetParent(cameraPivot);
        }
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = targetTransform.position + initialOffset;

        // Smoothly transition the camera position with some lag
        transform.position = Vector3.SmoothDamp(
            transform.position,   // Current position
            targetPosition,       // Target position
            ref cameraFollowVelocity,   // Reference velocity (SmoothDamp uses this)
            cameraFollowSpeed     // The smooth time (higher = more lag)
        );
    }

    public void HandleAllCameraMovement()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (!bShouldMoveCamera)
            return;

        lookAngle -= inputManager.cameraInputX * cameraLookSpeed;
        lookAngle = Mathf.Clamp(lookAngle, minimumHorizontalLook, maximumHorizontalLook);
        //pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed;
        //pivotAngle = Mathf.Clamp(pivotAngle, minimumVerticalLook, maximumVerticalLook);

        if (useCameraBoom)
        {
            Quaternion pivotRotation = Quaternion.Euler(pivotAngle, lookAngle, 0);
            cameraPivot.rotation = pivotRotation;
        }
        else
        {
            Quaternion horizontalRotation = Quaternion.Euler(0, lookAngle, 0);
            transform.rotation = horizontalRotation;

            Quaternion verticalRotation = Quaternion.Euler(pivotAngle, 0, 0);
            cameraPivot.localRotation = verticalRotation;
        }
    }
}