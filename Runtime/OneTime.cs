using System.Collections.Generic;
using System;

namespace OneTimeVariable
{
    public static class OneTime
    {
        static Dictionary<Type, Scope> scopes = new Dictionary<Type, Scope>();

        public static void Register<T>() where T : Scope, new()
        {
            if (!scopes.ContainsKey(typeof(T)))
            {
                T scope = new T();
                scope.Init();
                scopes.Add(typeof(T), scope);
            }
            else
                throw new ArgumentException($"{typeof(T).Name} already register");
        }

        public static void Unregsiter<T>() where T : Scope, new()
        {
            if (scopes.ContainsKey(typeof(T)))
            {
                scopes[typeof(T)].Save();
                scopes.Remove(typeof(T));
            }
            else
                throw new KeyNotFoundException($"{typeof(T).Name} was not register");
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