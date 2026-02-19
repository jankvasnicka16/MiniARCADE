using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LanePlayerSwipe : MonoBehaviour
{
    public MiniGameManager gm;

    public float[] lanesX = new float[] { -1.8f, 0f, 1.8f };
    [HideInInspector] public int laneIndex = 1;

    public float swipeThreshold = 50f;
    float startX;
    bool isTouching;

    bool lockXUntilPlay = true;

    public float laneSmoothTime = 0.08f;
    public float maxLaneSpeed = 50f;
    float xVelocity;

    public float doubleSwipeSmoothMultiplier = 0.55f;
    public float boostDuration = 0.12f;
    public float laneArriveEpsilon = 0.02f;

    float boostTimer;

    void Awake()
    {
        laneIndex = 1;
        SnapToLaneInstant();
    }

    void LateUpdate()
    {
        if (lockXUntilPlay)
        {
            transform.position = new Vector3(0f, transform.position.y, 0f);
            xVelocity = 0f;
            boostTimer = 0f;

            if (gm != null && gm.CurrentState == MiniGameManager.State.Playing)
            {
                lockXUntilPlay = false;
                isTouching = false;
            }

            return;
        }
    }

    void Update()
    {
        if (gm == null || gm.CurrentState != MiniGameManager.State.Playing)
            return;

        if (lockXUntilPlay)
            return;

        if (boostTimer > 0f)
            boostTimer -= Time.deltaTime;

        SmoothToLane();

        if (Input.GetMouseButtonDown(0))
        {
            startX = Input.mousePosition.x;
            isTouching = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isTouching) return;
            isTouching = false;

            float dx = Input.mousePosition.x - startX;
            if (Mathf.Abs(dx) > swipeThreshold)
                Move(dx > 0 ? 1 : -1);
        }
    }

    void Move(int dir)
    {
        int newIndex = Mathf.Clamp(laneIndex + dir, 0, lanesX.Length - 1);
        if (newIndex == laneIndex) return;

        if (Mathf.Abs(transform.position.x - lanesX[laneIndex]) > laneArriveEpsilon)
            boostTimer = boostDuration;

        laneIndex = newIndex;
    }

    void SmoothToLane()
    {
        float smooth = boostTimer > 0f
            ? laneSmoothTime * doubleSwipeSmoothMultiplier
            : laneSmoothTime;

        float targetX = lanesX[laneIndex];

        float newX = Mathf.SmoothDamp(
            transform.position.x,
            targetX,
            ref xVelocity,
            smooth,
            maxLaneSpeed
        );

        transform.position = new Vector3(newX, transform.position.y, 0f);
    }

    void SnapToLaneInstant()
    {
        transform.position = new Vector3(lanesX[laneIndex], transform.position.y, 0f);
        xVelocity = 0f;
    }

    public void ResetToMiddleLane()
    {
        laneIndex = 1;
        SnapToLaneInstant();
        lockXUntilPlay = true;
        isTouching = false;
    }
}
