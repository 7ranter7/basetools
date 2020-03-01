using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using RanterTools.Base;
namespace RanterTools.Editor.Base
{
    public static class ComponentsUtility
    {
        #region Events

        #endregion Events

        #region Global State

        #endregion Global State

        #region Global Methods
        /// <summary>
        /// Remove all missing scripts
        /// </summary>
        [MenuItem(GlobalNames.MenuRanterTools + "/" + GlobalNames.Components + "/" + GlobalNames.ComponentsRemoveAllMissingScripts)]
        static void RemoveAllMissingScripts()
        {
            for (int s = 0; s < SceneManager.sceneCount; s++)
            {
                foreach (var gameObject in SceneManager.GetSceneAt(s).GetRootGameObjects())
                {
                    RemoveMissingSciptsRecursive(gameObject);
                }
            }
        }
        static void RemoveMissingSciptsRecursive(GameObject gameObject)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
            for (int t = 0; t < gameObject.transform.childCount; t++)
            {
                RemoveMissingSciptsRecursive(gameObject.transform.GetChild(t).gameObject);
            }
        }

        /// <summary>
        /// Remove all selected missing scripts
        /// </summary>
        [MenuItem(GlobalNames.MenuRanterTools + "/" + GlobalNames.Components + "/" + GlobalNames.ComponentsRemoveAllSelectedMissingScripts)]
        static void RemoveAllSelectedMissingScripts()
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                var gameObject = Selection.gameObjects[i];
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
        }
        #endregion Global Methods
    }
}