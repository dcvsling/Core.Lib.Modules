using System.ComponentModel.Design;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Collections.Generic;

namespace Core.Lib.Modules
{

    public static class DependencyAssemblyProvider
    {
        public static IEnumerable<Assembly> GetAssemblies()
            => DependencyContext.Default.GetDefaultAssemblyNames()
                .Select(Assembly.Load);
    }
}
