using UnityEngine;

public interface IFlashlightable
{
   public void OnFlashlightHit();
}

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float Range = 10f;
    [SerializeField] private Transform FlashlightTransform;

    private void Update()
    {
        if (Input.GetMouseButton(0)) // Example trigger (Left Mouse Click)
        {
            CastFlashlightRay();
        }
    }

    public void CastFlashlightRay()
    {
        Ray ray = new Ray(FlashlightTransform.position, FlashlightTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit HitInfo, Range))
        {
            if (HitInfo.collider.gameObject.TryGetComponent(out IFlashlightable Flashlightable))
            {
                Flashlightable.OnFlashlightHit();
            }
        }
    }
}
