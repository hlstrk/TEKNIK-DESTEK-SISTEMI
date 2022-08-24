using TeknikServis.Bll;
using TeknikServis.Cache;
using TeknikServis.MvcUI.App_Start;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Web.Http;
using AutoMapper;

namespace TeknikServis.MvcUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["TeknikServisContext"].ConnectionString;
        protected void Application_Start()
        {
            SqlDependency.Start(con);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalFilters.Filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            CacheProvider.Instance = new DefaultCacheProvider();
            AutoMapperConfiguration.Configure();
            CacheFonsiyon fonksiyon = new CacheFonsiyon();
            fonksiyon.CacheTemizle();
            fonksiyon.CacheOlustur();
        }


        protected void Session_Start(object sender, EventArgs e)
        {
            NotificationComponent NC = new NotificationComponent();
            var currentTime = DateTime.Now;
            HttpContext.Current.Session["LastUpdated"] = currentTime;
            NC.RegisterNotification(currentTime);
        }


        protected void Application_End()
        {
            //here we will stop Sql Dependency
            SqlDependency.Stop(con);
        }

        public class AutoMapperConfiguration
        {
            public static void Configure()
            {
                Mapper.Initialize(config => config.AddProfile(new MappingProfile()));
            }
        }
    }
}
