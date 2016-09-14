using System;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMapper
{

    private static Dictionary<Type, object> map = new Dictionary<Type, object>();

    public static void Map<T>(T instance)
    {
        if (Has<T>())
        {
            Debug.LogWarning("SingletonMapper already has a mapping for " + typeof(T));
        }
        map[typeof(T)] = instance;
    }

    public static T Get<T>()
    {
        if (!Has<T>())
        {
            return default(T);
        }
        return (T) map[typeof(T)];
    }

    public static bool Has<T>()
    {
        return map.ContainsKey(typeof(T));
    }

}
