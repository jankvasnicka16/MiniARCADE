using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    [Header("SFX")]
    public AudioClip click;
    public AudioClip score;
    public AudioClip gameOver;

    AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayClick()
    {
        if (click) sfxSource.PlayOneShot(click);
    }

    public void PlayScore()
    {
        if (score) sfxSource.PlayOneShot(score);
    }

    public void PlayGameOver()
    {
        if (gameOver) sfxSource.PlayOneShot(gameOver);
    }
}
