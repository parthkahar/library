using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace emptables.App_Start
{
    public class SwaggerConfig
    {





        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c => c.SingleApiVersion("v1", "First WEB API Demo"))
              .EnableSwaggerUi();

        }

        private static string GetXmlCommentsPath()
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyApi.XML");
        }
    }
}
