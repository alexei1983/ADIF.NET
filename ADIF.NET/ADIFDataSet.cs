using System;
using System.Globalization;
using System.IO;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Represents an ADIF data set, including header and QSOs.
  /// </summary>
  public class ADIFDataSet : IFormattable {

    /// <summary>
    /// Header text used on the first line of an emitted ADIF file.
    /// </summary>
    public string HeaderText
    {
      get { return headerText; }

      set
      {
        if (string.IsNullOrEmpty(value))
          headerText = null;
        else
          headerText = value;
      }
    }

    /// <summary>
    /// Header tags.
    /// </summary>
    public ADIFHeader Header { get; set; }

    /// <summary>
    /// QSO collection.
    /// </summary>
    public ADIFQSOCollection QSOs { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="outputFile"></param>
    /// <param name="flags"></param>
    public void ToADIF(string outputFile, EmitFlags flags = EmitFlags.None)
    {
      if (string.IsNullOrEmpty(outputFile))
        throw new ArgumentException("Output file is required.", nameof(outputFile));

      var adif = ToADIF(flags);

      if (!string.IsNullOrEmpty(adif))
        File.WriteAllText(outputFile, adif);
    }

    /// <summary>
    /// 
    /// </summary>
    public string ToADIF(EmitFlags flags = EmitFlags.None)
    {
      var formatString = (flags & EmitFlags.LowercaseTagNames) == EmitFlags.LowercaseTagNames ? "a" : "A";

      if ((flags & EmitFlags.MirrorOperatorAndStationCallSign) == EmitFlags.MirrorOperatorAndStationCallSign)
      {
        if (QSOs != null)
        {
          for (var q = 0; q < QSOs.Count; q++)
          {
            if (QSOs[q] != null)
            {
              var qso = QSOs[q];

              if (qso.Contains(TagNames.Operator) && !qso.Contains(TagNames.StationCallSign))
              {
                var stationCallSignTag = TagFactory.TagFromName(TagNames.StationCallSign);
                var operatorTag = qso[qso.IndexOf(TagNames.Operator)];
                stationCallSignTag.SetValue(operatorTag.Value);
                QSOs[q].Insert(QSOs[q].IndexOf(TagNames.Operator), stationCallSignTag);
              } else if (!qso.Contains(TagNames.Operator) && qso.Contains(TagNames.StationCallSign))
              {
                var operatorTag = TagFactory.TagFromName(TagNames.Operator);
                var stationCallSignTag = qso[qso.IndexOf(TagNames.StationCallSign)];
                operatorTag.SetValue(stationCallSignTag.Value);
                QSOs[q].Insert(QSOs[q].IndexOf(TagNames.StationCallSign), operatorTag);
              }
            }
          }
        }

        if ((flags & EmitFlags.AddCreatedTimestampIfNotPresent) == EmitFlags.AddCreatedTimestampIfNotPresent)
        {
          if (Header == null)
            Header = new ADIFHeader();

          if (!Header.Contains(TagNames.CreatedTimestamp))
            Header.Add(new CreatedTimestampTag(DateTime.UtcNow));
        }

        if ((flags & EmitFlags.AddProgramIdIfNotPresent) == EmitFlags.AddProgramIdIfNotPresent)
        {
          if (Header == null)
            Header = new ADIFHeader();

          if (!Header.Contains(TagNames.ProgramId))
            Header.Add(new ProgramIdTag(Values.DEFAULT_PROGRAM_ID));
        }
      }

      return ToString(formatString, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ADIFDataSet"/>.
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ADIFDataSet"/>.
    /// </summary>
    /// <param name="format">Format string.</param>
    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ADIFDataSet"/>.
    /// </summary>
    /// <param name="format">Format string.</param>
    /// <param name="provider">Culture-specific format provider.</param>
    public string ToString(string format, IFormatProvider provider)
    {
      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format)
      {
        case "G":
        case "C":
          return $"Header Count: {(Header != null ? Header.Count : 0)}, QSO Count: {(QSOs != null ? QSOs.Count : 0)}";

        case "H":
          return HeaderText ?? string.Empty;

        case "A":
        case "a":
          var val = string.Empty;

          if (Header != null)
          {
            val += (string.IsNullOrEmpty(HeaderText) ? Values.DEFAULT_ADIF_HEADER_TEXT : HeaderText) + Environment.NewLine;
            foreach (var tag in Header)
              val += $"{tag.ToString(format, provider)}{Environment.NewLine}";

            if (!Header.Contains(TagNames.EndHeader))
              val += new EndHeaderTag().ToString(format, provider);

            val += Environment.NewLine;
          }

          if (QSOs != null)
          {
            foreach (var qso in QSOs)
            {
              if (qso != null)
              {
                foreach (var tag in qso)
                {
                  val += $"{tag.ToString(format, provider)}";
                }

                if (!qso.Contains(TagNames.EndRecord))
                  val += new EndRecordTag().ToString(format, provider);

                val += Environment.NewLine;
              }
            }
          }

          return val;

        default:
          throw new FormatException($"Format string '{format}' is not valid.");
      }
    }

    string headerText;
  }
}
