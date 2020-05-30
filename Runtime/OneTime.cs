using System.Collections.Generic;
using System;

namespace OneTimeVariable
{
    public static class OneTime
    {
        private static readonly Dictionary<Type, Scope> Scopes = new Dictionary<Type, Scope>();

        public static void Register<T>() where T : Scope, new()
        {
            if (!Scopes.ContainsKey(typeof(T)))
            {
                T scope = new T();
                scope.Init();
                Scopes.Add(typeof(T), scope);
            }
            else
                throw new ArgumentException($"{typeof(T).Name} already register");
        }

        public static void Unregsiter<T>() where T : Scope, new()
        {
            if (Scopes.ContainsKey(typeof(T)))
            {
                Scopes[typeof(T)].Save();
                Scopes.Remove(typeof(T));
            }
            else
                throw new KeyNotFoundException($"{typeof(T).Name} was not register");
        }

        public static Scope Get<T>()
        {
            if (Scopes.ContainsKey(typeof(T)))
                return Scopes[typeof(T)];
            else
                throw new KeyNotFoundException($"Scope {nameof(T)} not register yet");
        }
    }
}