using System;
using UnityEngine;

namespace SingletonCollection
{
    /// <summary>
    /// Provides a <see cref="MonoBehaviour"/> with the ability to behave like a singleton.
    /// </summary>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static event Action<SingletonBehaviour<T>> onEnabled, onDisabled, onDestroyed;

        [SerializeField] private bool _dontDestroyOnLoad;

        private static T _instance;

        public static T instance
        {
            get
            {
                if (_instance) return _instance;

                if (Attribute.GetCustomAttribute(typeof(T), typeof(LoadFromResourceAttribute)) 
                    is LoadFromResourceAttribute loadFromResourceAttribute)
                {
                    var resource = Resources.Load<T>(loadFromResourceAttribute.resourcePath);

                    if (resource)
                    {
                        return Instantiate(resource);
                    }
                    
                    Debug.LogError($"There is no prefab for type {typeof(T).Name} located at \"{loadFromResourceAttribute.resourcePath}\"");
                    return null;
                }
                
                Debug.LogError($"There is no active instance of type {typeof(T).Name}");
                return null;
            }

            private set => _instance = value;
        }

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