using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
  public class EnumerationAttribute : Attribute {

    public EnumerationAttribute() {
      }
    }
  }
