namespace OneTimeVariable
{
    public abstract class ScopeNested<T> : Scope where T : Scope, new()
    {
        protected T nestedScope = new T();

        public override void Init()
        {
            nestedScope.Init();
        }

        public override bool this[string key, bool isSeeking = false] => nestedScope[key, isSeeking];
        public override bool this[System.Type type, string key, bool isSeeking = false] => nestedScope[type, key, isSeeking];
    }
}