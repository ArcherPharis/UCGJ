using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    InputManager playerInput;
    [SerializeField] private EndlessHallway endlessHallway;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject door;
    [SerializeField] private float revealDistanceThreshold = 10f;
    [SerializeField] private float tooCloseDistance = 3f;
    [SerializeField] private float revealDelay = 1.5f;
    [SerializeField] private bool doorRevealed = false;
    private Coroutine revealCoroutine;
    private Coroutine lightCoroutine;

    [SerializeField] Light Light;
    [SerializeField] private float lightTransitionSpeed = 2f;

    [SerializeField] private float minIntensity = 0f;
    [SerializeField] private float maxIntensity = 5f;

    private void Awake()
    {
        playerInput = FindFirstObjectByType<InputManager>();
    }

    private void HandleDoorRevealLogic()
    {
        float playerDistance = Vector3.Distance(player.position, door.transform.position);

        if (playerInput.shiftInput)
        {
    
            if (revealCoroutine != null)
            {
                StopCoroutine(revealCoroutine);
                revealCoroutine = null;
            }
            HideDoor();
        }
        else
        {
            if (!doorRevealed && playerDistance >= revealDistanceThreshold)
            {
                if (revealCoroutine == null)
                {
                    revealCoroutine = StartCoroutine(DelayedReveal());
                }
            }
        }
    }

    private IEnumerator DelayedReveal()
    {
        yield return new WaitForSeconds(revealDelay); // Wait before revealing the door

        float playerDistance = Vector3.Distance(player.position, door.transform.position);

        if (!playerInput.shiftInput && playerDistance >= revealDistanceThreshold)
        {
            RevealDoor();
        }

        revealCoroutine = null;
    }

    private void RevealDoor()
    {
        doorRevealed = true;
        endlessHallway.DisableResetBox();
        StartLightTransition(maxIntensity);

        ToggleDoorColliders(true);
    }

    private void HideDoor()
    {
        doorRevealed = false;
        endlessHallway.EnableResetBox();
        StartLightTransition(minIntensity);
        ToggleDoorColliders(false);

    }


    void OnEnable()
    {
        MovingWall.OnWallMidpointReached += HandleWallMidpointReached;
        MovingWall.OnWallSlamReached += HandleSlampointReached;
    }

    void OnDisable()
    {
        MovingWall.OnWallMidpointReached -= HandleWallMidpointReached;
        MovingWall.OnWallSlamReached -= HandleWallMidpointReached;

    }

    void HandleWallMidpointReached()
    {
        Debug.Log("A wall has reached the (|0.5|) point!");
        playerInput.DisablePlayerInput();
    }

    void HandleSlampointReached()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        HandleDoorRevealLogic();
    }

    private void StartLightTransition(float targetIntensity)
    {
        if (lightCoroutine != null)
        {
            StopCoroutine(lightCoroutine);
        }
        lightCoroutine = StartCoroutine(SmoothLightTransition(targetIntensity));
    }

    private IEnumerator SmoothLightTransition(float targetIntensity)
    {
        float startIntensity = Light.intensity;
        float elapsedTime = 0f;

        while (Mathf.Abs(Light.intensity - targetIntensity) > 0.01f)
        {
            elapsedTime += Time.deltaTime * lightTransitionSpeed;
            Light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime);
            yield return null;
        }

        Light.intensity = targetIntensity;
        lightCoroutine = null;
    }

    private void ToggleDoorColliders(bool enable)
    {
        MeshCollider[] colliders = door.GetComponentsInChildren<MeshCollider>();

        foreach (MeshCollider collider in colliders)
        {
            collider.enabled = enable;
        }
    }
}
