using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrafficCar : MonoBehaviour
{
    public float speed = 4f;

    Rigidbody2D rb;
    MiniGameManager gm;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<MiniGameManager>();
    }

    void FixedUpdate()
    {
        if (gm != null && gm.CurrentState != MiniGameManager.State.Playing)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = Vector2.down * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gm == null) return;
        if (gm.CurrentState != MiniGameManager.State.Playing) return;

        if (collision.collider.CompareTag("Player"))

            gm.GameOver();
    }
}
