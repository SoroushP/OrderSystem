using System.Web.Http;
using WebAPI.App_Start;
using WebAPI.DI;

namespace OrderSystem
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
      DependencyConfig.Register();
    }
  }
}
