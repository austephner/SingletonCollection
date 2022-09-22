using System;
using UnityEngine;

namespace SingletonCollection
{
    /// <summary>
    /// A singleton <see cref="MonoBehaviour"/> that can be loaded from "Resources/Singletons" if null, using the
    /// implementation's type's name as the asset name.
    /// </summary>
    /// <typeparam name="T">The implementation type.</typeparam>
    public abstract class ResourceSingletonBehaviour<T> : MonoBehaviour where T : ResourceSingletonBehaviour<T>
    {
        public static event Action<ResourceSingletonBehaviour<T>> onEnabled, onDisabled, onDestroyed;
        
        [SerializeField] private bool _dontDestroyOnLoad;

        private static T _instance;

        public static T instance
        {
            get
            {
                if (_instance) return _instance;

                var resourcePath = $"Prefabs/System/{typeof(T).Name}";
                var resource = Resources.Load<T>(resourcePath);

                if (resource != null)
                {
                    return Instantiate(resource);
                }
                
                Debug.LogError($"No singleton resource exists at: {resourcePath}");

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