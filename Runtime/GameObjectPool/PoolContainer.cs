using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RanterTools.Base
{

    /// <summary>
    /// Container that contained pooled gameObjects.
    /// </summary>
    public class PoolContainer : MonoBehaviour
    {
        #region State
        /// <summary>
        ///  Prefab for instance or destroying pooled gameObjects.
        /// </summary>
        GameObject parentPrefab;
        /// <summary>
        /// Prefab for instance or destroying pooled gameObjects property.
        /// </summary>
        /// <value> Prefab for instance or destroying pooled gameObjects.</value>
        public GameObject ParentPrefab
        {
            get { return parentPrefab; }
            set { parentPrefab = value; }
        }
        #endregion State
        #region Unity
        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        void OnDestroy()
        {
            ParentPrefab.DestroyGameObjectPool();
        }
        #endregion Unity
    }


}