using System;
using System.Reflection;
using Autofac;
using SampleInterface;

namespace BootstrapTest
{
  class Program
  {
    static void Main(string[] args)
    { //  configure and create MEF container
      var mefContainer = new CatalogConfigurator()
        .AddAssembly(Assembly.GetExecutingAssembly())
        .AddDirectory("Extensions")
        .BuildContainer();
      
      //  now configure and create autofac container
      IContainer container = Bootstrap.Initialize(mefContainer);
      container.Resolve<IStuff>().DoSomething();

      Console.WriteLine("Press ENTER to quit.");
      Console.ReadLine();
    }
  }
}
