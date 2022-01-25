using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class UIResourceInfo : MonoBehaviour
{
    public ResourceType resource;
    public TextMeshProUGUI caption;
    public Slider slider;
    public Image icon;
    public Image indicator;

    public bool useMax = false;
    public Button skillUpButton;
    public int skillLevelValue;

    int amount;
    int max;

    float blinkUntil = -1f;

    private void Start()
    {
        ResourceCtrl.OnResourceChanged += ResourceChanged;
        ResourceCtrl.OnNotEnough += ResourceNotEnough;
        Set(Game.resources.GetInfo(resource));
        if(icon) icon.sprite = Game.images.Resource(resource);

        ResetIndicator();
        RefreshLevelUpButton();
    }

    private void OnDestroy()
    {
        ResourceCtrl.OnResourceChanged -= ResourceChanged;
        ResourceCtrl.OnNotEnough -= ResourceNotEnough;
    }

    void ResourceChanged(ResourceInfo info)
    {
        if (info.type == resource)
        {
            Set(info);
        }

        if(info.type == ResourceType.SkillPoints)
        {
            RefreshLevelUpButton();
        }
    }

    void ResourceNotEnough(ResourceType type)
    {
        if (resource == type) Blink(Color.red);
    }

    void Blink(Color color, float duration = 0.15f, float endValue = 0.9f)
    {
        if (!indicator) return;

        blinkUntil = Time.time + duration;
        indicator.color = color;
    }

    void ResetIndicator()
    {
        if (!indicator) return;

        indicator.color = new Color(0, 0, 0, 0);
        blinkUntil = -1f;
    }

    void Set(ResourceInfo info)
    {
        if(info.amount > amount)
        {
            Blink(Color.green, 0.15f, 0.4f);
        }

        amount = info.amount;
        max = info.max;

        Refresh();
    }

    public void OnLevelUpButton()
    {
        var res = Game.resources;

        if (!res.Enough(ResourceType.SkillPoints, 1)) return;
        res.Remove(ResourceType.SkillPoints, 1);
        if (useMax)
        {
            res.AddBoth(resource, skillLevelValue);
        }
        else
        {
            res.Add(resource, skillLevelValue);
        }
        Game.sounds.Play(GenericSound.SkillUp);
    }

    void RefreshLevelUpButton()
    {
        if (!skillUpButton) return;

        int pts = Game.resources.Get(ResourceType.SkillPoints);
        skillUpButton.gameObject.SetActive(pts > 0);
    }

    private void Update()
    {
        if(blinkUntil >= 0 && Time.time > blinkUntil )
        {
            ResetIndicator();
        }
    }

    void Refresh()
    {
        bool hasMax = max > 0;
        
        if (slider && hasMax)
        {
            slider.maxValue = max;
            slider.value = amount;
        }

        if (caption)
        {
            if (useMax)
            {
                caption.SetText($"{max}");
            }
            else if (hasMax)
            {
                caption.SetText($"{amount}/{max}");
            }
            else
            {
                caption.SetText($"{amount}");
            }
        }
    }
}
