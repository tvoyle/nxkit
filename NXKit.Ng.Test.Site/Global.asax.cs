﻿using System.Web.Http;

namespace NXKit.View.Server.Ng.Test.Site
{

    public class WebApiApplication : 
        System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

    }

}
