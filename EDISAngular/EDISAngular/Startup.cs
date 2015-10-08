using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(EDISAngular.Startup))]

namespace EDISAngular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //var webApiConfiguration = new HttpConfiguration();
            //WebApiConfig.Register(webApiConfiguration);
            //app.UseWebApi(webApiConfiguration);



        }
    }
}
