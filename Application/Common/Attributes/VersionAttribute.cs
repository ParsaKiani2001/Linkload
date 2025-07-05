using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VersionAttribute:ActionFilterAttribute
    {
        private readonly bool _version;
        public VersionAttribute(bool version = true)
        {
            _version = version;
        }
    }
}
