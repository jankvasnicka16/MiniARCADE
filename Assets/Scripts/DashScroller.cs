using UnityEngine;

public class DashScroller : MonoBehaviour
{
    public MiniGameManager gm;

    public float speed = 4f;
    public float topY = 6.5f;
    public float bottomY = -6.5f;

    void Update()
    {
        if (gm != null && gm.CurrentState != MiniGameManager.State.Playing)
            return;

        foreach (Transform dash in transform)
        {
            Vector3 p = dash.localPosition;
            p.y -= speed * Time.deltaTime;

            if (p.y < bottomY)
                p.y = topY;

            dash.localPosition = p;
        }
    }
}
