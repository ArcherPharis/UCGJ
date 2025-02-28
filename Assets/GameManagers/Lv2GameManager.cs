using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv2GameManager : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Camera mainCamera;
    public int BallRunOverLevelIndex = 2;

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
}
