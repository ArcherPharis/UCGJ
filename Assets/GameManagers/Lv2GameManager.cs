using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Lv2GameManager : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Camera mainCamera;
    public int BallRunOverLevelIndex = 2;
    public GameObject GoText;

    public RenderPipelineAsset newPipelineAsset;
    DialogueUI dialogueUI;
    [SerializeField] DialogueObject IntroDialogue;
    [SerializeField] DialogueObject CrushedDialoague;
    LevelTransitioner levelTransitioner;
    bool crushed = false;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        QualitySettings.SetQualityLevel(1);
        dialogueUI = FindFirstObjectByType<DialogueUI>();
        levelTransitioner = GetComponent<LevelTransitioner>();
    }

    private void Start()
    {
        dialogueUI.ShowDialogue(IntroDialogue);

    }

    private void Update()
    {
        if (CheckScreenOverlap(object1, object2) && !crushed)
        {
            dialogueUI.ShowDialogue(CrushedDialoague);
            levelTransitioner.StartFadein(2);
            crushed = true;
        }

    }

    private bool CheckScreenOverlap(Transform obj1, Transform obj2)
    {

        Vector3 screenPos1 = mainCamera.WorldToScreenPoint(obj1.position);
        Vector3 screenPos2 = mainCamera.WorldToScreenPoint(obj2.position);

        float size1 = 50f;
        float size2 = 385f;

        Rect rect1 = new Rect(screenPos1.x - size1 / 2, Screen.height - screenPos1.y - size1 / 2, size1, size1);
        Rect rect2 = new Rect(screenPos2.x - size2 / 2, Screen.height - screenPos2.y - size2 / 2, size2, size2);

        return rect2.Overlaps(rect1);
    }



}

