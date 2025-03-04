using UnityEngine;

public class Spin : MonoBehaviour
{

    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Save children’s current world transforms
        Vector3[] childPositions = new Vector3[transform.childCount];
        Quaternion[] childRotations = new Quaternion[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childPositions[i] = transform.GetChild(i).position;
            childRotations[i] = transform.GetChild(i).rotation;
        }

        // Rotate only the parent
        transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);

        // Restore children’s world transforms
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = childPositions[i];
            transform.GetChild(i).rotation = childRotations[i];
        }
    }
}

