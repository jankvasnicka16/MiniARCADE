using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public MiniGameManager gm;
    public GameObject pipePrefab;

    public float spawnInterval = 1.5f;
    public float minY = -1.5f;
    public float maxY = 1.5f;
    public float margin = 1.0f;

    float timer;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (gm == null || pipePrefab == null) return;
        if (gm.CurrentState != MiniGameManager.State.Playing) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            float y = Random.Range(minY, maxY);
            float spawnX = CameraBounds2D.Right(cam) + margin;
            Instantiate(pipePrefab, new Vector3(spawnX, y, 0f), Quaternion.identity);
        }
    }
}
