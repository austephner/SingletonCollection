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
Generic singleton for `MonoBehaviour` implementations that requires nothing special.
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

### `ResourceSingletonBehaviour<T>`
Special type of singleton. When accessing `instance` in code for this type, a prefab will be instantiated from "Resources" if no instance has been set. This is useful for guaranteeing an instance of the singleton will be available. 

- In order for the resource to be found correctly, a prefab must exist in `Resources/Prefabs/System` that is named after the implementation type.
- Implementation and usage is the same as `SingletonBehaviour<T>`
- Instances won't constantly be created, only 1 so long as `Resources.Load<T>()` succeeded

```c#
// singleton implementation
public class MySingleton : ResourceSingletonBehaviour<MySingleton>
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

### `ResourceScriptableObjectSingleton<T>`
Combines the patterns found in `SingletonBehaviour<T>` and `ResourceSingletonBehaviour<T>` but for `ScriptableObject` types.

```c#
TODO
```

# To Do
- Attributes for allowing custom resource paths instead of hardcoded resource paths