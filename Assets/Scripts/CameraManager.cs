using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class CameraManager : MonoBehaviour
{
    public Transform cameraPivot;

    public float cameraLookSpeed = 0.2f;
    public float cameraPivotSpeed = 0.2f;

    public float lookAngle; //Up/Down
    public float pivotAngle; //left/right
    //public float minimumHorizontalLook = -90;
    //public float maximumHorizontalLook = 90;
    public float minimumVerticalLook = -35;
    public float maximumVerticalLook = 35;
    public bool bShouldMoveCamera = true;

    InputManager inputManager;

    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
    }

    public void HandleAllCameraMovement()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (!bShouldMoveCamera)
            return;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumVerticalLook, maximumVerticalLook);
        //lookAngle = Mathf.Clamp(lookAngle, minimumHorizontalLook, maximumHorizontalLook);


        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion TargetRotation = Quaternion.Euler(rotation);
        transform.rotation = TargetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        TargetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = TargetRotation;
    }
}
