using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv2GameManager : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Camera mainCamera;
    public int BallRunOverLevelIndex = 2;
    bool objectLeft = false;
    public GameObject Blocker;
    public Camera secondaryCamera;
    public GameObject GoText;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (CheckScreenOverlap(object1, object2))
        {
            SceneManager.LoadScene(BallRunOverLevelIndex);
        }

        if (IsObjectOffScreen(object1) && !objectLeft)
        {
            objectLeft = true;
            BoxCollider Collider = Blocker.GetComponent<BoxCollider>();
            Collider.enabled = true;
            SwitchToSecondaryCamera();
            GoText.SetActive(false);
            ChangeObjectToSecondCamera();
        }
    }

    private bool CheckScreenOverlap(Transform obj1, Transform obj2)
    {
        if (mainCamera == null || obj1 == null || obj2 == null)
            return false;

        // Get screen positions
        Vector3 screenPos1 = mainCamera.WorldToScreenPoint(obj1.position);
        Vector3 screenPos2 = mainCamera.WorldToScreenPoint(obj2.position);

        float size1 = 50f;
        float size2 = 50f;

        Rect rect1 = new Rect(screenPos1.x - size1 / 2, screenPos1.y - size1 / 2, size1, size1);
        Rect rect2 = new Rect(screenPos2.x - size2 / 2, screenPos2.y - size2 / 2, size2, size2);

        return rect1.Overlaps(rect2);
    }

    private bool IsObjectOffScreen(Transform obj)
    {
        if (mainCamera == null || obj == null)
            return false;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(obj.position);

        return viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
    }

    private void SwitchToSecondaryCamera()
    {
        if (secondaryCamera == null)
        {
            Debug.LogWarning("Secondary camera is not assigned!");
            return;
        }

        mainCamera.enabled = false;
        secondaryCamera.enabled = true;

        Debug.Log("Switched to secondary camera!");
    }

    void ChangeObjectToSecondCamera()
    {
        object2 = secondaryCamera.transform;
    }
}

