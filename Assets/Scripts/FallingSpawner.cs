using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    public MiniGameManager gm;
    public GameObject goodPrefab;
    public GameObject badPrefab;

    public float spawnInterval = 1.2f;
    public float badChance = 0.25f;
    public float spawnMargin = 0.5f;

    float timer;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (gm.CurrentState != MiniGameManager.State.Playing) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;

            float x = Random.Range(
                CameraBounds2D.Left(cam) + spawnMargin,
                CameraBounds2D.Right(cam) - spawnMargin
            );

            float y = CameraBounds2D.Top(cam) + 1f;

            GameObject prefab =
                Random.value < badChance ? badPrefab : goodPrefab;

            Instantiate(prefab, new Vector3(x, y, 0f), Quaternion.identity);
        }
    }
}
