using System.Linq;
using Microsoft.Extensions.DependencyModel;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Core.Lib.Modules
{

    public interface ITypesProvider
    {
        IEnumerable<TypeInfo> Types { get; }
    }
}
