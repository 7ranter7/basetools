using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RanterTools.Base;

public class PooledObject : MonoBehaviour, IPooledObject
{
    #region Events
    #region  IPooledObject
    /// <summary>
    /// Pooled object start event
    /// </summary>
    public event System.Action PooledObjectStartEvent;
    /// <summary>
    /// Pooled object destroy event
    /// </summary>
    public event System.Action PooledObjectDestroyEvent;
    #endregion IPooledObject
    #endregion Events
    #region Parameters
    #region  IPooledObject
    /// <summary>
    /// Parant prefabe used for creation this instace.
    /// </summary>
    GameObject parentPrefab;
    /// <summary>
    /// Parant prefabe used for creation this instace.
    /// </summary>
    /// <value>Parant prefabe used for creation this instace</value>
    public GameObject ParentPrefab { get { return parentPrefab; } set { if (parentPrefab == null) parentPrefab = value; } }
    #endregion IPooledObject
    #endregion Parameters
    #region Methods
    #region  IPooledObject
    /// <summary>
    ///  Custom handler for start pooled game object
    /// </summary>
    public void StartPooledObject()
    {
        if (PooledObjectStartEvent != null) PooledObjectStartEvent();
    }
    /// <summary>
    /// Custom handler for destroy pooled game object
    /// </summary>
    public void DestroyPooledObject()
    {
        if (PooledObjectDestroyEvent != null) PooledObjectDestroyEvent();
    }
    #endregion IPooledObject
    #endregion Methods
}
