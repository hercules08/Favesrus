
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Infrastructure.Impl
{
    public static class AutoMapperConfigurator
    {
        public static void Configure(IEnumerable<IAutoMapperTypeConfigurator> autoMapperTypeConfigurations)
        {
            autoMapperTypeConfigurations.ToList().ForEach(x => x.Configure());
        }
    }
}