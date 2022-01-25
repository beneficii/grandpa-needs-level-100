using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class HealthComponent
{
    public System.Action OnDeath;

    public TextMeshPro txtHealth;
    public Slider slider;
    bool dead = false;

    [SerializeField] int maxHp = 10;
    [SerializeField] int hp = 10;

    public int Health
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            Refresh();
        }
    }

    public bool IsAlive => !dead;

    public void Init() => Init(maxHp);

    public void Init(int max)
    {
        SetMax(max, false);
        Health = max;
    }

    public void SetMax(int value, bool refresh = true)
    {
        if (slider) slider.maxValue = value;
        maxHp = value;
        if(refresh) Refresh();
    }

    public int Remove(int amount)
    {
        int old = Health;
        Health -= amount;

        return old - Health;
    }

    public int Add(int amount)
    {
        int old = Health;
        Health += amount;

        return Health - old;
    }

    public void Refresh()
    {
        txtHealth?.SetText($"{hp}");
        if (slider) slider.value = hp;
        if(!dead && hp == 0)
        {
            dead = true;
            OnDeath?.Invoke();
        }
    }

    public static implicit operator int(HealthComponent d) => d.Health;

    // Only use for += 
    public static HealthComponent operator +(HealthComponent a, int value)
    {
        a.Health += value;
        return a;
    }

    // Only use for -= 
    public static HealthComponent operator -(HealthComponent a, int value)
    {
        a.Health -= value;
        return a;
    }
}
