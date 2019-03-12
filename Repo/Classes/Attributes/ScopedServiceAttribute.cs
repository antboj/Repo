using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Classes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScopedServiceAttribute : Attribute
    {
    }
}
