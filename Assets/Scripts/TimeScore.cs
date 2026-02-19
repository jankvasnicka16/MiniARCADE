using UnityEngine;

public class TimeScore : MonoBehaviour
{
    public MiniGameManager gm;
    public float pointsPerSecond = 1f;

    float acc;

    void Update()
    {
        if (gm.CurrentState != MiniGameManager.State.Playing) return;

        acc += Time.deltaTime * pointsPerSecond;
        if (acc >= 1f)
        {
            int add = Mathf.FloorToInt(acc);
            acc -= add;
            gm.AddScore(add);
        }
    }
}
