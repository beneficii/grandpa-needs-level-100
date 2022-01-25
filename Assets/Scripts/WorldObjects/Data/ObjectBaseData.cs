using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBaseData : ScriptableObject
{
    public Sprite sprite;
    public AudioClip removeSound;

    public int size = 1;
    public ObjectVisuals visuals;

    public virtual void Init(WorldObject obj)
    {
        //obj.render.sprite = sprite;
        obj.transform.localScale = new Vector3(size, size, 0);
        obj.SetVisuals(visuals);
    }

    public virtual void OnCollide(WorldObject obj, PlayerCtrl player)
    {
        
    }
}
