using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneTimeVariable.BasicScope
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
        /// <summary>Assign before creating</summary>
        public static event System.Action<T> OnInit;
        /// <summary>Assign before creating</summary>
        public static event System.Action<T, Scene> OnSceneChange;

        public override void Init()
        {
            if (OnInit != null)
                OnInit.Invoke(nestedScope);
            base.Init();

            SceneManager.activeSceneChanged += ActiveSceneChanged;
            SceneManager.sceneUnloaded += s => { nestedScope.Save(); };
        }

        private void ActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            if (oldScene.name != null)
                nestedScope.Save();
            nestedScope = new T();
            if (OnSceneChange != null)
                OnSceneChange.Invoke(nestedScope, newScene);
            (nestedScope as ScopePlayerPrefs)?.Setup(newScene.name);
            nestedScope.Init();
        }
    }
}
