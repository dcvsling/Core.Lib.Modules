using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Microsoft.Extensions.Options;

namespace Core.Lib.Modules.Tests
{
    public class ModuleStoreTests
    {
        [Fact]
        public void PopulateTypeFeature_ByNamespaceTypeProvider_WillGetAllInterface()
        {
            var provider = new ServiceCollection()
                .AddModule<NamespaceTypeProvider>()
                .BuildServiceProvider();
            var expect = typeof(IModuleStore).Assembly.DefinedTypes
                .Where(x => x.IsInterface)
                .ToList();
            
            var feature = provider.GetRequiredService<IModuleStore>()
                .Populate();

            Assert.Equal(expect.Count, feature.Types.Count);
            Assert.All(feature.Types, type => expect.Contains(type));
        }

        [Fact]
        public void PopulateTypeFeature_ByFilter_WillGetAllInterface()
        {
            var provider = new ServiceCollection()
                .AddTypeModule(nameof(PopulateTypeFeature_ByFilter_WillGetAllInterface),
                    x => x.IsInterface 
                        && x.Assembly.FullName == typeof(IModuleStore).Assembly.FullName)
                .BuildServiceProvider();
            var expect = typeof(IModuleStore).Assembly.DefinedTypes
                .Where(x => x.IsInterface)
                .ToList();

            var store = provider.GetRequiredService<IModuleStore>();
            var feature = provider.GetRequiredService<IOptionsSnapshot<FilterFeature>>().Get(nameof(PopulateTypeFeature_ByFilter_WillGetAllInterface));

            store.Populate(feature);

            Assert.Equal(expect.Count, feature.Types.Count);
            Assert.All(feature.Types, type => expect.Contains(type));
        }
    }

    public class NamespaceTypeProvider : IModuleProvider<TypeFeature>
    {
        public void Populate(IEnumerable<ITypesProvider> types, TypeFeature container)
            => types.SelectMany(x => x.Types)
                .Where(x => x.IsInterface && x.Assembly.FullName == typeof(IModuleStore).Assembly.FullName)
                .Aggregate(container, Populate);


        private TypeFeature Populate(TypeFeature feature, TypeInfo type)
        {
            feature.Types.Add(type);
            return feature;
        }
    }
}
