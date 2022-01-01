using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public class DeprecatedTagAttribute : Attribute {

    public bool Deprecated { get; set; }

    public string AlternateTagName { get; set; }

    public DeprecatedTagAttribute(bool deprecated)  {
      Deprecated = deprecated;
      }

    public DeprecatedTagAttribute(string alternateTagName) : this(true)
    {
      AlternateTagName = alternateTagName;
    }

    public DeprecatedTagAttribute() : this(true) { }
    }
  }
