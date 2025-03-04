using UnityEngine;

public class IntroDoor : MonoBehaviour, IFlashlightable
{
    MeshCollider mc;
    MeshRenderer mr;
    AudioSource audioSource;
    bool isRevealed = false;
    [SerializeField] int LevelIndex;

    private void Awake()
    {
        mc = GetComponent<MeshCollider>();
        mr = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnFlashlightHit()
    {
        if (isRevealed)
            return;

        isRevealed = true;
        mr.enabled = true;
        audioSource.Play();
        Door door = gameObject.AddComponent<Door>();
        door.SetDoorLoadIndex(LevelIndex);
        
    }
}
