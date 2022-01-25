using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CooldownComponent
{
    public event System.Action OnChanged;

    float next;
    float randomrange = 0f;
    float cooldown;
    public float Cooldown
    {
        get => cooldown;
        set
        {
            float delta = cooldown - value;
            next += delta;
            cooldown = value;

            OnChanged?.Invoke();
        }
    }

    void SetNext()
    {
        next = Time.time + cooldown + Random.Range(0, randomrange);
    }

    public CooldownComponent(float cooldown, float randomrange = 0f)
    {
        this.cooldown = cooldown;
        this.randomrange = randomrange;
        SetNext();
    }

    public bool CanUse => Time.time >= next;

    public bool Use()
    {
        if(CanUse)
        {
            SetNext();
            return true;
        }

        return false;
    }

    public void Pause(bool value = true)
    {
        if(value)
        {
            next = float.MaxValue;
        }
        else
        {
            next = Time.time + cooldown;
        }
    }

    public void Restore()
    {
        next = 0f;
    }
}
