using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TMP_Text txt;
    public float lifetime = 0.6f;
    public float distance = 0.4f;

    public Color defaultColor = Color.white;
    public Color goodColor = Color.white;
    public Color badColor = Color.white;

    float startTime = 0f;
    Vector3 startPosition;

    //buffer stuff
    Color color;

    private void Start()
    {
        startTime = Time.time;
        startPosition = transform.localPosition;
    }

    public void SetColor(Color color)
    {
        txt.color = defaultColor;
        this.color = color;
    }

    public static string Colorize(string text, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";
    }

    // Use it from prefab
    public FloatingText Create(string msg, Vector3 position, Transform parent = null)
    {
        return Create(this, msg, position, parent);
    }

    public static FloatingText Create(FloatingText prefab, string msg, Vector3 position, Transform parent = null)
    {
        var instance = Instantiate(prefab, position, Quaternion.identity, parent);
        instance.txt.SetText(msg);
        instance.SetColor(instance.defaultColor);
        return instance;
    }

    public static FloatingText Create(FloatingText prefab, int value, Vector3 position, Transform parent = null)
    {
        return Create(prefab, value.ToString(), position, parent);
    }

    private void Update()
    {
        float delta = (Time.time - startTime)/lifetime;

        if (delta > 1f)
        {
            Destroy(gameObject);
            return;
        }

        color.a = 1 - Easings.InCubic(delta);
        txt.color = color;
        transform.localPosition = startPosition + Vector3.up * Easings.OutCubic(delta) * distance;
    }
}
