using UnityEngine;
using System.Collections;
using TMPro;

public class UIScoreList : MonoBehaviour
{
    public UILeaderBoardItem itemTemplate;
    public TextMeshProUGUI txtWarning;

    public int maxItems = 20;

    void Start()
    {
        txtWarning.gameObject.SetActive(true);
        txtWarning.SetText("Loading..");

        StartCoroutine(ScoreAPI.GetScore(HandleLoadedScores, HandleError));
    }

    public void HandleError(string message)
    {
        txtWarning.SetText($"Error: {message}");
    }

    public void HandleLoadedScores(ScoreData scores)
    {
        if (scores.data.Count == 0)
        {
            txtWarning.SetText("No scores yet!");
            return;
        }

        txtWarning.gameObject.SetActive(false);

        for (int i = 0; i < maxItems && i < scores.data.Count; i++)
        {
            var item = scores.data[i];

            var instance = UIUtils.CreateFromTemplate(itemTemplate);

            instance.Init(i + 1, item.name, item.score);
        }
    }

}
