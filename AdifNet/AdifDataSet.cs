using System.Globalization;
using System.Xml;
using org.goodspace.Data.Radio.Adif.Tags;
using org.goodspace.Data.Radio.Adif.Helpers;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Represents an ADIF data set, including header and QSOs.
    /// </summary>
    public class AdifDataSet : IFormattable
    {
        /// <summary>
        /// Header text emitted on the first line of an ADIF file or used as an XML 
        /// comment in an ADX file.
        /// </summary>
        public string HeaderText
        {
            get { return headerText ?? string.Empty; }

            set
            {
                headerText = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        /// <summary>
        /// Header tags belonging to the current data set.
        /// </summary>
        public AdifHeader Header { get; set; }

        /// <summary>
        /// QSOs belonging to the current data set.
        /// </summary>
        public AdifQsoCollection Qsos { get; set; }

        /// <summary>
        /// Total number of QSOs in the data set.
        /// </summary>
        public int QsoCount => Qsos?.Count ?? 0;

        /// <summary>
        /// ADIF version to target when generating the data set.
        /// </summary>
        public Version? ADIFVersion
        {
            get
            {
                if (adifVer == null)
                    return Header?.GetTagValue<Version>(AdifTags.AdifVer);
                else
                    return adifVer;
            }

            set
            {
                adifVer = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifDataSet"/> class.
        /// </summary>
        public AdifDataSet()
        {
            Header = [];
            Qsos = [];
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifDataSet"/> class.
        /// </summary>
        /// <param name="values">Tags and associated values to add to the data set.</param>
        public AdifDataSet(IEnumerable<IDictionary<string, string>> values) : this()
        {
            if (values != null)
            {
                foreach (var value in values)
                {
                    var qso = new AdifQso();
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
                        Qsos.Add(qso);
                }
            }
        }

        /// <summary>
        /// Converts the current <see cref="AdifDataSet"/> to ADX.
        /// </summary>
        /// <param name="flags">Flags that determine how the ADX XML is generated.</param>
        public string ToAdx(EmitFlags flags = EmitFlags.None)
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

            if (Qsos != null)
            {
                foreach (var qso in Qsos)
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
        /// Converts the current <see cref="AdifDataSet"/> to ADX and saves the resulting 
        /// XML to the specified file.
        /// </summary>
        /// <param name="outputFile">Destination file where the ADX XML will be saved.</param>
        /// <param name="flags">Flags that determine how the ADX XML is generated.</param>
        public void ToAdx(string outputFile, EmitFlags flags = EmitFlags.None)
        {
            if (string.IsNullOrEmpty(outputFile))
                throw new ArgumentException("Output file is required.", nameof(outputFile));

            var adx = ToAdx(flags);

            if (!string.IsNullOrEmpty(adx))
                File.WriteAllText(outputFile, adx);
        }

        /// <summary>
        /// Converts the current <see cref="AdifDataSet"/> to ADIF text and saves the resulting 
        /// data to the specified file.
        /// </summary>
        /// <param name="outputFile">Destination file where the ADIF text will be saved.</param>
        /// <param name="flags">Flags that determine how the ADIF text is generated.</param>
        public void ToAdif(string outputFile, EmitFlags flags = EmitFlags.None)
        {
            if (string.IsNullOrEmpty(outputFile))
                throw new ArgumentException("Output file is required.", nameof(outputFile));

            var adif = ToAdif(flags);

            if (!string.IsNullOrEmpty(adif))
                File.WriteAllText(outputFile, adif);
        }

        /// <summary>
        /// Converts the current <see cref="AdifDataSet"/> to ADIF text.
        /// </summary>
        /// <param name="flags">Flags that determine how the ADIF text is generated.</param>
        public string ToAdif(EmitFlags flags = EmitFlags.None)
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
                if (Qsos != null)
                {
                    for (var q = 0; q < Qsos.Count; q++)
                    {
                        if (Qsos[q] != null)
                        {
                            var qso = Qsos[q];

                            if (qso.Contains(AdifTags.Operator) && !qso.Contains(AdifTags.StationCallSign))
                            {
                                var stationCallSignTag = new StationCallSignTag();
                                var operatorTag = qso.GetTag(AdifTags.Operator);
                                stationCallSignTag.SetValue(operatorTag?.Value);
                                Qsos[q].Insert(Qsos[q].IndexOf(AdifTags.Operator), stationCallSignTag);
                            }
                            else if (!qso.Contains(AdifTags.Operator) && qso.Contains(AdifTags.StationCallSign))
                            {
                                var operatorTag = new OperatorTag();
                                var stationCallSignTag = qso.GetTag(AdifTags.StationCallSign);
                                operatorTag.SetValue(stationCallSignTag?.Value);
                                Qsos[q].Insert(Qsos[q].IndexOf(AdifTags.StationCallSign), operatorTag);
                            }
                        }
                    }
                }

                if ((flags & EmitFlags.AddCreatedTimestamp) == EmitFlags.AddCreatedTimestamp)
                {
                    Header ??= [];

                    if (!Header.Contains(AdifTags.CreatedTimestamp))
                        Header.Add(new CreatedTimestampTag(DateTime.UtcNow));
                }

                if ((flags & EmitFlags.AddProgramHeaderTags) == EmitFlags.AddProgramHeaderTags)
                {
                    Header ??= [];

                    if (!Header.Contains(AdifTags.ProgramId))
                    {
                        Header.Add(new ProgramIdTag(Values.DEFAULT_PROGRAM_ID));
                        Header.AddOrReplace(new ProgramVersionTag(Values.ProgramVersion));
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the total number of ADIF tags in the data set.
        /// </summary>
        /// <param name="excludeHeader">Whether or not to exclude header tags from the count.</param>
        public int GetTagCount(bool excludeHeader = false)
        {
            var count = 0;

            if (Header != null && !excludeHeader)
                count += Header.Count;

            if (Qsos != null)
            {
                foreach (var qso in Qsos)
                    if (qso != null)
                        count += qso.Count;
            }

            return count;
        }

        /// <summary>
        /// Retrieves the total number of ADIF tags in the data set grouped by tag name.
        /// </summary>
        /// <param name="excludeHeader">Whether or not to exclude header tags from the count.</param>
        public IDictionary<string, int> GetTagCounts(bool excludeHeader = false)
        {
            var counts = new SortedDictionary<string, int>();

            if (Header != null && !excludeHeader)
            {
                foreach (var tag in Header)
                    if (counts.TryGetValue(tag.Name, out int value))
                        counts[tag.Name] = ++value;
                    else
                        counts.Add(tag.Name, 1);
            }

            if (Qsos != null)
            {
                foreach (var qso in Qsos)
                {
                    if (qso == null)
                        continue;

                    foreach (var tag in qso)
                        if (counts.TryGetValue(tag.Name, out int value))
                            counts[tag.Name] = ++value;
                        else
                            counts.Add(tag.Name, 1);
                }
            }

            return counts;
        }

        /// <summary>
        /// Executes the specified <paramref name="action"/> against each QSO in the current data set.
        /// </summary>
        /// <param name="action">Action to execute against each QSO.</param>
        /// <param name="endOnException">Whether or not to stop processing QSOs when an exception is thrown.</param>
        public void ForEachQso(Action<AdifQso> action, bool endOnException = false)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action is required.");

            Qsos ??= [];

            var exceptions = new List<Exception>();

            foreach (var qso in Qsos)
            {
                try
                {
                    action(qso);
                }
                catch (Exception ex)
                {
                    if (endOnException)
                        throw;

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
        public void AddQsoTag(ITag tag)
        {
            if (tag is null)
                return;

            if (tag.Header)
                throw new ArgumentException("Tag must not be a header tag.", nameof(tag));

            Qsos ??= [];

            for (var i = 0; i < Qsos.Count; i++)
            {
                if (!Qsos[i].Contains(tag.Name))
                    Qsos[i].Add(tag);
            }
        }

        /// <summary>
        /// Adds or replaces the specified tag in each QSO in the current data set.
        /// </summary>
        /// <param name="tag">Tag to add or replace.</param>
        public void AddOrReplaceQsoTag(ITag tag)
        {
            if (tag is null)
                return;

            if (tag.Header)
                throw new ArgumentException("Tag must not be a header tag.", nameof(tag));

            Qsos ??= [];

            for (var i = 0; i < Qsos.Count; i++)
                Qsos[i].AddOrReplace(tag);
        }

        /// <summary>
        /// Adds the specified QSO to the current data set.
        /// </summary>
        /// <param name="qso">QSO to add.</param>
        public void AddQso(AdifQso qso)
        {
            if (qso == null)
                throw new ArgumentNullException(nameof(qso), "QSO is required.");

            Qsos ??= [];
            Qsos.Add(qso);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qsoTags"></param>
        public void AddQso(params ITag[] qsoTags)
        {
            if (qsoTags == null || qsoTags.Length < 1)
                throw new ArgumentException("At least one QSO tag is required.", nameof(qsoTags));

            var qso = new AdifQso();
            foreach (var tag in qsoTags)
            {
                if (tag.Header)
                    throw new Exception($"Tag {tag.Name} is a header tag and cannot be added to a QSO.");

                qso.Add(tag);
            }

            AddQso(qso);
        }

        /// <summary>
        /// Adds the specified tag to the header for the current data set.
        /// </summary>
        /// <param name="tag">Header tag to add.</param>
        public void AddHeaderTag(ITag tag)
        {
            if (tag is null)
                return;

            if (!tag.Header && !tag.IsAppDef)
                throw new ArgumentException("Tag must be a header tag.", nameof(tag));

            Header ??= [];
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

            if (!tag.Header && !tag.IsAppDef)
                throw new ArgumentException("Tag must be a header tag.", nameof(tag));

            Header ??= [];
            Header.AddOrReplace(tag);
        }

        /// <summary>
        /// Adds a user-defined tag definition to the current data set.
        /// </summary>
        /// <param name="fieldName">Name of the user-defined field.</param>
        /// <param name="dataType">ADIF data type of the user-defined field.</param>
        public UserDefTag AddUserDefinedTagDefinition(string fieldName, string dataType)
        {
            Header ??= [];
            return Header.AddUserDefinedTag(fieldName, dataType);
        }

        /// <summary>
        /// Adds a user-defined tag definition to the current data set.
        /// </summary>
        /// <param name="fieldName">Name of the user-defined field.</param>
        /// <param name="options">Custom enumeration values for the user-defined field.</param>
        public UserDefTag AddUserDefinedTagDefinition(string fieldName, params string[] options)
        {
            Header ??= [];
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
            Header ??= [];
            return Header.AddUserDefinedTag(fieldName, lowerBound, upperBound);
        }

        /// <summary>
        /// Validates the tags in the current data set against the ADIF version specified in the ADIF_VER tag.
        /// </summary>
        public void CheckVersion()
        {
            if (Header == null)
                throw new Exception("Cannot check tag version validity: no header found.");

            if (Header.GetTag(AdifTags.AdifVer) is not AdifVersionTag versionTag)
                throw new Exception($"Cannot check tag version validity: no '{AdifTags.AdifVer}' tag found.");

            if (versionTag.Value == null)
                throw new Exception($"Cannot check tag version validity: no value found for '{AdifTags.AdifVer}' tag.");

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

            foreach (var qso in Qsos)
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
        /// Returns a string representation of the current <see cref="AdifDataSet"/>.
        /// </summary>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="AdifDataSet"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        public string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="AdifDataSet"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="provider">Culture-specific format provider.</param>
        public string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            provider ??= CultureInfo.CurrentCulture;

            switch (format)
            {
                case "G":
                case "C":
                    return $"Header Count: {(Header != null ? Header.Count : 0)}, QSO Count: {(Qsos != null ? Qsos.Count : 0)}";

                case "H":
                    return HeaderText ?? string.Empty;

                case "A":
                case "a":
                    var val = string.Empty;
                    var endRecordTag = new EndRecordTag();

                    if (Header != null && Header.Count > 0)
                    {
                        val += (string.IsNullOrEmpty(HeaderText) ? Values.DEFAULT_ADIF_HEADER_TEXT : HeaderText) + Environment.NewLine;
                        foreach (var tag in Header)
                        {
                            if (tag is not EndHeaderTag)
                                val += $"{tag.ToString(format, provider)}{Environment.NewLine}";
                        }

                        val += new EndHeaderTag().ToString(format, provider);
                        val += Environment.NewLine;
                    }

                    if (Qsos != null)
                    {
                        foreach (var qso in Qsos)
                        {
                            if (qso != null)
                            {
                                foreach (var tag in qso)
                                {
                                    if (tag is not EndRecordTag)
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
                    return ToAdx();

                default:
                    throw new FormatException($"Format string '{format}' is not valid.");
            }
        }

        string? headerText;
        Version? adifVer;
    }
}
