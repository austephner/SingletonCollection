using System;
using UnityEngine;

namespace SingletonCollection
{
    /// <summary>
    /// A singleton <see cref="MonoBehaviour"/> class.
    /// </summary>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static event Action<SingletonBehaviour<T>> onEnabled, onDisabled, onDestroyed;

        [SerializeField] private bool _dontDestroyOnLoad;

        public static T instance { get; private set; } = null;

        protected virtual void OnEnable()
        {
            if (instance && instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = (T)this;

            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            onEnabled?.Invoke(this);
        }

        protected virtual void OnDisable()
        {
            if (instance == this)
            {
                instance = null;
                onDisabled?.Invoke(this);
            }
        }

        protected void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
                onDestroyed?.Invoke(this);
            }
        }
    }
}