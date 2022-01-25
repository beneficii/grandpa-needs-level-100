using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ImageCtrl : MonoBehaviour
{
    [Header("Resources")]
    public List<ResourceIcon> resources;
    Dictionary<ResourceType, Sprite> resourceDict;

    [Header("Enemies")]
    public List<Sprite> enemyLevels;
    public Sprite enemyImortal;
    [Header("Pickups")]
    public List<Sprite> weaponLevels;
    [Header("Units")]
    public List<Sprite> unitLevels;

    [Header("Colors")]
    public Color colorUnit;
    public Color colorEnemy;
    public Color colorSpikes;

    private void Awake()
    {
        Game.images = this;
        resourceDict = resources.ToDictionary(x => x.resource, x => x.icon);
    }

    public Sprite Resource(ResourceType type)
    {
        if(resourceDict.TryGetValue(type, out var sprite))
        {
            return sprite;
        }

        return null;
    }

    public Sprite GetEnemyLevel(int level)
    {
        if(level < enemyLevels.Count)
        {
            return  enemyLevels[Mathf.Max(level, 0)];
        }
        else
        {
            return enemyImortal;
        }
    }

    public Sprite GetWeaponLevel(int level)
    {
        level = Mathf.Clamp(level, 0, weaponLevels.Count-1);
        return weaponLevels[level];
    }

    public Sprite GetUnitLevel(int level)
    {
        level = Mathf.Clamp(level, 0, unitLevels.Count - 1);
        return unitLevels[level];
    }

    [System.Serializable]
    public struct ResourceIcon
    {
        public ResourceType resource;
        public Sprite icon;
    }
}