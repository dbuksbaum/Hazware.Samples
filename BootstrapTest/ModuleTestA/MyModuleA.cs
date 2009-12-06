using System.ComponentModel.Composition;
using Autofac;
using SampleInterface;

namespace ModuleTestA
{
  [Export(typeof(IModule))]
  public class MyModuleA : Autofac.Builder.Module
  {
    protected override void Load(Autofac.Builder.ContainerBuilder builder)
    {
      builder.Register<StuffA>().As<IStuff>();
    }
  }
}
