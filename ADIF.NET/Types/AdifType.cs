using System;
using System.Globalization;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents an ADIF data type.
  /// </summary>
  /// <typeparam name="T">The <seealso cref="System.Type"/> of the ADIF type's underlying value.</typeparam>
  public abstract class ADIFType<T> : IADIFType {

    /// <summary>
    /// The expected <seealso cref="System.Type"/> of the ADIF type's underlying value.
    /// </summary>
    public virtual Type UnderlyingType => typeof(T);

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public virtual string Type { get; }

    /// <summary>
    /// ADIF type name.
    /// </summary>
    public virtual string TypeName { get; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string FormatString { get; }

    /// <summary>
    /// Whether or not the type is a range.
    /// </summary>
    public virtual bool IsRange { get; }

    /// <summary>
    /// Whether or not the type is an enumeration.
    /// </summary>
    public virtual bool IsEnumeration { get; }

    /// <summary>
    /// Whether or not the type is multivalued.
    /// </summary>
    public virtual bool MultiValue { get; }

    /// <summary>
    /// 
    /// </summary>
    public virtual IFormatProvider FormatProvider { get; } = CultureInfo.CurrentCulture;

    /// <summary>
    /// Minimum numeric value for the type.
    /// </summary>
    public virtual double MinValue { get; }

    /// <summary>
    /// Maximum numeric value for the type.
    /// </summary>
    public virtual double MaxValue { get; }

    }
  }
