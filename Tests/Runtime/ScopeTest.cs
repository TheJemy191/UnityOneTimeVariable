﻿using NUnit.Framework;
using UnityEngine.SceneManagement;
using OneTimeVariable.BasicScope;

namespace OneTimeVariable
{
    public class ScopeTest
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            OneTime.Register<ScopeLocal>();
            OneTime.Register<ScopeScene<ScopeLocal>>();
        }

        [Test]
        public void ScopeLocalTest()
        {
            AssetScope<ScopeLocal>("Test");

            var scene = SceneManager.CreateScene("ScopeLocalTestScene");
            SceneManager.SetActiveScene(scene);

            Assert.IsFalse(OneTime.Get<ScopeLocal>()["Test"]);
        }

        [Test]
        public void ScopeSceneLocalTest()
        {
            //Setup
            var scene1 = SceneManager.CreateScene("ScopeSceneLocalTest1");
            var scene2 = SceneManager.CreateScene("ScopeSceneLocalTest2");

            SceneManager.SetActiveScene(scene1);
            AssetScope<ScopeScene<ScopeLocal>>("Test");

            SceneManager.SetActiveScene(scene2);
            AssetScope<ScopeScene<ScopeLocal>>("Test");


            SceneManager.SetActiveScene(scene1);
            AssetScope<ScopeScene<ScopeLocal>>("Test");

            SceneManager.SetActiveScene(scene2);
            AssetScope<ScopeScene<ScopeLocal>>("Test");
        }

        static void AssetScope<T>(string testString) where T : Scope, new()
        {
            Assert.IsTrue(OneTime.Get<T>()[testString]);
            Assert.IsFalse(OneTime.Get<T>()[testString]);
        }
    }
}