using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    public MiniGameManager gm;
    public float jumpForce = 6f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gm.CurrentState != MiniGameManager.State.Playing)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero; 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gm.CurrentState != MiniGameManager.State.Playing)
            return;

        gm.GameOver();
    }
}
