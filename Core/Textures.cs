using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace RanterTools.Textures
{
    public class Textures : MonoBehaviour
    {
        #region Events

        #endregion Events

        #region Global State

        #endregion Global State

        #region Global Methods
        public static string Texture2DToBase64(Texture2D texture)
        {
            byte[] imageData = texture.EncodeToPNG();
            return Convert.ToBase64String(imageData);
        }

        public static Texture2D Base64ToTexture2D(string encodedData)
        {
            byte[] imageData = Convert.FromBase64String(encodedData);

            int width, height;
            GetImageSize(imageData, out width, out height);

            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(imageData);

            return texture;
        }

        static void GetImageSize(byte[] imageData, out int width, out int height)
        {
            width = ReadInt(imageData, 3 + 15);
            height = ReadInt(imageData, 3 + 15 + 2 + 2);
        }
        static int ReadInt(byte[] imageData, int offset)
        {
            return (imageData[offset] << 8) | imageData[offset + 1];
        }
        #endregion Global Methods

        #region Parameters

        #endregion Parameters

        #region State

        #endregion State

        #region Methods

        #endregion Methods
    }
}