using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Helpers
{
    static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    public static Vector2 MouseToWorldPosition()
    {
        return Camera.ScreenToWorldPoint(Input.mousePosition);
    }
}