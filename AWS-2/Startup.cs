using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using AWS_2.Models;

[assembly: OwinStartup(typeof(AWS_2.Startup))]

namespace AWS_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration httpConfig = new HttpConfiguration();

        

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);
            ConfigureAuth(app);
        }

     

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
