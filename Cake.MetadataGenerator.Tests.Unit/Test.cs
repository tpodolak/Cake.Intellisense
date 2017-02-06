namespace Cake.MetadataGenerator.Tests.Unit
{
    public abstract class Test<T> where T : class
    {
        private readonly AutoSubstitute<T> autoSubstitute = new AutoSubstitute<T>();

        protected T Subject { get; }

        protected Test()
        {
            Subject = autoSubstitute.Subject;
        }

        protected TDependency FakeOf<TDependency>()
        {
            return autoSubstitute.Get<TDependency>();
        }

        protected TDependency Use<TDependency>(TDependency instance = default(TDependency))
        {
            return autoSubstitute.Use(instance);
        }
    }
}