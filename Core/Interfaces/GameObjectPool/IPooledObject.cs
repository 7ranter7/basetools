using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RanterTools.Base
{
    /// <summary>
    /// Interface for pooled game object
    /// </summary>
    public interface IPooledObject
    {
        #region Events
        /// <summary>
        /// Pooled object start event
        /// </summary>
        event System.Action PooledObjectStartEvent;
        /// <summary>
        /// Pooled object destroy event
        /// </summary>
        event System.Action PooledObjectDestroyEvent;
        #endregion Events
        #region Parameters
        /// <summary>
        /// Parant prefabe used for creation this instace
        /// </summary>
        /// <value>Parant prefabe used for creation this instace</value>
        GameObject ParentPrefab { get; set; }
        #endregion Parameters
        #region Methods
        /// <summary>
        ///  Custom handler for start pooled game object
        /// </summary>
        void StartPooledObject();
        /// <summary>
        /// Custom handler for destroy pooled game object
        /// </summary>
        void DestroyPooledObject();
        #endregion Methods
    }
}