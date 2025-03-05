using UnityEngine;

public class SecretWall : MonoBehaviour, IFlashlightable
{
    private MovingWall[] MovingWalls;
    private bool bHasBeenHit = false;

    public GameObject DoorToSpawn;
    public Transform DoorSpawnLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MovingWalls = FindObjectsOfType<MovingWall>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFlashlightHit()
    {
        if (bHasBeenHit)
            return;

        Debug.Log("The Flashlight hit us!");
        bHasBeenHit = true;
        foreach(MovingWall wall in MovingWalls)
        {
            wall.StopWall();
        }
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager)
        {
            gameManager.audioSource.Stop();
            gameManager.PlayLight();
        }

        Instantiate(DoorToSpawn, DoorSpawnLocation);
    }
}
