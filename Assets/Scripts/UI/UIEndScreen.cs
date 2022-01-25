using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEndScreen : MonoBehaviour
{
    public GameObject victory;
    public GameObject defeat;
    public TextMeshProUGUI time;

    private void OnEnable()
    {
        Game.OnGameOver += GameOver;
        Game.OnLevelStarted += LevelStarted;
    }

    private void OnDisable()
    {
        Game.OnGameOver -= GameOver;
        Game.OnLevelStarted -= LevelStarted;
    }

    void GameOver(GameOverParams param)
    {
        time.SetText($"Time: {UITimer.GetTime()}");
        StartCoroutine(EndRoutine(param.win));
    }

    IEnumerator EndRoutine(bool win)
    {
        yield return new WaitForSeconds(2f);
        victory.SetActive(win);
        defeat.SetActive(!win);
        time.gameObject.SetActive(true);
    }

    void LevelStarted()
    {
        victory.SetActive(false);
        defeat.SetActive(false);
        time.gameObject.SetActive(false);
    }
}
