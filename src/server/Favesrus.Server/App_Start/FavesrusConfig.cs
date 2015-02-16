using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Favesrus.Server.FavesrusConfig))]

namespace Favesrus.Server
{
    public class FavesrusConfig
    {
        public void Configuration(IAppBuilder app) { }
    }
}