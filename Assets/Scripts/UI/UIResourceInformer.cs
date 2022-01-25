using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourceInformer : MonoBehaviour
{
    UITextFloater floater;

    public Color plusColor = Color.green;
    public Color minusColor = Color.red;
    public Color neutralColor = Color.white;
    public Color expColor = Color.white;

    private void OnEnable()
    {
        ResourceCtrl.OnResourceDelta += ResourceChanged;
        floater = GetComponent<UITextFloater>();
    }

    private void OnDisable()
    {
        ResourceCtrl.OnResourceDelta -= ResourceChanged;
    }

    void ResourceChanged(ResourceInfo info)
    {
        switch (info.type)
        {
            case ResourceType.Experience:
                AddDelta(info.amount, "exp", expColor);
                break;
            case ResourceType.Level:
                AddMessage("Level up!", expColor, info.amount);
                break;
            case ResourceType.SkillPoints:
                break;
            case ResourceType.Health:
                if(info.max > 0)
                {
                    AddDelta(info.amount, "max hp");
                }
                else
                {
                    AddDelta(info.amount, "hp");
                }
                break;
            case ResourceType.Damage:
                AddDelta(info.amount, "damage", neutralColor);
                break;
            case ResourceType.Energy:
                AddDelta(info.amount, "speed", neutralColor);
                break;
            default:
                break;
        }


        if (info.type == ResourceType.Level)
        {
            if (info.amount <= 0)
            {
                Game.Defeat();
            }
        }
    }

    void AddDelta(int value, string msg)
    {
        if (value == 0) return;
        floater.ShowText(new FloatingTextInfo
        {
            message = $"{value:+#;-#} {msg}",
            color = value > 0 ? plusColor : minusColor
        });

    }

    void AddDelta(int value, string msg, Color color)
    {
        if (value == 0) return;
        floater.ShowText(new FloatingTextInfo
        {
            message = $"{value:+#;-#} {msg}",
            color = color
        });

    }

    void AddMessage(string msg, int times = 1)
    {
        var info = new FloatingTextInfo(msg, neutralColor);
        for (int i = 0; i < times; i++)
        {
            floater.ShowText(info);
        }
    }

    void AddMessage(string msg, Color color, int times = 1)
    {
        var info = new FloatingTextInfo(msg, color);
        for (int i = 0; i < times; i++)
        {
            floater.ShowText(info);
        }
    }
}
