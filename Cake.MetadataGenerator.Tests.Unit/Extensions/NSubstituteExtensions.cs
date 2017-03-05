using System.Linq;
using System.Reflection;

namespace Cake.MetadataGenerator.Tests.Unit.Extensions
{
    public static class NSubstituteExtensions
    {
        public static object ProtectedProperty(this object target, string name, params object[] args)
        {
            var type = target.GetType();
            var property = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).Single(prop => prop.Name == name);
            return property.GetMethod.Invoke(target, args);
        }
    }
}