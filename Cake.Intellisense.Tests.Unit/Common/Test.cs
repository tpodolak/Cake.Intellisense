using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoreLinq;
using NSubstitute;
using NSubstitute.Core;

namespace Cake.MetadataGenerator.Tests.Unit.Common
{
    public class Test<T> where T : class
    {
        private readonly Dictionary<Type, object> container = new Dictionary<Type, object>();

        public T Subject { get; }

        public Test()
        {
            var parameters = GetMostComplexConstructor().GetParameters();

            // allow to register custom instances via virtual call to CreateInstance
            var localContainer = parameters.ToDictionary(parameterInfo => parameterInfo.ParameterType, parameterInfo => CreateInstance(parameterInfo.ParameterType));
            localContainer.ForEach(pair => container.Add(pair.Key, pair.Value));
            Subject = (T)Activator.CreateInstance(typeof(T), localContainer.Values.ToArray());
        }

        public TDependency Get<TDependency>()
        {
            object dependency;
            var dependencyType = typeof(TDependency);
            if (container.TryGetValue(dependencyType, out dependency))
                return (TDependency)dependency;

            throw new InvalidOperationException($"Unable to resolve dependency {dependencyType}");
        }

        public object Use(Type dependencyType, params object[] constructorArgs)
        {
            if (container.ContainsKey(dependencyType))
                throw new InvalidOperationException($"Dependency {dependencyType} is already registerd");

            var instance = CreateInstance(dependencyType, constructorArgs);
            container.Add(dependencyType, instance);
            return instance;
        }

        public TDependency Use<TDependency>(params object[] constructorArgs)
        {
            return (TDependency)Use(typeof(TDependency), constructorArgs);
        }

        public TDependency Use<TDependency>(TDependency instance)
        {
            var dependencyType = typeof(TDependency);
            if (container.ContainsKey(dependencyType))
                throw new InvalidOperationException($"Dependency {dependencyType} is already registerd");

            container.Add(dependencyType, instance);
            return instance;
        }

        public virtual object CreateInstance(Type type, params object[] constructorArgs)
        {
            if (type.IsPrimitive)
                return Activator.CreateInstance(type);
            if (type == typeof(string))
                return null;

            return type.IsInterface ? Substitute.For(new[] { type }, constructorArgs) : SubstitutionContext.Current.SubstituteFactory.CreatePartial(new[] { type }, constructorArgs);
        }

        private ConstructorInfo GetMostComplexConstructor()
        {
            return typeof(T).GetConstructors().OrderBy(ctor => ctor.GetParameters().Length).Last();
        }
    }
}