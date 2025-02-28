using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class CameraManager : MonoBehaviour
{
    public Transform cameraPivot;
    public Transform cameraTransform; // Reference to the actual camera

    public float cameraLookSpeed = 0.2f;
    public float cameraPivotSpeed = 0.2f;

    public float lookAngle; // Left/Right
    public float pivotAngle; // Up/Down
    public float minimumVerticalLook = -35;
    public float maximumVerticalLook = 35;
    public bool bShouldMoveCamera = true;
    public bool useCameraBoom = false; // Toggle for boom functionality

    InputManager inputManager;

    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();

        if (cameraTransform != null && cameraPivot != null)
        {
            cameraTransform.SetParent(cameraPivot); // Ensure the camera is a child of the pivot
        }
    }

    public void HandleAllCameraMovement()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (!bShouldMoveCamera)
            return;

        lookAngle += inputManager.cameraInputX * cameraLookSpeed;
        pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumVerticalLook, maximumVerticalLook);

        if (useCameraBoom)
        {
            // Rotate the pivot itself, so the camera follows naturally
            Quaternion pivotRotation = Quaternion.Euler(pivotAngle, lookAngle, 0);
            cameraPivot.rotation = pivotRotation;
        }
        else
        {
            // Original behavior: Rotate the camera and pivot separately
            Quaternion horizontalRotation = Quaternion.Euler(0, lookAngle, 0);
            transform.rotation = horizontalRotation;

            Quaternion verticalRotation = Quaternion.Euler(pivotAngle, 0, 0);
            cameraPivot.localRotation = verticalRotation;
        }
    }
}