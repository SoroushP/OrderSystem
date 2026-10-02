using Serilog;
using System;
using System.IO;
using System.Web.Http;
using WebAPI.App_Start;
using WebAPI.DI;

namespace OrderSystem
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "logs",
                        "app-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    shared: true)
                .CreateLogger();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      DependencyConfig.Register();
    }
    protected void Application_End()
    {
      Log.CloseAndFlush();
    }
  }
}
