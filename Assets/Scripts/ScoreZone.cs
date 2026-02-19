using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    MiniGameManager gm;
    bool scored;

    void Awake()
    {
        gm = FindObjectOfType<MiniGameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (scored) return;
        if (gm == null) return;
        if (gm.CurrentState != MiniGameManager.State.Playing) return;

        if (other.CompareTag("Player"))
        {
            scored = true;
            gm.AddScore(1);
        }
    }
}
