using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static T GetComponentInFirstChildren<T>(this Transform transform)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var component = transform.GetChild(i).GetComponent<T>();

            if (component != null)
                return component;
        }
        return default(T);
    }
    
    public static T[] GetComponentsInFirstChildren<T>(this Transform transform)
    {
        var components = new List<T>();
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var component = transform.GetChild(i).GetComponent<T>();

            if (component != null)
                components.Add(component);
        }

        return components.ToArray();
    }
}