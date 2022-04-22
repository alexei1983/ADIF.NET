using System;

namespace ADIF.NET {

  /// <summary>
  /// Generates serial numbers for contest QSOs.
  /// </summary>
  public class SerialNumberGenerator {

    /// <summary>
    /// Name of the serial number generator.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Starting serial number.
    /// </summary>
    public int Start { get; private set; }

    /// <summary>
    /// Current serial number.
    /// </summary>
    public int Current { get; private set; } 

    /// <summary>
    /// Length of the serial number padded on the left with zeroes.
    /// </summary>
    public int StringLength { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="SerialNumberGenerator"/> class.
    /// </summary>
    public SerialNumberGenerator() : this(1, 1) { }

    /// <summary>
    /// Creates a new instance of the <see cref="SerialNumberGenerator"/> class.
    /// </summary>
    /// <param name="start">Starting serial number.</param>
    /// <param name="current">Current serial number.</param>
    public SerialNumberGenerator(int start, int current)
    {
      if (start <= 0)
        throw new ArgumentException("Starting serial number must be greater than zero.", nameof(start));

      if (current <= 0)
        throw new ArgumentException("Current serial number must be greater than zero.", nameof(start));

      Start = start;
      Current = current;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SerialNumberGenerator"/> class.
    /// </summary>
    /// <param name="start">Starting serial number.</param>
    public SerialNumberGenerator(int start) : this(start, start)
    {
    }

    /// <summary>
    /// Retrieves the next serial number.
    /// </summary>
    public int Next()
    {
      return Current++;
    }

    /// <summary>
    /// Retrieves the next serial number as a string.
    /// </summary>
    public string NextString()
    {
      var nextStr = Next().ToString();
      return StringLength > 0 ? nextStr.Length > StringLength ? nextStr : nextStr.PadLeft(StringLength, '0') : nextStr;
    }
  }
}
