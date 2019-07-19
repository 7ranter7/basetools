using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RanterTools.Base
{
    ///<summary>
    /// Don't destroy on load component
    ///</summary>
    [DisallowMultipleComponent]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        #region Parameters

        [SerializeField]
        DontDestroyOnLoadFlags flags = DontDestroyOnLoadFlags.MoveToCurrentSceneAfterRemoveComponent;

        #endregion Parameters

        #region Unity

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Object.DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        void OnDestroy()
        {
            if ((flags & DontDestroyOnLoadFlags.MoveToCurrentSceneAfterRemoveComponent) != 0)
            {
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(this.gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            }
            else if ((flags & DontDestroyOnLoadFlags.DeleteGameObjectAfterRemoveComponent) != 0)
            {
                Destroy(gameObject);
            }
        }

        #endregion Unity
    }


    public enum DontDestroyOnLoadFlags
    {
        MoveToCurrentSceneAfterRemoveComponent = (1 << 1),
        DeleteGameObjectAfterRemoveComponent = (1 << 2),
    }

}
