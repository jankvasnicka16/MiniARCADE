using System.Collections;
using UnityEngine;

public class IntroLoader : MonoBehaviour
{
    public float seconds = 2f;

    void Start()
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(seconds);
        SceneLoader.Load("MainMenu");
    }
}
