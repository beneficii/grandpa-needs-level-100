using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ScoreAPI
{
    //const string server = "http://127.0.0.1:8000";
    const string server = "https://fancyreckless.opalstacked.com";
    const string gameName = "grandpa_needs_level_100";

    static string GetUrl()
    {
        return $"{server}/scoreapi/get/{gameName}/";
    }

    static string PostUrl()
    {
        
        return $"{server}/scoreapi/add/";
    }

    public static IEnumerator GetScore(System.Action<ScoreData> OnSuccess, System.Action<string> OnError = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(GetUrl()))
        {
            yield return webRequest.SendWebRequest();

            if(webRequest.result != UnityWebRequest.Result.Success)
            {
                OnError?.Invoke(webRequest.error);
                yield break;
            }

            string json = webRequest.downloadHandler.text;
            //Debug.Log(json);
            OnSuccess?.Invoke(JsonUtility.FromJson<ScoreData>(json));
        }
    }

    public static IEnumerator PostScore(string myName, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("game", gameName);
        form.AddField("name", myName);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(PostUrl(), form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                yield break;
            }

            //Debug.Log("Form upload complete! " + www.downloadHandler.text);
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public List<ScoreItem> data;
}

[System.Serializable]
public class ScoreItem
{
    public string name;
    public int score;
}

