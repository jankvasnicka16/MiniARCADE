using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void Flappy()
    {
        SceneLoader.Load("Flappy");
    }

    public void Catch()
    {
        SceneLoader.Load("CatchDodge");
    }

    public void Traffic()
    {
        SceneLoader.Load("TrafficDodge");
    }
}
