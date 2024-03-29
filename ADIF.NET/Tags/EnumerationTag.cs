﻿
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is either free-form text or selected 
  /// from a list of options.
  /// </summary>
  public class EnumerationTag : Tag<string>, ITag {

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFEnumerationType();

    /// <summary>
    /// Creates a new instance of the <see cref="EnumerationTag"/> class.
    /// </summary>
    public EnumerationTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="EnumerationTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EnumerationTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new instance of the <see cref="EnumerationTag"/> class.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public EnumerationTag(ADIFEnumerationValue enumValue) : base(enumValue?.Code) { }

    }
  }
