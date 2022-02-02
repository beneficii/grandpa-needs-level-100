using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreCtrl : MonoBehaviour
{
    const string prefsString = "score_prefs";
    const string localPrefs = "local_score";
    const string onlineName = "granpa_needs_level100_v1";
    public static int lastScore = 0;

    /*
    public static IEnumerator GetOnlineScore()
    {
        if (lastScore == 0) return;
        SetLocalScore(lastScore);
        var scores = GetScores();
        scores.list.Add(new Score { name = nickname, time = lastScore });
        var json = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString(prefsString, json);
    }

    public static IEnumerator SendOnlineScore(string nickname, int time)
    {
        if (lastScore == 0) return;
        SetLocalScore(lastScore);
        var scores = GetScores();
        scores.list.Add(new Score { name = nickname, time = lastScore });
        var json = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString(prefsString, json);
    }*/

    public static IEnumerator SendScore(string nickname)
    {
        if (lastScore == 0) yield break;

        SetLocalScore(lastScore);
        var scores = GetScores();
        scores.list.Add(new Score { name = nickname, time = lastScore });
        var json = JsonUtility.ToJson(scores);
        
        PlayerPrefs.SetString(prefsString, json);

        yield return ScoreAPI.PostScore(nickname, lastScore);
    }

    public static Scores GetScores()
    {
        var json = PlayerPrefs.GetString(prefsString, "");

        var scores = JsonUtility.FromJson<Scores>(json);
        if(scores == null)
        {
            scores = new Scores();
        }

        if(scores.list == null)
        {
            scores.list = new List<Score>();
        }

        scores.list.Sort((a, b) => a.time - b.time);

        return scores;
    }

    static void SetLocalScore(int score)
    {
        int best = GetLocalScore();
        if(best == 0 || score < best)
        {
            PlayerPrefs.SetInt(localPrefs, score);
        }
    }

    public static int GetLocalScore()
    {
        return PlayerPrefs.GetInt(localPrefs, 0);
    }

    private void OnEnable()
    {
        Game.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= GameOver;
    }

    void GameOver(GameOverParams param)
    {
        if(param.win)
        {
            lastScore = (int)(Time.time - Game.levelStartTime);
        }
    }
}

[System.Serializable]
public class Score
{
    public string name;
    public int time;
}

[System.Serializable]
public class Scores
{
    public int maxScores = 100;
    public List<Score> list;
}
