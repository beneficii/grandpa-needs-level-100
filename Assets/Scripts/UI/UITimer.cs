using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    TextMeshProUGUI txtTimer;

    private void Awake()
    {
        txtTimer = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        

        txtTimer.SetText(GetTime());
    }

    public static string GetTime()
    {
        float time = 0;
        if (Game.levelStartTime >= 0)
        {
            time = Time.time - Game.levelStartTime;
        }

        return GetTime((int)time);
    }

    public static string GetTime(int time)
    {
        var timespan = System.TimeSpan.FromSeconds(time);
        return $"{timespan.Minutes:00}:{timespan.Seconds:00}";
    }
}
