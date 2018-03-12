using Microsoft.Extensions.DependencyModel;
using System.Linq;
using Xunit;

namespace Core.Lib.Modules.Tests
{

    public class AssemblyProviderTests
    {
        [Fact]
        public void AssemblyProvider_GetAssemblies_AreSameWithDependencyModel()
        {
            var expect = DependencyContext.Default.GetDefaultAssemblyNames();

            var actual = DependencyAssemblyProvider.GetAssemblies().Select(x => x.GetName());

            Assert.Equal(expect.Count(), actual.Count());

            Assert.All(expect.OrderBy(x => x.Name)
                .Zip(actual.OrderBy(x => x.Name),
                (l, r) => (l, r)), x => Assert.Equal(x.l.Name,x.r.Name));
                
        }
    }
}
