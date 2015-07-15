﻿using Favesrus.DAL.Impl;
using Favesrus.Server.Infrastructure;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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
        public void Configuration(IAppBuilder app) {
            
            app.CreatePerOwinContext<FavesrusDbContext>(FavesrusDbContext.Create);
            app.CreatePerOwinContext<FavesrusUserManager>(FavesrusUserManager.Create);
            app.CreatePerOwinContext<FavesrusRoleManager>(FavesrusRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            //app.UseOAuthBearerTokens(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions
            //{
            //    Provider = new FavesrusAuthProvider(),
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/Authenticate")
            //});
        }
    }
}