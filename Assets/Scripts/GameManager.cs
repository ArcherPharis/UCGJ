using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    InputManager inputManager;

    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
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
        inputManager.DisablePlayerInput();
    }

    void HandleSlampointReached()
    {
        Debug.Log("Load the next level or whatever");
        SceneManager.LoadScene(2);
    }
}
