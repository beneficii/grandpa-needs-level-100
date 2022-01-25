using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionArrow : MonoBehaviour
{
    public float speed = 50;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, -Time.fixedDeltaTime * speed);
    }
}
