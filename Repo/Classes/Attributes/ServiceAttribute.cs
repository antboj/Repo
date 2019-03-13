using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Classes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public ServiceEnumAttributeValues Value { get; }

        public ServiceAttribute(ServiceEnumAttributeValues value = ServiceEnumAttributeValues.Transient)
        {
            Value = value;
        }
    }
}
