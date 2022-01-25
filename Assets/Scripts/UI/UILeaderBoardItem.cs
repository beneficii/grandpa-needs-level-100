using UnityEngine;
using System.Collections;
using TMPro;

public class UILeaderBoardItem : MonoBehaviour
{
    public TextMeshProUGUI txtPlaceName;
    public TextMeshProUGUI txtTime;


    public void Init(int place, string username, int time)
    {
        txtPlaceName.SetText($"{place}. {username}");
        txtTime.SetText($"{UITimer.GetTime(time)}");
    }
}
