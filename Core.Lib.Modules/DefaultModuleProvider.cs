using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Lib.Modules
{

    public class DefaultModuleProvider : IModuleProvider<FilterFeature>
    {
        public void Populate(IEnumerable<ITypesProvider> types, FilterFeature container)
            => types.SelectMany(x => x.Types)
                .Where(container.Filter)
                .Aggregate(container, ModuleStoreExtensions.Populate);
    }
}
