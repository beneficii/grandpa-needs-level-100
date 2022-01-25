using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Easings
{
    public delegate float easingDelegate(float t);

    public static float Linear(float t) => t;

    public static float OutCubic(float t) => InCubic(t - 1) + 1;
    public static float InCubic(float t) => t*t*t;
}