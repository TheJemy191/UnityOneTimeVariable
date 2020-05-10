# UnityOneTimeVariable
Little unity package that handle one timed variable

## Why?
Ever done something like this?
```c#
bool oneTime = true;

void Update()
{
  if(oneTime)
  {
    oneTime = false;
    //Do something
  }
}
```
Yes so this little package is for you.  
This package will handle oneTimed variable by scope like:  
Session, Scene, PlayerPref, Database even, WorldWide? YES YOU CAN!

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
## Advance use
By classes type
```c#
OneTime.Get<ScopeLocal>()[typeof(MyClass)]; // One time for the class MyClass
OneTime.Get<ScopeLocal>()[typeof(MyClass), "oneTime"]; // One Time for "oneTime" for the classMyClass
```
