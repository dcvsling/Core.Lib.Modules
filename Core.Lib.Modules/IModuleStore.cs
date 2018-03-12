using System.Collections.Generic;

namespace Core.Lib.Modules
{

    public interface IModuleStore
    {
        void Populate<TModuleContainer>(TModuleContainer container);
    }
}
