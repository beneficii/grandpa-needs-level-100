using UnityEngine;
using System.Collections;
using TMPro;

public class UIScoreList : MonoBehaviour
{
    public UILeaderBoardItem itemTemplate;
    public TextMeshProUGUI txtWarning;


    public int maxItems = 20;

    private void Start()
    {
        txtWarning.gameObject.SetActive(false);

        var scores = ScoreCtrl.GetScores();

        foreach (var item in UIUtils.IterateFromTemplate(itemTemplate))
        {
            Destroy(item.gameObject);
        }

        if(scores.list.Count == 0)
        {
            txtWarning.SetText("No scores yet!");
            txtWarning.gameObject.SetActive(true);
            return;
        }

        for (int i = 0; i < maxItems && i < scores.list.Count; i++)
        {
            var score = scores.list[i];

            var instance = UIUtils.CreateFromTemplate(itemTemplate);

            instance.Init(i + 1, score.name, score.time);
        }
    }

}
