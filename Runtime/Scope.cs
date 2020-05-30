using System;
using System.Collections.Generic;

namespace OneTimeVariable
{
    public abstract class Scope
    {
        protected HashSet<string> OneTimes = new HashSet<string>();

        public virtual void Init() { }

        public virtual void Save() { }
        protected virtual void Load() { }

        public virtual bool this[string key, bool isSeeking = false]
        {
            get
            {
                if (isSeeking)
                    return !OneTimes.Contains(key);

                if (OneTimes.Contains(key))
                    return false;
                
                OneTimes.Add(key);

                return true;
            }
        }

        public virtual bool this[Type typeKey, string key, bool isSeeking = false] => this[$"{key}_{typeKey}", isSeeking];
    }
}