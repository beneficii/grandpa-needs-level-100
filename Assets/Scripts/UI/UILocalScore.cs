using UnityEngine;
using System.Collections;
using TMPro;

public class UILocalScore : MonoBehaviour
{
    private void Start()
    {
        var label = GetComponent<TextMeshProUGUI>();

        var score = ScoreCtrl.GetLocalScore();
        if(score < 1)
        {
            label.SetText("Not set");
        }
        else
        {
            label.SetText($"{UITimer.GetTime(score)}");
        }
    }
}
