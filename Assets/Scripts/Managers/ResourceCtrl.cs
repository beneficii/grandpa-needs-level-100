using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResourceCtrl : MonoBehaviour
{
    public static event System.Action<ResourceInfo> OnResourceChanged;
    public static event System.Action<ResourceInfo> OnResourceDelta;
    public static event System.Action<ResourceInfo> OnMaxReached;
    public static event System.Action<ResourceType> OnNotEnough;

    public List<ResourceInfo> startingResources;
    Dictionary<ResourceType, int> resources;
    Dictionary<ResourceType, int> maxValues;

    private void Awake()
    {
        Game.resources = this;
        resources = new Dictionary<ResourceType, int>();
        maxValues = new Dictionary<ResourceType, int>();
        foreach (var item in EnumUtil.GetValues<ResourceType>())
        {
            resources.Add(item, 0);
            maxValues.Add(item, 0); //means no max
        }

        ResetResources();
    }

    void ResetResources()
    {
        foreach (var item in startingResources)
        {
            Set(item);
        }
    }

    public int Get(ResourceType type)
    {
        if(resources !=null && resources.TryGetValue(type, out var value))
        {
            return value;
        }

        return 0;
    }

    public ResourceInfo GetInfo(ResourceType type)
    {
        return new ResourceInfo
        {
            type = type,
            amount = resources[type],
            max = maxValues[type]
        };
    }

    public int GetMax(ResourceType type)
    {
        if (maxValues != null && maxValues.TryGetValue(type, out var value))
        {
            return value;
        }

        return 0;
    }

    private void Set(ResourceType type, int amount)
    {
        amount = Mathf.Max(amount, 0);
        resources[type] = amount;
        ResourceChanged(type);
    }

    private void SetMax(ResourceType type, int amount)
    {
        maxValues[type] = amount;
        ResourceChanged(type);
    }

    private void Set(ResourceInfo info)
    {
        resources[info.type] = info.amount;
        maxValues[info.type] = info.max;
        ResourceChanged(info.type);
    }

    void ResourceChanged(ResourceType type)
    {
        var info = GetInfo(type);

        if (info.max > 0 && info.amount >= info.max)
        {
            resources[info.type] = info.max;
            OnMaxReached?.Invoke(info);
        }

        OnResourceChanged?.Invoke(GetInfo(type));
    }

    public void Add(ResourceType type, int amount)
    {
        OnResourceDelta?.Invoke(new ResourceInfo(type, amount));
        Set(type, Get(type) + amount);
    }

    public bool Remove(ResourceType type, int amount)
    {
        /*
        if(!Enough(type, amount, true))
        {
            return false;
        }*/

        OnResourceDelta?.Invoke(new ResourceInfo(type, -amount));
        Set(type, Get(type) - amount);
        return true;
    }

    public void AddMax(ResourceType type, int value, bool resetAmount)
    {
        var info = GetInfo(type);

        if(resetAmount)
        {
            info.amount = 0;
        }

        OnResourceDelta?.Invoke(new ResourceInfo(type, 0, value));

        info.max += value;

        Set(info);
    }

    public void AddBoth(ResourceType type, int value)
    {
        var info = GetInfo(type);
        OnResourceDelta?.Invoke(new ResourceInfo(type, value, value));

        info.max += value;
        info.amount += value;

        Set(info);
    }

    public bool Enough(ResourceType type, int amount, bool indicate = false)
    {
        var result = Get(type) >= amount;
        if(indicate && !result)
        {
            OnNotEnough?.Invoke(type);
        }
        return result;
    }

    private void OnEnable()
    {
        //LevelData.OnLoad += OnLevelLoaded;
    }

    private void OnDisable()
    {
        //LevelData.OnLoad -= OnLevelLoaded;
    }

    void OnLevelLoaded(string data)
    {
        ResetResources();
    }
}

public enum ResourceType
{
    Experience,
    Level,
    SkillPoints,
    Health,
    Damage,
    Energy, //speed
}

[System.Serializable]
public struct ResourceInfo
{
    public ResourceType type;
    public int amount;
    public int max;

    public ResourceInfo(ResourceType type, int amount) : this()
    {
        this.type = type;
        this.amount = amount;
    }

    public ResourceInfo(ResourceType type, int amount, int max) : this(type, amount)
    {
        this.max = max;
    }
}


public static class EnumUtil
{
    public static IEnumerable<T> GetValues<T>()
    {
        return System.Enum.GetValues(typeof(T)).Cast<T>();
    }
}