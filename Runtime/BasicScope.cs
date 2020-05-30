using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneTimeVariable.BasicScope
{
    public class ScopePlayerPrefs : Scope
    {
        public static void Setting(string playerPrefName) => ScopePlayerPrefs.playerPrefName = playerPrefName;

        static string playerPrefName = "OneTimePref";

        string oneTimePrefName = playerPrefName;

        public void Setup(string extraInfo) => oneTimePrefName = $"{playerPrefName}_{extraInfo}";
        public override void Init() => Load();
        public override void Save() => PlayerPrefs.SetString(oneTimePrefName, string.Join(";", OneTimes));
        protected override void Load() => OneTimes = new HashSet<string>(PlayerPrefs.GetString(oneTimePrefName, "").Split(';'));
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
            OnInit?.Invoke(NestedScope);
            base.Init();

            SceneManager.activeSceneChanged += ActiveSceneChanged;
            SceneManager.sceneUnloaded += s => { NestedScope.Save(); };
        }

        private void ActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            if (oldScene.name != null)
                NestedScope.Save();
            
            NestedScope = new T();
            
            OnSceneChange?.Invoke(NestedScope, newScene);
            (NestedScope as ScopePlayerPrefs)?.Setup(newScene.name);
            
            NestedScope.Init();
        }
    }
}
