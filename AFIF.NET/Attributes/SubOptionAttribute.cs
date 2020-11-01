using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
  public class SubOptionAttribute : Attribute {

    public string SubOption { get; set; }

    public SubOptionAttribute(string subOption) {
      SubOption = subOption;
      }
    }
  }
