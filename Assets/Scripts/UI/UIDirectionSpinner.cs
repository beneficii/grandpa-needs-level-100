using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionSpinner : MonoBehaviour
{
    public static event System.Action<DirectionParams> OnDirection;

    public UIDirectionArrow arrow;
    public float cooldown = 1f;

    CanvasGroup cg;

    float _timeout = -1f;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        UIInputPanel.OnMouseAction += HandleClick;
    }

    private void OnDisable()
    {
        UIInputPanel.OnMouseAction -= HandleClick;
    }

    void HandleClick(ClickParams param)
    {
        if(param.down)
        {
            //arrow.gameObject.SetActive(true);
            arrow.gameObject.SetActive(true);
        } else
        {
            if (!arrow.gameObject.activeSelf) return;
            arrow.gameObject.SetActive(false);

            if (_timeout > 0) return;
            OnDirection?.Invoke(new DirectionParams { direction = arrow.transform.up});
            _timeout = Time.time + cooldown;
            cg.alpha = 0.2f;
        }
    }

    private void Update()
    {
        if (_timeout > 0 && Time.time >= _timeout)
        {
            cg.alpha = 1f;
            _timeout = -1f;
        }
    }
}


public struct DirectionParams
{
    public float z;
    public Vector2 direction;
}