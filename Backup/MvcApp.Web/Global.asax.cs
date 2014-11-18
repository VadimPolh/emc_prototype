using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using MvcApp.Bootstrap.IoC;

namespace MvcApp.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            InitializeContainer();
        }

        public void InitializeContainer()
        {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}