using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Rigidbody2D rb2d { get; private set; }

    public float launchForce = 1f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void HandleDirection(DirectionParams param)
    {
        //rb2d.AddForce(param.direction * launchForce);
        rb2d.velocity = param.direction * launchForce * Game.resources.Get(ResourceType.Energy);
    }

    void HandleMouse(ClickParams param)
    {
        if (param.down)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            
        }
    }

    private void OnEnable()
    {
        UIDirectionSpinner.OnDirection += HandleDirection;
        UIInputPanel.OnMouseAction += HandleMouse;
        Game.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        UIDirectionSpinner.OnDirection -= HandleDirection;
        UIInputPanel.OnMouseAction -= HandleMouse;
        Game.OnGameOver -= GameOver;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<WorldObject>();

        if(enemy)
        {
            enemy.OnCollide(this);
        }
    }

    void GameOver(GameOverParams param)
    {
        if(!param.win)
        {
            GetComponent<ObjectVisuals>().LaunchOnce(AnimState.Death);
        }
        enabled = false;
        //Destroy(gameObject); //ToDo: put some animation
    }
}
