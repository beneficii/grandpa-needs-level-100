using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UILeaderBoardItem : MonoBehaviour
{
    public TextMeshProUGUI txtPlaceName;
    public TextMeshProUGUI txtTime;
    public Image render;


    public void Init(int place, string username, int time)
    {
        txtPlaceName.SetText($"{place}. {username}");
        txtTime.SetText($"{UITimer.GetTime(time)}");
        render.enabled = place % 2 == 1;
    }
}
