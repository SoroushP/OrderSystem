using System.Web.Http;
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
