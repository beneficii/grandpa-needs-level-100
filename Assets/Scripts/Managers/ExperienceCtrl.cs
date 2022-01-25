using UnityEngine;
using System.Collections;

public class ExperienceCtrl : MonoBehaviour
{
    public int GetNextLevelMaxXP(int currentMax)
    {
        return 1;
    }

    private void Start()
    {
        ResourceCtrl.OnResourceChanged += ResourceChanged;
        ResourceCtrl.OnMaxReached += OnMaxResourceReached;
    }

    private void OnDestroy()
    {
        ResourceCtrl.OnResourceChanged -= ResourceChanged;
        ResourceCtrl.OnMaxReached -= OnMaxResourceReached;
    }

    void ResourceChanged(ResourceInfo info)
    {
        if (info.type == ResourceType.Level)
        {
            if (info.amount <= 0)
            {
                Game.Defeat();
            }
        }
    }

    void OnMaxResourceReached(ResourceInfo info)
    {
        if(info.type == ResourceType.Experience)
        {
            var resources = Game.resources;
            int nextMax = info.max;
            int levels = 0;

            while(info.amount >= nextMax)
            {
                info.amount -= nextMax;
                nextMax++;
                levels++;
            }

            resources.AddMax(ResourceType.Experience, levels, true);
            resources.Add(ResourceType.Level, levels);
            resources.Add(ResourceType.SkillPoints, levels);
            Game.sounds.Play(GenericSound.LevelUp);

            if (resources.Get(ResourceType.Level) >= 100)
            //if (resources.Get(ResourceType.Level) >= 60)
            {
                Game.Victory();
            }
        }

    }
}
