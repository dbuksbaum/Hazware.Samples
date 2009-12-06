using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Autofac;
using Autofac.Builder;

namespace BootstrapTest
{
  public sealed class Bootstrap
  {
    #region Static Fields
    private static readonly object _lockObject = new object();
    private static Bootstrap _instance;
    #endregion 

    #region Properties
    [ImportMany(typeof(IModule))]
    internal IModule[] Modules { get; set; }
    #endregion

    #region Constructors
    private Bootstrap()
    {
    }
    #endregion

    public static IContainer Initialize(CompositionContainer container)
    {
      return Initialize(container, false);
    }
    public static IContainer Initialize(CompositionContainer container, bool resetSingleton)
    {
      Bootstrap bootStrap;

      lock (_lockObject)
      { //  only work on bootstrap instance in one thread at a time
        if (resetSingleton || _instance == null)
        { //  create and MEF initialize the bootstrap object
          bootStrap = new Bootstrap();
          container.SatisfyImportsOnce(bootStrap);
          //  store in singleton
          _instance = bootStrap;
        }
        //  use current instance
        bootStrap = _instance;
      }

      //  now initialize autofac
      ContainerBuilder builder = new ContainerBuilder();
      
      //  add support for implicit collections
      builder.RegisterModule(new Autofac.Modules.ImplicitCollectionSupportModule());
      
      //  add all MEF-ed modules
      foreach (Module module in bootStrap.Modules)
        builder.RegisterModule(module);

      //  create the container
      return builder.Build();
    }
  }
}
