using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Dungeon/Objects/Resource")]
public class ObjectResourceData : ObjectBaseData
{
    public ResourceType resource;
    public int amount = 1;
    
    public override void OnCollide(WorldObject obj, PlayerCtrl player)
    {
        base.OnCollide(obj, player);
        var resources = Game.resources;
        resources.Add(resource, amount);
        obj.Remove();
    }
}