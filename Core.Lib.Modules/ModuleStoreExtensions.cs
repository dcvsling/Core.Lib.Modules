using System.Reflection;
namespace Core.Lib.Modules
{

    public static class ModuleStoreExtensions
    {
        public static TFeature Populate<TFeature>(this IModuleStore store)
            where TFeature : new()
        {
            var feature = new TFeature();
            store.Populate(feature);
            return feature;
        }
        public static TypeFeature Populate(this IModuleStore store)
            => store.Populate<TypeFeature>();

        internal static T Populate<T>(T feature, TypeInfo type)
            where T : TypeFeature
        {
            feature.Types.Add(type);
            return feature;
        }
    }
}
