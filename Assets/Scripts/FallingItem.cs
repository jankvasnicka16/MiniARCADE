using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public MiniGameManager gm;
    public bool isGood = true;

    void Start()
    {
        gm = FindObjectOfType<MiniGameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isGood)
                gm.AddScore(1);
            else
                gm.GameOver();

            Destroy(gameObject);
        }
    }
}
