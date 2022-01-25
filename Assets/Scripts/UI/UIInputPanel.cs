using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event System.Action<ClickParams> OnMouseAction;


    public void OnPointerDown(PointerEventData eventData)
    {
        MouseAction(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MouseAction(false);
    }

    public void MouseAction(bool down)
    {
        if (!Game.Started) return;

        OnMouseAction?.Invoke(new ClickParams { down = down });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MouseAction(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            MouseAction(false);
        }
    }
}


public struct ClickParams
{
    public bool down;
    
}
