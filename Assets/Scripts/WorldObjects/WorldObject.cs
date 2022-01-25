using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WorldObject : MonoBehaviour
{
    public SpriteRenderer render;
    public Slider hpSlider;

    public ObjectBaseData predefinedData;
    public bool dontHideHPBar;

    public UnityEvent OnRemoveEvent;

    public ObjectVisuals visuals;


    float _healthBarHideTime = -1f;
    public int health { get; set; }
    bool isAlive = false;

    public void ShowHpBar()
    {
        if (dontHideHPBar) return;

        hpSlider.value = health;

        hpSlider.gameObject.SetActive(true);
        _healthBarHideTime = Time.time + 5f;
    }

    ObjectBaseData data;

    private void Start()
    {
        if(predefinedData)
        {
            Init(predefinedData);
        }
    }

    public virtual void OnCollide(PlayerCtrl player)
    {
        if (!isAlive) return;
        data.OnCollide(this, player);
    }

    public void Init(ObjectBaseData data)
    {
        this.data = data;
        data.Init(this);
    }

    public void SetVisuals(ObjectVisuals prefab)
    {
        var instance = Instantiate(prefab, transform);
        instance.transform.localScale = Vector3.one;
        visuals = instance;

        visuals.LaunchOnce(AnimState.Spawn, OnSpawnFinish);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnSpawnFinish()
    {
        isAlive = true;
        visuals.LaunchLoop(AnimState.Idle);
    }

    void OnDeathFinish()
    {
        Destroy(gameObject);
    }

    public void Remove()
    {
        isAlive = false;
        visuals.LaunchOnce(AnimState.Death, OnDeathFinish);
        hpSlider.gameObject.SetActive(false);
        OnRemoveEvent?.Invoke();
        Game.sounds.Play(data.removeSound);

    }

    private void Update()
    {
        if(_healthBarHideTime > 0 && Time.time >= _healthBarHideTime )
        {
            hpSlider.gameObject.SetActive(false);
            _healthBarHideTime = -1f;
        }
    }

}
