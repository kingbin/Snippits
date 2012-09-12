public class ServiceResolverAdapter : IDependencyResolver
{
    private readonly System.Web.Mvc.IDependencyResolver dependencyResolver;

    public ServiceResolverAdapter(System.Web.Mvc.IDependencyResolver dependencyResolver)
    {
        if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");
        this.dependencyResolver = dependencyResolver;
    }

    public object GetService(Type serviceType)
    {
        return dependencyResolver.GetService(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return dependencyResolver.GetServices(serviceType);
    }
}

public static class ServiceResolverExtensions
{
    public static IDependencyResolver ToServiceResolver(this System.Web.Mvc.IDependencyResolver dependencyResolver)
    {
        return new ServiceResolverAdapter(dependencyResolver);
    }
}


// Thanks to http://haacked.com/archive/2012/03/11/itrsquos-the-little-things-about-asp-net-mvc-4.aspx

//// Ninject bindings
//private static void RegisterServices(IKernel kernel)
//{
//    kernel.Bind<ISomeService>().To<SomeService>();
//}

//public class MyApiController : ApiController
//{
//  public MyApiController(ISomeService service)
//  {
//  }
//  // Other code...
//}


// Support DI in ApiController inherited controllers 
//public static void Start()
//{
  // ...Pre-existing lines of code...

//  GlobalConfiguration.Configuration.ServiceResolver
//  .SetResolver(DependencyResolver.Current.ToServiceResolver());
//}
