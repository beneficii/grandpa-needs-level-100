using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;


public static class Extensions
{
    public static bool MoveTowards(this Transform transform, Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        return transform.position == target;
    }

    public static Vector3 RandomAround(this Vector3 vector, float radiusX, float radiusY, float radiusZ = 0f)
    {
        return vector + new Vector3(
            UnityEngine.Random.Range(-radiusX, +radiusX),
            UnityEngine.Random.Range(-radiusY, +radiusY),
            UnityEngine.Random.Range(-radiusZ, +radiusZ)
        );
    }

    public static TReturn GetMin<TReturn>(this IEnumerable<TReturn> list, Func<TReturn, float> valueGetter, float limit = float.MaxValue)
    {
        float minValue = limit;
        TReturn result = default;

        foreach (var item in list)
        {
            float value = valueGetter(item);
            if (value < minValue)
            {
                minValue = value;
                result = item;
            }
        }

        return result;
    }

    public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
    {
        if (dict.TryGetValue(key, out var value))
        {
            return value;
        }

        return default;
    }

    public static TValue Rand<TValue>(this IEnumerable<TValue> list)
    {
        if (list.Count() == 0) return default;

        return list.ElementAt(UnityEngine.Random.Range(0, list.Count()));
    }


    public static bool TryStringAt(this IEnumerable<string> list, int idx, out string value)
    {
        if (list.Count() > idx)
        {
            value = list.ElementAt(idx);
            return true;
        }

        value = null;
        return false;
    }


    public static float FloatAt(this IEnumerable<string> list, int idx)
    {
        if (list.TryStringAt(idx, out var s))
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        return 0f;
    }

    public static int IntAt(this IEnumerable<string> list, int idx)
    {
        if (list.TryStringAt(idx, out var s))
        {
            return int.Parse(s, CultureInfo.InvariantCulture);
        }

        return 0;
    }

    public static void Play(this AudioClip clip, float volume = 1f)
    {
        if (!clip) return;
        AudioCtrl.current.Play(clip, volume);
    }
}
