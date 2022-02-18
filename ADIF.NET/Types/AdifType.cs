using System;
using System.Globalization;

namespace ADIF.NET.Types {

  public abstract class ADIFType<T> : IADIFType {

    public virtual Type UnderlyingType => typeof(T);

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public virtual string Type { get; }

    public virtual string FormatString { get; }

    public virtual bool IsRange { get; }

    public virtual bool IsEnumeration { get; }

    public virtual bool MultiValue { get; }

    public virtual IFormatProvider FormatProvider { get; } = CultureInfo.CurrentCulture;

    public virtual double MinValue { get; } 

    public virtual double MaxValue { get; }

    }
  }
