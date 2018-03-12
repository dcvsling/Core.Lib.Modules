using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Lib.Modules
{

    public class TypeFeature
    {
        public List<TypeInfo> Types { get; set; } = new List<TypeInfo>();
        
    }

    public class FilterFeature : TypeFeature
    {
        public Func<TypeInfo, bool> Filter { get; set; } = _ => false;
    }
}
