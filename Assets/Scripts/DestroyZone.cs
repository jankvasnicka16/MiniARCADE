using UnityEngine;

public class AutoDestroyZone : MonoBehaviour
{
    public float margin = 1.5f;

    void Start()
    {
        var cam = Camera.main;
        transform.position = new Vector3(
            CameraBounds2D.Left(cam) - margin,
            0f,
            0f
        );
    }
}
