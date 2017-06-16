using System;

namespace Cake.Intellisense.Tests.Unit.Common
{
    public class MockInfo
    {
        public Type Type { get; }

        public object Instance { get; }

        public MockInfo(Type type, object instance)
        {
            Type = type;
            Instance = instance;
        }
    }
}