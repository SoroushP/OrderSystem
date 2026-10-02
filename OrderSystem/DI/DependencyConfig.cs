using Autofac;
using Autofac.Integration.WebApi;
using DapperDataAccess.Connection;
using DapperDataAccess.Repository;
using Domain.DataAccess;
using Domain.DataService;
using Domain.Service;
using Serilog;
using Service;
using Service.Logging;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace WebAPI.DI
{
  public static class DependencyConfig
  {
    public static void Register()
    {
      var builder = new ContainerBuilder();

      // Register Web API controllers
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      // Services
      builder.RegisterType<SerilogAppLogger>()
       .As<IAppLogger>()
       .SingleInstance();

      builder.RegisterInstance(Log.Logger)
             .As<ILogger>()
             .SingleInstance();

      builder.RegisterType<OrderService>()
             .As<IOrderService>()
             .InstancePerRequest();

      // Repositories
      var connectionString =
    ConfigurationManager
        .ConnectionStrings["Order"]
        .ConnectionString;

      builder.RegisterType<SqlConnectionFactory>()
       .AsSelf()
       .WithParameter("connectionString", connectionString)
       .SingleInstance();

      builder.RegisterType<OrderRepository>()
             .As<IOrderRepository>()
             .InstancePerRequest();

      var container = builder.Build();

      GlobalConfiguration.Configuration.DependencyResolver =
          new AutofacWebApiDependencyResolver(container);
    }
  }
}