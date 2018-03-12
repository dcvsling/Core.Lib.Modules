
using System.Linq;
using Core.Lib.Modules;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModulesExtensions
    {
        public static IServiceCollection AddModule<TModuleProvider>(this IServiceCollection services)
            where TModuleProvider : class, IModuleProvider
            => services.AddModuleCore()
                .AddScoped<IModuleProvider, TModuleProvider>();

        public static IServiceCollection AddModule<TModuleProvider>(this IServiceCollection services,Func<IServiceProvider,TModuleProvider> factory)
            where TModuleProvider : class, IModuleProvider
            => services.AddModuleCore()
                .AddScoped<IModuleProvider, TModuleProvider>(factory);
        public static IServiceCollection AddTypeModule(
            this IServiceCollection services,
            string name,
            Func<TypeInfo,bool> filter)
            => services.AddModuleCore()
                .Configure<FilterFeature>(name,o => o.Filter = filter);

        private static IServiceCollection AddModuleCore(this IServiceCollection services)
            => services.Any(x => x.ServiceType == typeof(ModuleMarkup))
                ? services
                : services.AddSingleton<IEnumerable<ITypesProvider>>(_ => DependencyAssemblyProvider.GetAssemblies().Select(x => new AssemblyPart(x)))
                    .AddScoped<IModuleStore, ModuleStore>()
                    .AddScoped<IModuleProvider,DefaultModuleProvider>()
                    .AddSingleton<ModuleMarkup>();

        private class ModuleMarkup { }
    }

    
}
