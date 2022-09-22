using System;

namespace SingletonCollection
{
    /// <summary>
    /// Allows <see cref="SingletonBehaviour{T}"/> 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LoadFromResourceAttribute : Attribute
    {
        public readonly string resourcePath;
        
        public LoadFromResourceAttribute(string resourcePath)
        {
            this.resourcePath = resourcePath;
        }
    }
}