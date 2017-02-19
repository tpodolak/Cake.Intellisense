using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cake.MetadataGenerator.Reflection
{
    public class CakeAssemlbyResolver
    {
        private static readonly string[] CakeAttributes = {
            CakeAttributeNames.CakePropertyAlias, CakeAttributeNames.CakeAliasCategory,
            CakeAttributeNames.CakeMethodAlias, CakeAttributeNames.CakeNamespaceImport
        };

        public IEnumerable<Assembly> GetCakeAssemblies(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(val => val.ExportedTypes.Any(IsCakeLikeType));
        }

        private bool IsCakeLikeType(Type type)
        {
            return
                type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Any(
                        method =>
                            method.GetCustomAttributes()
                                .Any(attr => CakeAttributes.Any(name => attr.GetType().Name.EndsWith(name))));
        }
    }
}