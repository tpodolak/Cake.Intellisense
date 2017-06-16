using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSubstitute;
using NSubstitute.Core;

namespace Cake.Intellisense.Tests.Unit.Common
{
    public class Test<T> where T : class
    {
        private readonly Dictionary<Type, object> _container = new Dictionary<Type, object>();

        public T Subject { get; }

        public Test(params MockInfo[] mocksInfo)
        {
            var parameters = GetMostComplexConstructor().GetParameters();
            mocksInfo = mocksInfo ?? new MockInfo[0];

            foreach (var parameterInfo in parameters)
            {
                var instance = mocksInfo.SingleOrDefault(mock => mock.Type == parameterInfo.ParameterType)?.Instance ??
                               CreateInstance(parameterInfo.ParameterType);

                Use(parameterInfo.ParameterType, instance);
            }

            Subject = (T)Activator.CreateInstance(typeof(T), _container.Values.ToArray());
        }

        public TDependency Get<TDependency>()
        {
            object dependency;
            var dependencyType = typeof(TDependency);
            if (_container.TryGetValue(dependencyType, out dependency))
                return (TDependency)dependency;

            throw new InvalidOperationException($"Unable to resolve dependency {dependencyType}");
        }

        public TDependency Use<TDependency>(params object[] constructorArgs)
        {
            var instance = (TDependency)CreateInstance(typeof(TDependency), constructorArgs);

            return Use(instance);
        }

        public object Use(Type dependencyType, object instance)
        {
            if (_container.ContainsKey(dependencyType))
                throw new InvalidOperationException($"Dependency {dependencyType} is already registerd");

            _container.Add(dependencyType, instance);
            return instance;
        }

        private TDependency Use<TDependency>(TDependency instance)
        {
            return (TDependency)Use(typeof(TDependency), instance);
        }

        private object CreateInstance(Type type, params object[] constructorArgs)
        {
            if (type.IsPrimitive)
                return Activator.CreateInstance(type);
            if (type == typeof(string))
                return null;

            return type.IsInterface
                ? Substitute.For(new[] { type }, constructorArgs)
                : SubstitutionContext.Current.SubstituteFactory.CreatePartial(new[] { type }, constructorArgs);
        }

        private ConstructorInfo GetMostComplexConstructor()
        {
            return typeof(T).GetConstructors().OrderBy(ctor => ctor.GetParameters().Length).Last();
        }
    }
}