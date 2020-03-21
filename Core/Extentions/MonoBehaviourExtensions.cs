using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RanterTools.Base
{
    /// <summary>
    /// MonoBehaviour Extentions
    /// </summary>
    public static class MonoBehaviourExtentions
    {

        #region Global State
        #region MonoBehaviourPool
        /// <summary>
        /// Pool of MonoBehaviours.
        /// </summary>
        /// <typeparam name="MonoBehaviour">MonoBehaviour which describe their pool. </typeparam>
        /// <typeparam name="MonoBehaviourPool">Pool of instances of this MonoBehaviours.</typeparam>
        /// <returns>Dictionary for rapid access of instances.</returns>
        static Dictionary<MonoBehaviour, MonoBehaviourPool> pool = new Dictionary<MonoBehaviour, MonoBehaviourPool>();
        public static GameObject rootMonoBehaviourForPools;
        #endregion MonoBehaviourPool
        #endregion Global State

        #region Global Methods
        #region MonoBehaviourPool
        /// <summary>
        /// Initiate MonoBehaviours pool with size.
        /// </summary>
        /// <param name="origin">Origin of MonoBehaviour for creating MonoBehaviours on pool.</param>
        /// <param name="size">Size of pool.</param>
        public static void InitiateMonoBehaviourPool(this MonoBehaviour origin, int size = 1000)
        {
            if (rootMonoBehaviourForPools == null)
            {
                rootMonoBehaviourForPools = new GameObject();
                rootMonoBehaviourForPools.name = "Pools of MonoBehaviours";
            }
            if (pool.ContainsKey(origin))
            {
                pool[origin].ResizePool(size);
                return;
            }
            GameObject container = new GameObject();
            container.transform.parent = rootMonoBehaviourForPools.transform;
            container.name = "Container " + origin.name;
            var poolContainer = container.AddComponent<PoolContainer>();
            poolContainer.ParentOrigin = origin;
            MonoBehaviourPool poolTmp = new MonoBehaviourPool();
            poolTmp.poolContainer = poolContainer;
            poolTmp.ResizePool(origin, size);
            pool[origin] = poolTmp;
        }

        /// <summary>
        /// Destroy MonoBehaviours pool of origin.
        /// </summary>
        /// <param name="origin">Origin of MonoBehaviour for destroy game MonoBehaviours pool of this.</param>
        public static void DestroyMonoBehaviourPool(this MonoBehaviour origin)
        {
            if (rootMonoBehaviourForPools == null) return;
            if (!pool.ContainsKey(origin)) return;
            pool[origin].DestroyAll();
            pool.Remove(origin);
        }


        /// <summary>
        /// Special instansiate method for pooled MonoBehaviours.
        /// </summary>
        public static MonoBehaviour InstancePooledMonoBehaviour(this MonoBehaviour origin)
        {
            if (pool.ContainsKey(origin))
            {
                return pool[origin].Instance();
            }
            else
            {
                InitiateMonoBehaviourPool(origin);
                return pool[origin].Instance();
            }
        }

        /// <summary>
        /// Special destroy method for pooled MonoBehaviours.
        /// </summary>
        public static void DestroyPooledMonoBehaviour(this MonoBehaviour origin, MonoBehaviour MonoBehaviour)
        {

            if (pool.ContainsKey(origin))
            {
                pool[origin].Destroy(MonoBehaviour);
            }
        }
        #endregion MonoBehaviourPool


        #endregion Unity
    }


    [System.Serializable]
    class MonoBehaviourPool
    {
        #region Parameters
        /// <summary>
        /// GameObject pool container
        /// </summary>
        public PoolContainer poolContainer;
        #endregion Parameters
        #region State
        /// <summary>
        /// Parent origin for creation instances.
        /// </summary>
        MonoBehaviour origin;
        /// <summary>
        /// Dictionary for available MonoBehaviours in pool.
        /// </summary>
        /// <typeparam name="MonoBehaviour">Key type.</typeparam>
        /// <typeparam name="MonoBehaviour">Value type.</typeparam>
        /// <returns>MonoBehaviour.</returns>
        Dictionary<MonoBehaviour, MonoBehaviour> available = new Dictionary<MonoBehaviour, MonoBehaviour>();
        /// <summary>
        /// Dictionary for inaccessible MonoBehaviours in pool.
        /// </summary>
        /// <typeparam name="MonoBehaviour">Key type.</typeparam>
        /// <typeparam name="MonoBehaviour">Value type.</typeparam>
        /// <returns>MonoBehaviour.</returns>
        Dictionary<MonoBehaviour, MonoBehaviour> inaccessible = new Dictionary<MonoBehaviour, MonoBehaviour>();
        /// <summary>
        /// Current pool size.
        /// </summary>
        int currentSize = 10;

        #endregion State

        #region Mathods
        /// <summary>
        /// Resize MonoBehaviour pool.
        /// </summary>
        /// <param name="origin">Parent origin for creation instances.</param>
        /// <param name="size">Pool size.</param>
        public void ResizePool(MonoBehaviour origin, int size)
        {
            if (size <= 2) return;
            if (size <= available.Count)
            {
                MonoBehaviour temp;
                var enumerator = available.GetEnumerator();
                enumerator.MoveNext();
                for (int i = 0; i < (available.Count - size); i++)
                {
                    temp = enumerator.Current.Value;
                    available.Remove(temp);
                    MonoBehaviour.DestroyImmediate(temp);
                }
            }
            else
            {
                MonoBehaviour temp;
                IPooledObject interfacePooledObject = null;
                for (int i = 0; i < (size - available.Count); i++)
                {
                    temp = MonoBehaviour.Instantiate(origin);
                    interfacePooledObject = temp.GetComponent<IPooledObject>();
                    if (interfacePooledObject == null) interfacePooledObject = temp.gameObject.AddComponent<PooledObject>();
                    interfacePooledObject.ParentPrefab = origin;
                    interfacePooledObject.ParentPoolContainer = poolContainer;
                    temp.transform.parent = poolContainer.transform;
                    temp.gameObject.SetActive(false);
                    available[temp] = temp;
                }
            }
            this.origin = origin;
            currentSize = size;
        }
        /// <summary>
        /// Resize MonoBehaviour pool.
        /// </summary>
        /// <param name="size">Pool size.</param>
        public void ResizePool(int size)
        {
            ResizePool(origin, size);
        }

        /// <summary>
        /// Get instance from MonoBehaviour pool.
        /// </summary>
        /// <returns></returns>
        public MonoBehaviour Instance()
        {
            if (available.Count != 0)
            {
                IPooledObject interfacePooledObject = null;
                var enumerator = available.GetEnumerator();
                enumerator.MoveNext();
                MonoBehaviour temp = enumerator.Current.Key;
                interfacePooledObject = temp.GetComponent<IPooledObject>();
                if (interfacePooledObject == null) interfacePooledObject = temp.gameObject.AddComponent<PooledObject>();
                available.Remove(temp);
                inaccessible[temp] = temp;
                temp.gameObject.SetActive(true);
                interfacePooledObject.StartPooledObject();
                return temp;
            }
            else
            {
                throw (new MonoBehaviourPoolException());
            }
        }
        /// <summary>
        /// Destroy instance of pooled MonoBehaviour.
        /// </summary>
        /// <param name="MonoBehaviour">Deleting MonoBehaviour.</param>
        public void Destroy(MonoBehaviour MonoBehaviour)
        {
            if (inaccessible.ContainsKey(MonoBehaviour))
            {
                IPooledObject interfacePooledObject = null;
                interfacePooledObject = MonoBehaviour.GetComponent<IPooledObject>();
                if (interfacePooledObject == null) interfacePooledObject = MonoBehaviour.gameObject.AddComponent<PooledObject>();
                interfacePooledObject.DestroyPooledObject();
                inaccessible.Remove(MonoBehaviour);
                available[MonoBehaviour] = MonoBehaviour;
                MonoBehaviour.gameObject.SetActive(false);
                MonoBehaviour.transform.parent = poolContainer.transform;
            }
        }
        /// <summary>
        /// Destroy all MonoBehaviours in this pool.
        /// </summary>
        public void DestroyAll(bool justInited = false)
        {
            var iterator = inaccessible.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (!justInited)
                {
                    IPooledObject interfacePooledObject = null;
                    interfacePooledObject = iterator.Current.Value.GetComponent<IPooledObject>();
                    if (interfacePooledObject == null) interfacePooledObject = iterator.Current.Value.gameObject.AddComponent<PooledObject>();
                    interfacePooledObject.DestroyPooledObject();
                    available[iterator.Current.Value] = iterator.Current.Value;
                    iterator.Current.Value.gameObject.SetActive(false);
                    iterator.Current.Value.transform.parent = poolContainer.transform;
                }
                else
                {
                    Debug.Log($"Value {iterator.Current.Value}");
                    if (iterator.Current.Value != null) MonoBehaviour.DestroyImmediate(iterator.Current.Value);
                }
            }
            inaccessible.Clear();
        }
        #endregion Methods
    }
    /// <summary>
    /// Exception if pool empty.
    /// </summary>
    public class MonoBehaviourPoolException : System.Exception
    {
        public override string Message { get { return "Try get instance from empty pool."; } }
    }

}