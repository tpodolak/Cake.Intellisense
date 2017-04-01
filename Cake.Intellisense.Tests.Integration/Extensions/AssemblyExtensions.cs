using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Cake.Intellisense.Constants.CakeAttributeNames;
using static Cake.Intellisense.Constants.CakeEngineNames;

namespace Cake.Intellisense.Tests.Integration.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetCakeAliasTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(val => val.GetCustomAttributes()
                    .Any(attr => attr.GetType().FullName == CakeAliasCategoryFullName));
        }

        public static IEnumerable<Type> GetCakeScriptHostTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(val => val.FullName == ScripHostFullName);
        }
    }
}