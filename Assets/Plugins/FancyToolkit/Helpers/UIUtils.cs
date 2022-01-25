using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UIUtils
{
    public static T CreateFromTemplate<T>(T template) where T : Component
    {
        var instance = GameObject.Instantiate(template, template.transform.parent);
        instance.gameObject.SetActive(true);

        return instance;
    }

    public static IEnumerable<T> IterateFromTemplate<T>(T template) where T : Component
    {
        foreach (Transform item in template.transform.parent)
        {
            if (item.gameObject.activeSelf)
            {
                var retval = item.GetComponent<T>();
                if (retval != null)
                {
                    yield return retval;
                }
            }
        }
    }

    public static void Clear<T>(T template) where T : Component
    {
        foreach (Transform item in template.transform.parent)
        {
            if (item.gameObject.activeSelf)
            {
                GameObject.Destroy(item.gameObject);
            }
        }
    }
}