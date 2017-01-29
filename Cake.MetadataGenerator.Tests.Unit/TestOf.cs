using NSubstituteAutoMocker;

namespace Cake.MetadataGenerator.Tests.Unit
{
    public abstract class TestOf<T> where T : class
    {
        private readonly NSubstituteAutoMocker<T> autoMocker = new NSubstituteAutoMocker<T>();

        protected T Subject { get; }

        protected TestOf()
        {
            Subject = autoMocker.ClassUnderTest;
        }

        protected TMock FakeOf<TMock>()
        {
            return autoMocker.Get<TMock>();
        }
    }
}