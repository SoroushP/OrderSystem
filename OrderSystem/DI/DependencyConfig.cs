using Autofac;
using Autofac.Integration.WebApi;
using DapperDataAccess.Repository;
using Domain.DataAccess;
using Domain.DataService;
using Domain.Service;
using Service;
using Service.Security;
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

      builder.RegisterType<OrderService>()
             .As<IOrderService>()
             .InstancePerRequest();

      // Repositories
      var connectionString =
    ConfigurationManager
        .ConnectionStrings["Order"]
        .ConnectionString;

      builder.RegisterType<OrderRepository>()
             .As<IOrderRepository>()
             .WithParameter("connectionString", connectionString)
             .InstancePerRequest();

      var container = builder.Build();

      GlobalConfiguration.Configuration.DependencyResolver =
          new AutofacWebApiDependencyResolver(container);
    }
  }
}