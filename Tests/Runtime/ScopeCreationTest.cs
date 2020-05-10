using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OneTimeVariable.BasicScope;
using UnityEngine;
using UnityEngine.TestTools;

namespace OneTimeVariable
{
    public class ScopeCreationTest
    {
        [Test]
        public void RegisterScopeMultipleTime()
        {
            Assert.DoesNotThrow(() => { OneTime.Register<ScopeLocal>(); });
            Assert.Throws<ArgumentException>(() => { OneTime.Register<ScopeLocal>(); });

            OneTime.Unregsiter<ScopeLocal>();
        }

        [Test]
        public void UnregisterUnregistedScope() => Assert.Throws<KeyNotFoundException>(() => { OneTime.Unregsiter<ScopeLocal>(); });

        [Test]
        public void UnregisterRegisteredScope()
        {
            OneTime.Register<ScopeLocal>();
            Assert.DoesNotThrow(() => { OneTime.Unregsiter<ScopeLocal>(); });
        }
    }
}
