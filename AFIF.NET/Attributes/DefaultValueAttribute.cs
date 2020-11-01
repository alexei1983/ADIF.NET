using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class DefaultValueAttribute : Attribute {

    public string DefaultValue { get; set; }

    public DefaultValueAttribute(string defaultValue) {
      DefaultValue = defaultValue;
      }

    public DefaultValueAttribute() { }
    }
  }
