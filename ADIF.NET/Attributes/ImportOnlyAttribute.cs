using System;

namespace ADIF.NET.Attributes {

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
  public class ImportOnlyAttribute : Attribute {

    public bool ImportOnly { get; set; }

    public ImportOnlyAttribute(bool importOnly) {
      ImportOnly = importOnly;
      }

    public ImportOnlyAttribute() : this(true) {
      }
    }
  }
