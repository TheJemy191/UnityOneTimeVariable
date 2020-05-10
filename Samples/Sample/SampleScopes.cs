using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTimeVariable
{
    public class ScopePlayerPrefs : Scope
    {
        public static void Setting(string playerPrefName) => ScopePlayerPrefs.playerPrefName = playerPrefName;

        private static string playerPrefName = "OneTimePref";

        string oneTimePrefName = playerPrefName;

        public void Setup(string extraInfo) => oneTimePrefName = $"{playerPrefName}_{extraInfo}";
        public override void Init() => Load();
        public override void Save() => PlayerPrefs.SetString(oneTimePrefName, string.Join(";", oneTimes));

        protected override void Load() => oneTimes = new HashSet<string>(PlayerPrefs.GetString(oneTimePrefName, "").Split(';'));
    }

    public class ScopeLocal : Scope { }

    public class ScopeScene<T> : ScopeNested<T> where T : Scope, new()
    {
        public override void Init()
        {
            (nestedScope as ScopePlayerPrefs)?.Setup(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            base.Init();

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += ActiveSceneChanged;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += s => { nestedScope.Save(); };
        }

        private void ActiveSceneChanged(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
        {
            if (oldScene.name != null)
                nestedScope.Save();
            nestedScope = new T();
            (nestedScope as ScopePlayerPrefs)?.Setup(newScene.name);
            nestedScope.Init();
        }
    }
}
