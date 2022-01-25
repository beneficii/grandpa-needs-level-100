using UnityEngine;
using System.Collections;

public class HealthCtrl : MonoBehaviour
{
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
        if (info.type == ResourceType.Health)
        {
            if(info.amount <= 0)
            {
                Game.Defeat();
            }
        }
    }

    void OnMaxResourceReached(ResourceInfo info)
    {
    }
}
