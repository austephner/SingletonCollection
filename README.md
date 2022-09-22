# Singleton Collection
#### Summary
Various types of singleton-like classes for Unity scripts and assets.

# Usage
1. Implement the singleton class of your choice
2. Create a new game object in the scene and add the new component
3. Reference the singleton as needed in code
4. ???
5. Profit

# Singleton Types
### `SingletonBehaviour<T>`
Provides singleton functionality for `MonoBehaviour` types.
```c#
// Singleton implementation
public class MySingleton : SingletonBehaviour<MySingleton> 
{
    public void HelloWorld() => Debug.Log("Hello World");
}

// A class that uses the singleton's function by calling `instance`
public class MyBehaviour : MonoBehaviour
{
    private void Start()
    {
        MySingleton.instance.HelloWorld();
    }
} 
```

### `SingletonScriptableObject<T>`
Provides singleton functionality for `ScriptableObject` types.

```c#
// Singleton implementation
[CreateAssetMenu(menuName = "My Singleton")]
public class MySingleton : SingletonScriptableObject<MySingleton> 
{
    public int someConfigurationValue = 20;
}

// A class that uses the singleton's configuration value by calling `instance`
public class MyPlayerBehaviour : MonoBehaviour 
{
    public int movementSpeed { get; private set;

    private void Start()
    {
        movementSpeed = MySingleton.instance.someConfigurationValue;
    }
}
```

# Attributes
### LoadFromResourceAttribute
This attribute allows devs to use a resource as the source object for a singleton when an `instance` has not yet been set in the scene. Thus referencing a singleton implementation's `instance` property would instantiate the resource at the given path provided it exists and a current `instance` has not yet been set.
- Singletons can become prefabs or one-off configuration files
- Prevents race conditions issues
- Guarantees an instance is available for a singleton
- Can be used on either `SingletonBehaviour<T>` or `SingletonScriptableObject<T>`

```c#
[LoadFromResource($"Prefabs/System/{typeof(MySingleton).Name}")]
public class MySingleton : SingletonBehaviour<MySingleton>
{
}

[LoadFromResource($"Prefabs/System/{typeof(MyConfigurationSingleton).Name}")]
[CreateAssetMenu(menuName = "My Configuration")]
public class MyConfigurationSingleton : SingletonScriptableObject<MyConfigurationSingleton>
{
}
```

# Use Cases
It's often not best practice to provide global access to certain manager or system classes. However, in scenarios where game data is necessary on multiple levels, using singletons can be a means to save time and provide easy access.

My favorite use case is a generic `GameConfiguration` implementation that can be accessed from multiple classes, behaviours, etc. from across the codebase:
```c#
[LoadFromResource($"Configuration/{typeof(GameConfiguration).Name}")]
[CreateAssetMenu(menuName = "Configuration/Game Configuration")]
public class GameConfiguration : SingletonScriptableObject<GameConfiguration>
{
    public int maxLocalPlayers = 4;
    public int maxAfkSeconds = 20; 
    // etc. 
}
```