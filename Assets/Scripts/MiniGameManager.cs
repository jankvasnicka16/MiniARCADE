using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public enum State { StartMenu, Playing, GameOver }

    [Header("Config")]
    public bool skipStartMenu = true;
    public string bestKey = "BEST_GAME";

    [Header("Canvases")]
    public GameObject startMenuCanvas;
    public GameObject hudCanvas;
    public GameObject gameOverCanvas;

    [Header("Texts")]
    public TMP_Text bestTextStartMenu;
    public TMP_Text scoreTextHUD;
    public TMP_Text finalScoreText;
    public TMP_Text bestTextGameOver;

    public enum MiniGameType { Generic, Traffic, Catch }
    public MiniGameType miniGameType = MiniGameType.Generic;


    public State CurrentState { get; private set; }

    int score;
    bool over;

    void Start()
    {
        Time.timeScale = 1f;
        over = false;
        SetScore(0);
        RefreshBestTexts();

        if (PlayerPrefs.GetInt("SKIP_START_ONCE", 0) == 1)
        {
            PlayerPrefs.DeleteKey("SKIP_START_ONCE");

            if (startMenuCanvas)
                startMenuCanvas.SetActive(false);

            SetState(State.Playing);

            var swipe = FindObjectOfType<LanePlayerSwipe>();
            if (swipe) swipe.ResetToMiddleLane();

            var spawner = FindObjectOfType<TrafficSpawner>();
            if (spawner) spawner.ResetSpawner();
        }
        else
        {
            SetState(State.StartMenu);
        }
    }


    public void Restart()
    {
        PlayerPrefs.SetInt("SKIP_START_ONCE", 1);
        PlayerPrefs.Save();
        SceneLoader.Load(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    public void StartGame()
    {
        over = false;
        SetScore(0);
        SetState(State.Playing);
    }

    public void StartGameTraffic()
    {
        over = false;
        SetScore(0);

        foreach (var car in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(car);

        var spawner = FindObjectOfType<TrafficSpawner>();
        if (spawner) spawner.ResetSpawner();

        var player = FindObjectOfType<LanePlayerSwipe>(true);
        if (player)
            player.ResetToMiddleLane();   

        SetState(State.Playing);
    }




    public void StartGameCatch()
    {
        over = false;
        SetScore(0);

        foreach (var go in GameObject.FindGameObjectsWithTag("Bad"))
            Destroy(go);

        foreach (var go in GameObject.FindGameObjectsWithTag("Good"))
            Destroy(go);

         var player = GameObject.FindGameObjectWithTag("Player");
    if (player)
        player.transform.position = new Vector3(0f, -1.72f, 0f);

        SetState(State.Playing);
    }



    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
    }

    public void AddScore(int amount)
    {
        if (over || CurrentState != State.Playing) return;
        SetScore(score + amount);

        if (SoundManager.Instance)
            SoundManager.Instance.PlayScore();
    }

    public void GameOver()
    {
        if (over) return;
        over = true;

        if (Application.isMobilePlatform)
            Handheld.Vibrate();

        if (SoundManager.Instance)
            SoundManager.Instance.PlayGameOver();

        ScoreService.SaveBest(bestKey, score);

        if (finalScoreText)
            finalScoreText.text = "Score: " + score;

        RefreshBestTexts();
        SetState(State.GameOver);
    }

    void SetScore(int newScore)
    {
        score = newScore;
        if (scoreTextHUD)
            scoreTextHUD.text = score.ToString();
    }

    void RefreshBestTexts()
    {
        int best = ScoreService.GetBest(bestKey);
        if (bestTextStartMenu)
            bestTextStartMenu.text = "Best: " + best;
        if (bestTextGameOver)
            bestTextGameOver.text = "Best: " + best;
    }

    void SetState(State s)
    {
        CurrentState = s;

        if (startMenuCanvas)
            startMenuCanvas.SetActive(s == State.StartMenu);

        if (hudCanvas)
            hudCanvas.SetActive(s == State.Playing);

        if (gameOverCanvas)
            gameOverCanvas.SetActive(s == State.GameOver);

        Time.timeScale = (s == State.Playing) ? 1f : 0f;
    }
}
