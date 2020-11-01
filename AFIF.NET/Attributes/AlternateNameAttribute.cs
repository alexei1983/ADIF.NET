using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public class AlternateNameAttribute : Attribute {

    public string Name { get; set; }

    public AlternateNameAttribute(string name) {
      Name = name;
      }
    }
  }
