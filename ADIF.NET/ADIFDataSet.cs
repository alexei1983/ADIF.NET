using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using ADIF.NET.Tags;
using ADIF.NET.Helpers;

namespace ADIF.NET {

  /// <summary>
  /// Represents an ADIF data set, including header and QSOs.
  /// </summary>
  public class ADIFDataSet : IFormattable {

    /// <summary>
    /// Header text emitted on the first line of an ADIF file or used as an XML 
    /// comment in an ADX file.
    /// </summary>
    public string HeaderText
    {
      get { return headerText; }

      set
      {
        headerText = string.IsNullOrWhiteSpace(value) ? null : value;
      }
    }

    /// <summary>
    /// Header tags belonging to the current data set.
    /// </summary>
    public ADIFHeader Header { get; set; }

    /// <summary>
    /// QSOs belonging to the current data set.
    /// </summary>
    public ADIFQSOCollection QSOs { get; set; }

    /// <summary>
    /// ADIF version to target when generating the data set.
    /// </summary>
    public Version ADIFVersion
    {
      get
      {
        if (adifVer == null)
          return Header?.GetTagValue<Version>(TagNames.ADIFVer);
        else
          return adifVer;
      }

      set
      {
        adifVer = value;
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFDataSet"/> class.
    /// </summary>
    public ADIFDataSet() {
      Header = new ADIFHeader();
      QSOs = new ADIFQSOCollection();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFDataSet"/> class.
    /// </summary>
    /// <param name="values">Tags and associated values to add to the data set.</param>
    public ADIFDataSet(IEnumerable<IDictionary<string, string>> values) : this()
    {
      if (values != null)
      {
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
    /// Converts the current <see cref="ADIFDataSet"/> to ADX.
    /// </summary>
    /// <param name="flags">Flags that determine how the ADX XML is generated.</param>
    public string ToADX(EmitFlags flags = EmitFlags.None)
    {
      HandleFlags(flags);

      var doc = new XmlDocument();
      var rootEl = doc.CreateElement(ADXValues.ADX_ROOT_ELEMENT);

      if (Header != null)
      {
        var headerEl = doc.CreateElement(ADXValues.ADX_HEADER_ELEMENT);

        var headerText = ToString("H", CultureInfo.CurrentCulture);

        if (!string.IsNullOrEmpty(headerText))
          headerEl.AppendChild(doc.CreateComment(headerText));

        foreach (var tag in Header)
        {
          var xmlEl = tag.ToXml(doc);
          if (xmlEl != null)
            headerEl.AppendChild(xmlEl);
        }

        rootEl.AppendChild(headerEl);
      }

      var recordEl = doc.CreateElement(ADXValues.ADX_RECORDS_ELEMENT);

      if (QSOs != null)
      {
        foreach (var qso in QSOs)
        {
          var qsoRecordEl = doc.CreateElement(ADXValues.ADX_RECORD_ELEMENT);

          foreach (var tag in qso)
          {
            var xmlEl = tag.ToXml(doc);
            if (xmlEl != null)
              qsoRecordEl.AppendChild(xmlEl);
          }

          recordEl.AppendChild(qsoRecordEl);
        }
      }

      rootEl.AppendChild(recordEl);

      doc.AppendChild(rootEl);

      return doc.OuterXml;
    }

    /// <summary>
    /// Converts the current <see cref="ADIFDataSet"/> to ADX and saves the resulting 
    /// XML to the specified file.
    /// </summary>
    /// <param name="outputFile">Destination file where the ADX XML will be saved.</param>
    /// <param name="flags">Flags that determine how the ADX XML is generated.</param>
    public void ToADX(string outputFile, EmitFlags flags = EmitFlags.None)
    {
      if (string.IsNullOrEmpty(outputFile))
        throw new ArgumentException("Output file is required.", nameof(outputFile));

      var adx = ToADX(flags);

      if (!string.IsNullOrEmpty(adx))
        File.WriteAllText(outputFile, adx);
    }

    /// <summary>
    /// Converts the current <see cref="ADIFDataSet"/> to ADIF text and saves the resulting 
    /// data to the specified file.
    /// </summary>
    /// <param name="outputFile">Destination file where the ADIF text will be saved.</param>
    /// <param name="flags">Flags that determine how the ADIF text is generated.</param>
    public void ToADIF(string outputFile, EmitFlags flags = EmitFlags.None)
    {
      if (string.IsNullOrEmpty(outputFile))
        throw new ArgumentException("Output file is required.", nameof(outputFile));

      var adif = ToADIF(flags);

      if (!string.IsNullOrEmpty(adif))
        File.WriteAllText(outputFile, adif);
    }

    /// <summary>
    /// Converts the current <see cref="ADIFDataSet"/> to ADIF text.
    /// </summary>
    /// <param name="flags">Flags that determine how the ADIF text is generated.</param>
    public string ToADIF(EmitFlags flags = EmitFlags.None)
    {
      var formatString = (flags & EmitFlags.LowercaseTagNames) == EmitFlags.LowercaseTagNames ? "a" : "A";
      HandleFlags(flags);

      return ToString(formatString, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flags"></param>
    void HandleFlags(EmitFlags flags)
    {
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
                var stationCallSignTag = new StationCallSignTag();
                var operatorTag = qso.GetTag(TagNames.Operator);
                stationCallSignTag.SetValue(operatorTag.Value);
                QSOs[q].Insert(QSOs[q].IndexOf(TagNames.Operator), stationCallSignTag);
              }
              else if (!qso.Contains(TagNames.Operator) && qso.Contains(TagNames.StationCallSign))
              {
                var operatorTag = new OperatorTag();
                var stationCallSignTag = qso.GetTag(TagNames.StationCallSign);
                operatorTag.SetValue(stationCallSignTag.Value);
                QSOs[q].Insert(QSOs[q].IndexOf(TagNames.StationCallSign), operatorTag);
              }
            }
          }
        }

        if ((flags & EmitFlags.AddCreatedTimestamp) == EmitFlags.AddCreatedTimestamp)
        {
          if (Header == null)
            Header = new ADIFHeader();

          if (!Header.Contains(TagNames.CreatedTimestamp))
            Header.Add(new CreatedTimestampTag(DateTime.UtcNow));
        }

        if ((flags & EmitFlags.AddProgramHeaderTags) == EmitFlags.AddProgramHeaderTags)
        {
          if (Header == null)
            Header = new ADIFHeader();

          if (!Header.Contains(TagNames.ProgramId))
          {
            Header.Add(new ProgramIdTag(Values.DEFAULT_PROGRAM_ID));
            Header.AddOrReplace(new ProgramVersionTag(Values.ProgramVersion));
          }
        }
      }
    }

    /// <summary>
    /// Executes the specified <paramref name="action"/> against each QSO in the current data set.
    /// </summary>
    /// <param name="action">Action to execute against each QSO.</param>
    /// <param name="endOnException">Whether or not to stop processing QSOs when an exception is thrown.</param>
    public void ForEachQSO(Action<ADIFQSO> action, bool endOnException = false)
    {
      if (action == null)
        throw new ArgumentNullException(nameof(action), "Action is required.");

      if (QSOs == null)
        QSOs = new ADIFQSOCollection();

      var exceptions = new List<Exception>();

      foreach (var qso in QSOs)
      {
        try
        {
          action(qso);
        }
        catch (Exception ex)
        {
          if (endOnException)
            throw ex;

          exceptions.Add(ex);
        }
      }

      if (exceptions.Count > 0)
        throw new AggregateException(exceptions.ToArray());
    }

    /// <summary>
    /// Adds the specified tag to each QSO in the current data set.
    /// </summary>
    /// <param name="tag">QSO tag to add.</param>
    public void AddQSOTag(ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Tag must not be a header tag.", nameof(tag));

      if (QSOs == null)
        QSOs = new ADIFQSOCollection();

      for (var i = 0; i < QSOs.Count; i++)
      {
        if (!QSOs[i].Contains(tag.Name))
          QSOs[i].Add(tag);
      }
    }

    /// <summary>
    /// Adds or replaces the specified tag in each QSO in the current data set.
    /// </summary>
    /// <param name="tag">Tag to add or replace.</param>
    public void AddOrReplaceQSOTag(ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Tag must not be a header tag.", nameof(tag));

      if (QSOs == null)
        QSOs = new ADIFQSOCollection();

      for (var i = 0; i < QSOs.Count; i++)
          QSOs[i].AddOrReplace(tag);
    }

    /// <summary>
    /// Adds the specified QSO to the current data set.
    /// </summary>
    /// <param name="qso">QSO to add.</param>
    public void AddQSO(ADIFQSO qso)
    {
      if (qso == null)
        throw new ArgumentNullException(nameof(qso), "QSO is required.");

      if (QSOs == null)
        QSOs = new ADIFQSOCollection();

      QSOs.Add(qso);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoTags"></param>
    public void AddQSO(params ITag[] qsoTags)
    {
      if (qsoTags == null || qsoTags.Length < 1)
        throw new ArgumentException("At least one QSO tag is required.", nameof(qsoTags));

      var qso = new ADIFQSO();
      foreach (var tag in qsoTags)
      {
        if (tag.Header)
          throw new Exception($"Tag {tag.Name} is a header tag and cannot be added to a QSO.");

        qso.Add(tag);
      }

      AddQSO(qso);
    }

    /// <summary>
    /// Adds the specified tag to the header for the current data set.
    /// </summary>
    /// <param name="tag">Header tag to add.</param>
    public void AddHeaderTag(ITag tag)
    {
      if (tag is null)
        return;

      if (!tag.Header)
        throw new ArgumentException("Tag must be a header tag.", nameof(tag));

      if (Header == null)
        Header = new ADIFHeader();

      Header.Add(tag);
    }

    /// <summary>
    /// Adds or replaces the specified tag in the header for the current data set.
    /// </summary>
    /// <param name="tag">Header tag to add or replace.</param>
    public void AddOrReplaceHeaderTag(ITag tag)
    {
      if (tag is null)
        return;

      if (!tag.Header)
        throw new ArgumentException("Tag must be a header tag.", nameof(tag));

      if (Header == null)
        Header = new ADIFHeader();

      Header.AddOrReplace(tag);
    }

    /// <summary>
    /// Adds a user-defined tag definition to the current data set.
    /// </summary>
    /// <param name="fieldName">Name of the user-defined field.</param>
    /// <param name="dataType">ADIF data type of the user-defined field.</param>
    public UserDefTag AddUserDefinedTagDefinition(string fieldName, string dataType)
    {
      if (Header == null)
        Header = new ADIFHeader();

      return Header.AddUserDefinedTag(fieldName, dataType);
    }

    /// <summary>
    /// Adds a user-defined tag definition to the current data set.
    /// </summary>
    /// <param name="fieldName">Name of the user-defined field.</param>
    /// <param name="options">Custom enumeration values for the user-defined field.</param>
    public UserDefTag AddUserDefinedTagDefinition(string fieldName, params string[] options)
    {
      if (Header == null)
        Header = new ADIFHeader();

      return Header.AddUserDefinedTag(fieldName, options);
    }

    /// <summary>
    /// Adds a user-defined tag definition to the current data set.
    /// </summary>
    /// <param name="fieldName">Name of the user-defined field.</param>
    /// <param name="lowerBound">Minimum valid numeric value.</param>
    /// <param name="upperBound">Maximum valid numeric value.</param>
    public UserDefTag AddUserDefinedTagDefinition(string fieldName, double lowerBound, double upperBound)
    {
      if (Header == null)
        Header = new ADIFHeader();

      return Header.AddUserDefinedTag(fieldName, lowerBound, upperBound);
    }

    /// <summary>
    /// Validates the tags in the current data set against the ADIF version specified in the ADIF_VER tag.
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
    /// Validates the tags in the current data set against the specified ADIF version.
    /// </summary>
    /// <param name="version">ADIF version to validate against.</param>
    public void CheckVersion(Version version)
    {
      if (version == null)
        throw new ArgumentNullException(nameof(version), $"Cannot check tag version validity: no ADIF version was specified.");

      var exceptions = new List<Exception>();
      var checkedTags = new List<string>();

      foreach (var qso in QSOs)
      {
        if (qso == null)
          continue;

        foreach (var tag in qso)
        {
          if (tag == null)
            continue;

          var alreadyChecked = checkedTags.Contains(tag.Name.ToUpper());

          try
          {
            if (!alreadyChecked)
              TagValidationHelper.ValidateTagVersion(tag, version);
          }
          catch (Exception ex)
          {
            exceptions.Add(ex);
          }
          finally
          {
            if (!alreadyChecked)
              checkedTags.Add(tag.Name.ToUpper());
          }
        }
      }

      foreach (var tag in Header)
      {
        if (tag == null)
          continue;

        var alreadyChecked = checkedTags.Contains(tag.Name.ToUpper());

        try
        {
          if (!alreadyChecked)
            TagValidationHelper.ValidateTagVersion(tag, version);
        }
        catch (Exception ex)
        {
          exceptions.Add(ex);
        }
        finally
        {
          if (!alreadyChecked)
            checkedTags.Add(tag.Name.ToUpper());
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
          var endRecordTag = new EndRecordTag();

          if (Header != null)
          {
            val += (string.IsNullOrEmpty(HeaderText) ? Values.DEFAULT_ADIF_HEADER_TEXT : HeaderText) + Environment.NewLine;
            foreach (var tag in Header)
            {
              if (!(tag is EndHeaderTag))
                val += $"{tag.ToString(format, provider)}{Environment.NewLine}";
            }

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
                  if (!(tag is EndRecordTag))
                    val += $"{tag.ToString(format, provider)}";
                }

                val += endRecordTag.ToString(format, provider);
                val += Environment.NewLine;
              }
            }
          }

          return val;

        case "X":
        case "x":
          return ToADX();

        default:
          throw new FormatException($"Format string '{format}' is not valid.");
      }
    }

    string headerText;
    Version adifVer;
  }
}
