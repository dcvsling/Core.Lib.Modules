using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Core.Lib.Modules
{

    public class ModuleStore : IModuleStore
    {
        private readonly IEnumerable<IModuleProvider> _providers;
        private readonly IEnumerable<ITypesProvider> _types;

        public ModuleStore(
            IEnumerable<IModuleProvider> providers,
            IEnumerable<ITypesProvider> types)
        {
            _providers = providers;
            _types = types;
        }

        public void Populate<TModuleContainer>(TModuleContainer container)
            => _providers.OfType<IModuleProvider<TModuleContainer>>()
                .Aggregate(container, Populate);

        private TModuleContainer Populate<TModuleContainer>(
            TModuleContainer container, 
            IModuleProvider<TModuleContainer> provider)
        {
            provider.Populate(_types, container);
            return container;
        }
    }
}
