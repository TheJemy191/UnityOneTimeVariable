namespace OneTimeVariable
{
    public abstract class ScopeNested<T> : Scope where T : Scope, new()
    {
        protected T NestedScope = new T();

        public override void Init()
        {
            NestedScope.Init();
        }

        public override bool this[string key, bool isSeeking = false] => NestedScope[key, isSeeking];
        public override bool this[System.Type type, string key, bool isSeeking = false] => NestedScope[type, key, isSeeking];
    }
}