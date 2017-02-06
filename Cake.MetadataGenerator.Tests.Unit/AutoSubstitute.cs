using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoreLinq;
using NSubstitute;
using NSubstitute.Core;

namespace Cake.MetadataGenerator.Tests.Unit
{
    public class AutoSubstitute<T> where T : class
    {
        private readonly Dictionary<Type, object> container = new Dictionary<Type, object>();

        public T Subject { get; }

        public AutoSubstitute()
        {
            var parameters = GetMostComplexConstructor().GetParameters();

            foreach (var parameterInfo in parameters)
            {
                container.Add(parameterInfo.ParameterType, CreateInstance(parameterInfo.ParameterType));
            }

            Subject = (T)Activator.CreateInstance(typeof(T), container.Values.ToArray());
        }

        public TDependency Get<TDependency>()
        {
            object dependency;
            var dependencyType = typeof(TDependency);
            if (container.TryGetValue(dependencyType, out dependency))
                return (TDependency)dependency;

            throw new InvalidOperationException($"Unable to resolve dependency {dependencyType}");
        }

        public TDependency Use<TDependency>(TDependency instane = default(TDependency))
        {
            var dependencyType = typeof(TDependency);
            if (container.ContainsKey(dependencyType))
                throw new InvalidOperationException($"Dependency {dependencyType} is already registerd");

            instane = instane != null ? instane : (TDependency)CreateInstance(dependencyType);
            container.Add(dependencyType, instane);
            return instane;
        }

        private object CreateInstance(Type type)
        {
            if (type.IsPrimitive)
                return Activator.CreateInstance(type);
            if (type == typeof(string))
                return null;

            return type.IsInterface ? Substitute.For(new[] { type }, null) : SubstitutionContext.Current.SubstituteFactory.CreatePartial(new[] { type }, null);
        }

        private ConstructorInfo GetMostComplexConstructor()
        {
            return typeof(T).GetConstructors().GroupBy(ctor => ctor.GetParameters().Length).MaxBy(group => group.Key).First();
        }
    }
}