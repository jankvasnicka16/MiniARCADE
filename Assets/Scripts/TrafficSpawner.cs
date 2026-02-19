using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    public MiniGameManager gm;
    public GameObject enemyPrefab;

    public float[] lanesX = new float[] { -1.8f, 0f, 1.8f };

    public float spawnInterval = 1.2f;
    public float spawnMarginTop = 1.2f;

    public float enemySpeed = 4f;

    float timer;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (gm.CurrentState != MiniGameManager.State.Playing) return;
        if (enemyPrefab == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;

            float x = lanesX[Random.Range(0, lanesX.Length)];
            float y = CameraBounds2D.Top(cam) + spawnMarginTop;

            var go = Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);

            var car = go.GetComponent<TrafficCar>();
            if (car) car.speed = enemySpeed;
        }
    }
    public void ResetSpawner()
    {
        timer = 0f;
    }

}
