using System;
using System.Linq;
using System.Reflection;

namespace Cake.Intellisense.Tests.Integration.Extensions
{
    public static class MethodInfoExtensions
    {
        public static bool IsSameCakeAliasMethod(this MethodInfo sourceMethod, MethodInfo emitedMethod)
        {
            var sourceMethodName = sourceMethod.ToString();
            var emitedMethodName = emitedMethod.ToString();
            if (sourceMethod.GetParameters().Length > 0)
            {
                var firstParam = sourceMethod.GetParameters().First().ParameterType.FullName;
                sourceMethodName = sourceMethodName.Replace($"({firstParam}, ", "(")
                    .Replace($"({firstParam}", "(");
            }

            return sourceMethodName.Equals(emitedMethodName, StringComparison.InvariantCulture);
        }

        public static bool IsSameCakeEngineMethod(this MethodInfo sourceMethod, MethodInfo emitedMethod)
        {
            return sourceMethod.ToString() == emitedMethod.ToString();
        }
    }
}