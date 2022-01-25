using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITextFloater : MonoBehaviour
{
    public FloatingText template;
    public Image icon;

    public float duration = 0.5f;
    public float distance = 1f;
    public float displayCooldown = 0.1f;

    Queue<FloatingTextInfo> queue = new Queue<FloatingTextInfo>();
    float nextMessage = 0f;


    public void ShowText(FloatingTextInfo info)
    {
        queue.Enqueue(info);
    }

    void ShowTextForReal(FloatingTextInfo info)
    {
        var instance = FloatingText.Create(
            template,
            info.message,
            template.transform.position,
            template.transform.parent
        );
        instance.SetColor(info.color);
        instance.lifetime = duration;
        instance.distance = distance;
        instance.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            //ShowText(new  "-1 hp", false);
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            //ShowText("+5 exp", true);
        }

        if (queue.Count == 0 || Time.time < nextMessage) return;

        ShowTextForReal(queue.Dequeue());
        nextMessage = Time.time + displayCooldown;
    }
}

[System.Serializable]
public struct FloatingTextInfo
{
    public string message;
    public Color color;

    public FloatingTextInfo(string message, bool good)
    {
        this.message = message;
        this.color = good ? Color.green : Color.red;
    }

    public FloatingTextInfo(string message, Color color)
    {
        this.message = message;
        this.color = color;
    }

    public FloatingTextInfo(string message)
    {
        this.message = message;
        this.color = Color.white;
    }


}
