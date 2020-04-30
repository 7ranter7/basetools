using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RanterTools.Base
{
    /// <summary>
    /// Extentions for Component class.
    /// </summary>
    public static class ComponentCacheExtension
    {
        static Dictionary<GameObject, Dictionary<Type, Component>> Cache { get; set; } = new Dictionary<GameObject, Dictionary<Type, Component>>();

        /// <summary>
        /// Get cached component if it exists.
        /// </summary>
        /// <param name="owner">Component owner</param>
        /// <param name="tryIfCacheNull">Try get component if already known that component is null.virtual Default false.</param>
        /// <typeparam name="T">Component type.</typeparam>
        /// <returns>Return component or null, if it exists not.</returns>
        public static T GetCachedComponent<T>(this Component owner, bool tryIfCacheNull = false) where T : Component
        {
            T result = null;
            if (Cache.ContainsKey(owner.gameObject) && Cache[owner.gameObject] != null)
            {
                ToolsDebug.Log($"Gameobject contains {owner.gameObject}");
                if (Cache[owner.gameObject].ContainsKey(typeof(T)))
                {
                    ToolsDebug.Log($"Component contains {typeof(T)}");
                    if (Cache[owner.gameObject][typeof(T)] == null && tryIfCacheNull)
                    {
                        result = owner.GetComponent<T>();
                        Cache[owner.gameObject][typeof(T)] = (Component)result;
                    }
                    else
                    {
                        result = (T)Cache[owner.gameObject][typeof(T)];
                    }
                }
                else
                {
                    ToolsDebug.Log($"Component doesn't contains {typeof(T)}");
                    result = owner.GetComponent<T>();
                    Cache[owner.gameObject][typeof(T)] = (Component)result;
                }
            }
            else
            {
                ToolsDebug.Log($"Gameobject doesn't contains {owner.gameObject}");
                Cache[owner.gameObject] = new Dictionary<Type, Component>();
                result = owner.GetComponent<T>();
                Cache[owner.gameObject][typeof(T)] = (Component)result;

            }
            return result;
        }
    }

    /// <summary>
    /// Extention for GameObject class.
    /// </summary>
    public static class GameObjectCacheExtension
    {
        static Dictionary<GameObject, Dictionary<Type, Component>> Cache { get; set; } = new Dictionary<GameObject, Dictionary<Type, Component>>();

        /// <summary>
        /// Get cached component if it exists.
        /// </summary>
        /// <param name="owner">Component owner</param>
        /// <param name="tryIfCacheNull">Try get component if already known that component is null.virtual Default false.</param>
        /// <typeparam name="T">Component type.</typeparam>
        /// <returns>Return component or null, if it exists not.</returns>
        public static T GetCachedComponent<T>(this GameObject owner, bool tryIfCacheNull = false) where T : Component
        {
            T result = null;
            if (Cache.ContainsKey(owner) && Cache[owner] != null)
            {
                ToolsDebug.Log($"Gameobject contains {owner}");
                if (Cache[owner].ContainsKey(typeof(T)))
                {
                    ToolsDebug.Log($"Component contains {typeof(T)}");
                    if (Cache[owner][typeof(T)] == null && tryIfCacheNull)
                    {
                        result = owner.GetComponent<T>();
                        Cache[owner][typeof(T)] = (Component)result;
                    }
                    else
                    {
                        result = (T)Cache[owner][typeof(T)];
                    }
                }
                else
                {
                    ToolsDebug.Log($"Component doesn't contains {typeof(T)}");
                    result = owner.GetComponent<T>();
                    Cache[owner][typeof(T)] = (Component)result;
                }
            }
            else
            {
                ToolsDebug.Log($"Gameobject doesn't contains {owner}");
                Cache[owner] = new Dictionary<Type, Component>();
                result = owner.GetComponent<T>();
                Cache[owner][typeof(T)] = (Component)result;

            }
            return result;
        }
    }

}