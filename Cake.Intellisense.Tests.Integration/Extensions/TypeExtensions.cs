using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Cake.Intellisense.Constants.CakeAttributeNames;
using static Cake.Intellisense.Constants.CakeEngineNames;

namespace Cake.Intellisense.Tests.Integration.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<MethodInfo> GetCakeAliasMethods(this Type sourceType)
        {
            return sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(method => method.GetCustomAttributes()
                    .Any(attr => attr.GetType().FullName == CakeMethodAliasFullName));
        }

        public static IEnumerable<MethodInfo> GetCakeScriptEngineMethods(this Type type)
        {
            return type.FullName == ScripHostFullName
                ? type.GetMethods().Where(val => !val.IsSpecialName && val.DeclaringType != typeof(object))
                : Enumerable.Empty<MethodInfo>();
        }

        public static IEnumerable<MethodInfo> GetCakeMetadataPropertes(this Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(val => val.IsSpecialName);
        }

        public static IEnumerable<MethodInfo> GetCakeMetadataMethods(this Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(val => !val.IsSpecialName);
        }

        public static IEnumerable<MethodInfo> GetCakeProperties(this Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                       .Where(val => val.GetCustomAttributes().Any(x => x.GetType().FullName == CakePropertyAliasFullName));
        }
    }
}