using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using ILogger = NLog.ILogger;

namespace Cake.Intellisense.Reflection
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public Assembly LoadFrom(string assemblyFile)
        {
            return Assembly.LoadFrom(assemblyFile);
        }

        public Assembly Load(string assembly)
        {
            return Assembly.Load(assembly);
        }

        public IEnumerable<Assembly> LoadReferencedAssemblies(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Select(ReflectionOnlyLoad).Where(val => val != null);
        }

        public IEnumerable<Assembly> LoadReferencedAssemblies(string assemblyFile)
        {
            return LoadReferencedAssemblies(LoadFrom(assemblyFile));
        }

        private Assembly ReflectionOnlyLoad(AssemblyName assemblyName)
        {
            try
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName.FullName);
                return assembly ?? Assembly.ReflectionOnlyLoad(assemblyName.FullName);
            }
            catch (Exception e)
            {
                Logger.Warn(e);
                return null;
            }
        }
    }
}