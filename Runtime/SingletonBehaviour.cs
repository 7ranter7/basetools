using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RanterTools.Base
{

    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Global State
        /// <summary>
        /// Flags describing the behavior of singleton
        /// </summary>
        public static SingletonBehaviourFlags singletonBehaviourFlags = SingletonBehaviourFlags.All;
        static T instance = null;
        /// <summary>
        /// Instance of singleton
        /// </summary>
        /// <value>Instance of singleton</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    List<T> monoBehaviours = new List<T>(GameObject.FindObjectsOfType<T>());
                    if (monoBehaviours.Count == 0)
                    {
                        if ((singletonBehaviourFlags & SingletonBehaviourFlags.CreatingFromPrefabe) != 0)
                        {
                            monoBehaviours = new List<T>(Resources.FindObjectsOfTypeAll<T>());
                            if (monoBehaviours.Count == 0)
                            {
                                ToolsDebug.LogWarning("Singleton " + typeof(T) + " have flag creation from prefabe, but prefab not found. Creating new instance.");
                                CreateNewInstance();

                            }
                            else if (monoBehaviours.Count == 1)
                            {
                                instance = Instantiate(monoBehaviours[0].gameObject).GetComponent<T>();
                                if ((singletonBehaviourFlags & SingletonBehaviourFlags.DontDestroyOnLoadOnNew) != 0)
                                    instance.gameObject.AddComponent<DontDestroyOnLoad>();
                            }
                            else
                            {
                                ToolsDebug.LogWarning("Singleton " + typeof(T) + " have flag creation from prefabe, but exist few prefab of this type. Creating new instance.");
                                CreateNewInstance();
                            }
                        }
                        else CreateNewInstance();
                    }
                    else if (monoBehaviours.Count == 1)
                    {
                        instance = monoBehaviours[0];
                    }
                    else
                    {
                        ToolsDebug.LogWarning("Singleton " + typeof(T) + " have few instance on scene. First entry received.");
                        instance = monoBehaviours[0];
                        if ((singletonBehaviourFlags & SingletonBehaviourFlags.DestroyExcess) != 0)
                        {
                            for (int i = 1; i < monoBehaviours.Count; i++)
                            {
                                DestroyImmediate(monoBehaviours[i]);
                            }
                        }
                    }
                }
                return instance;
            }
        }

        static void CreateNewInstance()
        {
            GameObject gameObjectTmp = new GameObject();
            instance = gameObjectTmp.AddComponent<T>();
            if ((singletonBehaviourFlags & SingletonBehaviourFlags.DontDestroyOnLoadOnNew) != 0)
                instance.gameObject.AddComponent<DontDestroyOnLoad>();
        }
        #endregion Global State
    }

    /// <summary>
    /// Flags describing the behavior of singleton
    /// </summary>
    public enum SingletonBehaviourFlags
    {
        None = 0, DontDestroyOnLoadOnNew = 1, CreatingFromPrefabe = 2, DestroyExcess = 3, All = ~0
    }

}
