using System;
using UnityEngine;

namespace SingletonCollection
{
    /// <summary>
    /// Provides a <see cref="ScriptableObject"/> with the ability to behave like a singleton.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
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

        /// <summary>
        /// Denotes whether or not an instance of this singleton exists.
        /// </summary>
        public static bool exists => _instance;
    }
}