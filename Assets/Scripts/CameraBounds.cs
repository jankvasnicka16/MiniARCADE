using UnityEngine;

public static class CameraBounds2D
{
    public static float HalfHeight(Camera cam) => cam.orthographicSize;
    public static float HalfWidth(Camera cam) => cam.orthographicSize * cam.aspect;

    public static float Left(Camera cam) => cam.transform.position.x - HalfWidth(cam);
    public static float Right(Camera cam) => cam.transform.position.x + HalfWidth(cam);
    public static float Bottom(Camera cam) => cam.transform.position.y - HalfHeight(cam);
    public static float Top(Camera cam) => cam.transform.position.y + HalfHeight(cam);
}
