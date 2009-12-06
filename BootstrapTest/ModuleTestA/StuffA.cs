using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleInterface;

namespace ModuleTestA
{
  public class StuffA : IStuff
  {
    #region IStuff Members
    public void DoSomething()
    {
      Console.WriteLine("StuffA is Doing Something!");
    }
    public string Echo(string text)
    {
      return text;
    }
    #endregion
  }
}
