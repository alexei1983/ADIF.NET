using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ADIF.NET.Tags;
using ADIF.NET.Helpers;

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
    public ADIFDataSet() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    public ADIFDataSet(List<Dictionary<string, string>> values)
    {
      if (values != null)
      {
        Header = new ADIFHeader();
        QSOs = new ADIFQSOCollection();

        foreach (var value in values)
        {
          var qso = new ADIFQSO();
          foreach (var key in value.Keys)
          {
            var tag = TagFactory.TagFromName(key);

            if (tag != null)
            {
              if (value[key] != null)
                tag.SetValue(value[key]);

              if (tag.Header)
                Header.Add(tag);
              else
                qso.Add(tag);
            }
          }

          if (qso.Count > 0)
            QSOs.Add(qso);
        }
      }
    }

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
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public void AddQSOTag(ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new Exception("Cannot add header tag to all QSOs.");

      for (var i = 0; i < QSOs.Count; i++)
      {
        if (!QSOs[i].Contains(tag.Name))
          QSOs[i].Add(tag);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public void AddOrReplaceQSOTag(ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new Exception("Cannot add header tag to all QSOs.");

      for (var i = 0; i < QSOs.Count; i++)
          QSOs[i].AddOrReplace(tag);
    }

    /// <summary>
    /// 
    /// </summary>
    public void CheckVersion()
    {
      if (Header == null)
        throw new Exception("Cannot check tag version validity: no header found.");

      if (!(Header.GetTag(TagNames.ADIFVer) is ADIFVersionTag versionTag))
        throw new Exception($"Cannot check tag version validity: no '{TagNames.ADIFVer}' tag found.");

      if (versionTag.Value == null)
        throw new Exception($"Cannot check tag version validity: no value found for '{TagNames.ADIFVer}' tag.");

      CheckVersion(versionTag.Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="version"></param>
    public void CheckVersion(Version version)
    {
      if (version == null)
        throw new Exception($"Cannot check tag version validity: no ADIF version specified.");

      var exceptions = new List<Exception>();

      foreach (var qso in QSOs)
      {
        if (qso == null)
          continue;

        foreach (var tag in qso)
        {
          if (tag == null)
            continue;

          try
          {
            TagValidationHelper.ValidateTagVersion(tag, version);
          }
          catch (Exception ex)
          {
            exceptions.Add(ex);
          }
        }
      }

      foreach (var tag in Header)
      {
        if (tag == null)
          continue;

        try
        {
          TagValidationHelper.ValidateTagVersion(tag, version);
        }
        catch (Exception ex)
        {
          exceptions.Add(ex);
        }
      }

      if (exceptions.Count > 1)
        throw new AggregateException(exceptions.ToArray());
      else if (exceptions.Count == 1)
        throw exceptions[0];
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
