# UnityOneTimeVariable
Little unity package that handle one timed variable

## How to use
```c#
using OneTimeVariable;
using OneTimeVariable.BasicScope;

void Awake()
{
  OneTime.Register<ScopePlayerPrefs>();
  OneTime.Register<ScopeLocal>();//One per session
  OneTime.Register<ScopeScene<ScopeLocal>>();//One per scene
}

void Update()
{
  if(OneTime.Get<ScopePlayerPrefs>()["OneTimeVariable"])
    Debug.Log("This will be executed only one time or until the PlayerPref are deleted");
    
  if(OneTime.Get<ScopeLocal>()["OneTimeVariable"])
    Debug.Log("This will be executed only one time in this play session");    
    
  if(OneTime.Get<ScopeScene<ScopeLocal>>()["OneTimeVariable"])
    Debug.Log("This will be executed only one time in a scene. When the scene is reloaded. It will execute again");
}
```
