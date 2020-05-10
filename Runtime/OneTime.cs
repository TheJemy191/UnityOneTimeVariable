using System.Collections.Generic;
using System;

namespace OneTimeVariable
{
    public static class OneTime
    {
        static Dictionary<Type, Scope> scopes = new Dictionary<Type, Scope>();

        public static bool Register<T>() where T : Scope, new()
        {
            if (!scopes.ContainsKey(typeof(T)))
            {
                T scope = new T();
                scope.Init();
                scopes.Add(typeof(T), scope);
                return true;
            }
            return false;
        }

        public static Scope Get<T>()
        {
            if (scopes.ContainsKey(typeof(T)))
                return scopes[typeof(T)];
            else
                throw new KeyNotFoundException($"Scope {nameof(T)} not register yet");
        }
    }
}