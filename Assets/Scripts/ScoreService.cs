using UnityEngine;

public static class ScoreService
{
    public static int GetBest(string key) => PlayerPrefs.GetInt(key, 0);

    public static void SaveBest(string key, int score)
    {
        int best = GetBest(key);
        if (score > best)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }
    }
}
