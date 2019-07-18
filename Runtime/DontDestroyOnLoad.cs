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

        #region Enumerations


        public enum DontDestroyOnLoadParameters
        {
            //None = (0),
            MoveToCurrentSceneAfterRemoveComponent = (1 << 1),
            DeleteGameObjectAfterRemoveComponent = (1 << 2),
            //All = (~0)
        }

        #endregion Enumerations



        #region Parameters

        [SerializeField]
        DontDestroyOnLoadParameters parameters = DontDestroyOnLoadParameters.MoveToCurrentSceneAfterRemoveComponent;

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
            if (((int)parameters & (1 << 1)) >= 1)
            {
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(this.gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            }
            else if (((int)parameters & (1 << 2)) >= 1)
            {
                Destroy(gameObject);
            }
        }

        #endregion Unity
    }

}
