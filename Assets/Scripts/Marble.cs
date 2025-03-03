using UnityEngine;

public class Marble : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjustable from Inspector
    private bool isRolling = true;

    private void Update()
    {
        if (isRolling)
        {
            RollMarble();
        }
    }

    private void RollMarble()
    {
        float movement = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 0);
        float rotationAngle = (-movement / (2 * Mathf.PI * transform.localScale.y)) * 360;
        transform.Rotate(Vector3.forward, rotationAngle, Space.Self);
    }

    public void StopMarble()
    {
        isRolling = false;
    }
}
