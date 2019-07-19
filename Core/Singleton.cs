using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace RanterTools.Base
{
    public abstract class Singleton<T> where T : class, new()
    {
        #region Global State
        T instance = null;

        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
        #endregion Global State
    }

}