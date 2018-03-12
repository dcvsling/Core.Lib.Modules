using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Lib.Modules
{

    public interface IModuleProvider<TModuleContainer> : IModuleProvider
    {
        void Populate(IEnumerable<ITypesProvider> types, TModuleContainer container);
    }
}
