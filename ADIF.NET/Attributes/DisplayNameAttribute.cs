using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public class DisplayNameAttribute : Attribute {

    public string Name { get; set; }

    public DisplayNameAttribute(string name) {
      Name = name;
      }
    }
  }
