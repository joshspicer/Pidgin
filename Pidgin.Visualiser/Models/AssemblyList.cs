using System.Collections.Generic;
using System.Reflection;

namespace Pidgin.Visualiser.Models
{
    static class AssemblyList
    {
        private static readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();
        public static IEnumerable<Assembly> LoadedAssemblies => _assemblies;

        public static void Load(string assemblyFile)
        {
            var assembly = Assembly.LoadFrom(assemblyFile);
            _assemblies.Add(assembly);
        }
    }
}