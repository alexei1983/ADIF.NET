using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value must be selected from a list 
  /// of valid options.
  /// </summary>
  public class RestrictedEnumerationTag : EnumerationTag, ITag {

    public override bool RestrictOptions => true;

    public override bool ValidateValue(object value) {    
      return base.ValidateValue(value) && 
             this.Options.IsValid(value.ToString());
      }
    }
  }
