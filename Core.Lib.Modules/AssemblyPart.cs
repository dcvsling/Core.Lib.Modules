using System.Reflection;
using System.Collections.Generic;

namespace Core.Lib.Modules
{

    public class AssemblyPart : ITypesProvider
    {
        private readonly Assembly _assembly;

        public AssemblyPart(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IEnumerable<TypeInfo> Types
            => _assembly.DefinedTypes;
    }
}
