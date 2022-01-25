using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Dungeon/Objects/Enemy")]
public class ObjectEnemyData : ObjectBaseData
{
    public int maxHealth = 1;
    public int damage = 0;
    public int experience = 1;

    public override void Init(WorldObject obj)
    {
        base.Init(obj);
        obj.health = maxHealth;
        obj.hpSlider.maxValue = maxHealth;
        if(obj.dontHideHPBar)
        {
            obj.hpSlider.gameObject.SetActive(true);
        }
    }

    public override void OnCollide(WorldObject obj, PlayerCtrl player)
    {
        base.OnCollide(obj, player);
        var resources = Game.resources;
        resources.Remove(ResourceType.Health, damage);
        obj.health -= resources.Get(ResourceType.Damage);
        if(obj.health <= 0)
        {
            resources.Add(ResourceType.Experience, experience);
            obj.Remove();
        } else
        {
            Game.sounds.Play(GenericSound.GetHit);
            player.rb2d.velocity = -player.rb2d.velocity/2f;
            obj.ShowHpBar();
        }
    }
}
