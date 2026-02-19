using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerMoveX : MonoBehaviour
{
    public MiniGameManager gm;

    [Header("Drag")]
    public float margin = 0.6f;        
    public float dragSensitivity = 1f;  

    [Header("Keyboard (optional)")]
    public bool allowKeyboard = true;
    public float keyboardSpeed = 8f;

    Camera cam;
    bool dragging;
    float dragOffsetX; 

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (gm.CurrentState != MiniGameManager.State.Playing) return;

        if (allowKeyboard && !Input.GetMouseButton(0))
        {
            float input = Input.GetAxis("Horizontal");
            if (Mathf.Abs(input) > 0.001f)
            {
                float x = transform.position.x + input * keyboardSpeed * Time.deltaTime;
                SetXClamped(x);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point = new Vector2(wp.x, wp.y);

            Collider2D hit = Physics2D.OverlapPoint(point);
            if (hit != null && hit.gameObject == gameObject)
            {
                dragging = true;

                dragOffsetX = transform.position.x - point.x;
            }
        }

        if (dragging && Input.GetMouseButton(0))
        {
            Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
            float targetX = wp.x + dragOffsetX;

            float newX = Mathf.Lerp(transform.position.x, targetX, dragSensitivity);

            SetXClamped(newX);
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }

    void SetXClamped(float x)
    {
        x = Mathf.Clamp(
            x,
            CameraBounds2D.Left(cam) + margin,
            CameraBounds2D.Right(cam) - margin
        );

        transform.position = new Vector3(x, transform.position.y, 0f);
    }
}
