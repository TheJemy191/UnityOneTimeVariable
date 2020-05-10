using System.Collections.Generic;
using System;

namespace OneTimeVariable
{
    public abstract class Scope
    {
        protected HashSet<string> oneTimes = new HashSet<string>();

        public virtual void Init() { }

        public virtual void Save() { }
        protected virtual void Load() { }

        public virtual bool this[string key, bool isSeeking = false]
        {
            get
            {
                if (isSeeking)
                    return !oneTimes.Contains(key);

                if (oneTimes.Contains(key))
                    return false;
                else
                    oneTimes.Add(key);

                return true;
            }
        }

        public bool this[Type key] => this[key.ToString()];
        public bool this[Type typeKey, string key] => this[$"{key}_{typeKey.ToString()}"];
    }
}