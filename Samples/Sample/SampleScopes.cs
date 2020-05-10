using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTimeVariable
{
    public class ScopePlayerPrefs : Scope
    {
        private const string PLAYER_PREF = "OneTimePref";
        string oneTimePrefName = PLAYER_PREF;

        public void Setup(string extraInfo) => oneTimePrefName = $"{PLAYER_PREF}_{extraInfo}";

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
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += s => { nestedScope.Save(); };
        }

        private void SceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode arg1)
        {
            nestedScope = new T();
            (nestedScope as ScopePlayerPrefs)?.Setup(scene.name);
            nestedScope.Init();
        }
    }
}
