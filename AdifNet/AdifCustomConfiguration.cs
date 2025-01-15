using System.Xml.Linq;
using org.goodspace.Data.Radio.Adif.Exceptions;
using org.goodspace.Data.Radio.Adif.Tags;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="tagName"></param>
    /// <param name="value"></param>
    public class AdifTagNameWithValue(string tagName, object? value)
    {
        /// <summary>
        /// 
        /// </summary>
        public string TagName { get; } = tagName;

        /// <summary>
        /// 
        /// </summary>
        public object? Value { get; } = value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        public AdifTagNameWithValue(string tagName) : this(tagName, null) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AdifCustomConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public EmitFlags EmitFlags { get; set; }

        /// <summary>
        /// Target ADIF version.
        /// </summary>
        public Version? AdifTargetVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AdifTagNameWithValue[] DefaultValues { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public AdifTagNameWithValue[] AddTags { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public AdifTagNameWithValue[] ReplaceTags { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public AdifTagNameWithValue[] RemoveTags { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public SerialNumberGenerator[] SerialNumberGenerators { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public UserDefTag[] UserDefTags { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public AdifCustomConfiguration()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public object? GetDefaultValue(string tagName)
        {
            var obj = DefaultValues.FirstOrDefault(d => d.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
            if (obj != null)
                return obj.Value;
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public UserDefTag? GetUserDefTag(string tagName)
        {
            return UserDefTags.FirstOrDefault(d => d.FieldName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool ShouldRemoveTag(ITag tag)
        {
            var removeTag = RemoveTags.FirstOrDefault(r => r.TagName.Equals(tag.Name, StringComparison.OrdinalIgnoreCase));

            if (removeTag != null)
            {
                if (removeTag.Value != null)
                {
                    if (removeTag.Value.Equals(tag.Value))
                        return true;
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool ShouldReplaceTag(ITag tag)
        {
            return ReplaceTags.Any(r => r.TagName.Equals(tag.Name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public bool ShouldReplaceTag(string tagName)
        {
            return ReplaceTags.Any(r => r.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="qso"></param>
        /// <returns></returns>
        public bool ShouldAddTag(string tagName, AdifQso qso)
        {
            var addTag = AddTags.FirstOrDefault(r => r.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
            return addTag != null && !qso.Contains(addTag.TagName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public SerialNumberGenerator? GetSerialNumberGenerator(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Serial number generator name is required.", nameof(name));

            return SerialNumberGenerators.FirstOrDefault(s => name.Equals(s.Name));
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //static IEnumerable<string> GetSearchPaths()
        //{
        //    string[] paths = [
        //              Environment.GetFolderPath(Environment.SpecialFolder.System),
        //              Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        //              Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
        //              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        //              Environment.CurrentDirectory
        //  ];

        //    foreach (var path in paths)
        //    {
        //        if (!string.IsNullOrEmpty(path))
        //        {
        //            var _path = Path.GetFullPath(path);
        //            if (!string.IsNullOrEmpty(_path))
        //                yield return _path;
        //        }
        //    }
        //}

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="fileName"></param>
    //    static string? FindConfigurationFile(string fileName)
    //    {
    //        var paths = GetSearchPaths();

    //        foreach (var p in paths)
    //        {
    //            var path = Path.Combine(p, fileName);
    //            if (new FileInfo(path).Exists)
    //                return path;
    //        }

    //        foreach (var s in Environment.GetLogicalDrives())
    //        {
    //            var path = Path.Combine(s, fileName);
    //            if (new FileInfo(path).Exists)
    //                return path;
    //        }

    //        return null;
    //    }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AdifCustomConfigurationManager
    {
        XDocument? xDoc;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void LoadFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Configuration file path is required.", nameof(path));

            if (!Path.IsPathRooted(path))
                path = Path.GetFullPath(path);

            var fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Configuration file does not exist: {path}", path);

            xDoc = XDocument.Load(fileInfo.OpenRead());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void LoadString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Configuration string cannot be null or empty.", nameof(value));
            xDoc = XDocument.Load(new StringReader(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void LoadStream(Stream stream)
        {
            if (stream == null || !stream.CanRead)
                throw new ArgumentException("Cannot read configuration: invalid stream.", nameof(stream));

            xDoc = XDocument.Load(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="outputFile"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public XDocument Create(AdifCustomConfiguration configuration, string outputFile)
        {
            if (string.IsNullOrEmpty(outputFile))
                throw new ArgumentException("Output configuration file is required.", nameof(outputFile));

            if (!Path.IsPathRooted(outputFile))
                outputFile = Path.GetFullPath(outputFile);

            var xDoc = Create(configuration);
            xDoc.Save(outputFile);
            return xDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public XDocument Create(AdifCustomConfiguration configuration)
        {
            var xDoc = new XDocument();
            var rootElement = new XElement(ELEMENT_ROOT);

            if (configuration.AdifTargetVersion != null)
                rootElement.Add(new XAttribute(ATTRIBUTE_ADIF_VERSION, configuration.AdifTargetVersion.ToString(3)));

            if (configuration.UserDefTags.Length > 0)
            {
                var userDefTagsElement = new XElement(ELEMENT_USER_DEF_TAGS);

                foreach (var userDefTag in configuration.UserDefTags)
                {
                    var userDefTagElement = new XElement(ELEMENT_USER_DEF_TAG);
                    userDefTagElement.Add(new XAttribute(ATTRIBUTE_ID, userDefTag.FieldId));

                    if (!string.IsNullOrEmpty(userDefTag.DataType))
                        userDefTagElement.Add(new XAttribute(ATTRIBUTE_DATA_TYPE, userDefTag.DataType));

                    if (userDefTag.LowerBound != userDefTag.UpperBound)
                        userDefTagElement.Add(new XAttribute(ATTRIBUTE_RANGE, $"{userDefTag.LowerBound}:{userDefTag.UpperBound}"));

                    if (userDefTag.CustomOptions.Length > 0)
                        userDefTagElement.Add(new XAttribute(ATTRIBUTE_ENUM, $"{{{string.Join(',', userDefTag.CustomOptions)}}}"));

                    userDefTagElement.Value = userDefTag.FieldName;
                    userDefTagsElement.Add(userDefTagElement);
                }

                rootElement.Add(userDefTagsElement);
            }

            if (configuration.DefaultValues.Length > 0)
            {
                var defaultsElement = new XElement(ELEMENT_DEFAULTS);

                foreach (var defaultValue in configuration.DefaultValues)
                {
                    var defaultElement = new XElement(ELEMENT_TAG);
                    defaultElement.Add(new XAttribute(ATTRIBUTE_NAME, defaultValue.TagName));

                    if (defaultValue.Value != null)
                    {
                        // format the value correctly
                        defaultElement.Value = defaultValue.Value.ToString() ?? string.Empty;
                    }

                    defaultsElement.Add(defaultElement);
                }

                rootElement.Add(defaultsElement);
            }

            if (configuration.AddTags.Length > 0)
            {
                var addTagsElement = new XElement(ELEMENT_ADD_TAGS);

                foreach (var addTag in configuration.AddTags)
                {
                    var addTagElement = new XElement(ELEMENT_TAG);
                    addTagElement.Add(new XAttribute(ATTRIBUTE_NAME, addTag.TagName));

                    if (addTag.Value != null)
                        addTagElement.Value = addTag.Value.ToString() ?? string.Empty;

                    addTagsElement.Add(addTagElement);
                }
                rootElement.Add(addTagsElement);
            }

            if (configuration.ReplaceTags.Length > 0)
            {
                var replaceTagsElement = new XElement(ELEMENT_REPLACE_TAGS);

                foreach (var replaceTag in configuration.ReplaceTags)
                {
                    var replaceTagElement = new XElement(ELEMENT_TAG);
                    replaceTagElement.Add(new XAttribute(ATTRIBUTE_NAME, replaceTag.TagName));

                    if (replaceTag.Value != null)
                        replaceTagElement.Value = replaceTag.Value.ToString() ?? string.Empty;

                    replaceTagsElement.Add(replaceTagElement);
                }
                rootElement.Add(replaceTagsElement);
            }

            if (configuration.RemoveTags.Length > 0)
            {
                var removeTagsElement = new XElement(ELEMENT_REMOVE_TAGS);

                foreach (var removeTag in configuration.RemoveTags)
                {
                    var removeTagElement = new XElement(ELEMENT_TAG);
                    removeTagElement.Add(new XAttribute(ATTRIBUTE_NAME, removeTag.TagName));

                    if (removeTag.Value != null)
                        removeTagElement.Value = removeTag.Value.ToString() ?? string.Empty;

                    removeTagsElement.Add(removeTagElement);
                }
                rootElement.Add(removeTagsElement);
            }

            if (configuration.SerialNumberGenerators.Length > 0)
            {
                var serialNumsElement = new XElement(ELEMENT_SERIAL_NUMBER_GENERATORS);

                foreach (var serialNumGen in configuration.SerialNumberGenerators)
                {
                    var serialNumElement = new XElement(ELEMENT_SERIAL_NUMBER_GENERATOR);
                    serialNumElement.Add(new XAttribute(ATTRIBUTE_START, serialNumGen.Start));

                    if (serialNumGen.Current > 0)
                        serialNumElement.Add(new XAttribute(ATTRIBUTE_CURRENT, serialNumGen.Current));

                    if (serialNumGen.StringLength > 0)
                        serialNumElement.Add(new XAttribute(ATTRIBUTE_STRING_LENGTH, serialNumGen.StringLength));

                    if (!string.IsNullOrEmpty(serialNumGen.Name))
                        serialNumElement.Value = serialNumGen.Name;

                    serialNumsElement.Add(serialNumElement);
                }

                rootElement.Add(serialNumsElement);
            }

            if (configuration.EmitFlags != EmitFlags.None)
            {
                var optionsElement = new XElement(ELEMENT_OPTIONS);

                if ((configuration.EmitFlags & EmitFlags.LowercaseTagNames) == EmitFlags.LowercaseTagNames)
                    optionsElement.Add(new XElement(ELEMENT_LOWERCASE_TAG_NAMES) { Value = Values.ADIF_BOOLEAN_TRUE });

                if ((configuration.EmitFlags & EmitFlags.AddProgramHeaderTags) == EmitFlags.AddProgramHeaderTags)
                    optionsElement.Add(new XElement(ELEMENT_ADD_PROGRAM_HEADER_TAGS) { Value = Values.ADIF_BOOLEAN_TRUE });

                if ((configuration.EmitFlags & EmitFlags.AddCreatedTimestamp) == EmitFlags.AddCreatedTimestamp)
                    optionsElement.Add(new XElement(ELEMENT_ADD_CREATED_TIMESTAMP_TAG) { Value = Values.ADIF_BOOLEAN_TRUE });

                if ((configuration.EmitFlags & EmitFlags.MirrorOperatorAndStationCallSign) == EmitFlags.MirrorOperatorAndStationCallSign)
                    optionsElement.Add(new XElement(ELEMENT_MIRROR_OPERATOR_STATION_CALLSIGN) { Value = Values.ADIF_BOOLEAN_TRUE });

                rootElement.Add(optionsElement);
            }

            xDoc.AddFirst(rootElement);
            return xDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public AdifCustomConfiguration Parse()
        {
            if (xDoc == null || xDoc.Root == null || !xDoc.Descendants().Any())
                throw new Exception("No configuration data found.");

            if (!xDoc.Root.Name.LocalName.Equals(ELEMENT_ROOT))
                throw new Exception("Not an AdifNet configuration file.");

            var config = new AdifCustomConfiguration();

            var versionStr = xDoc.Root.Attribute(ATTRIBUTE_ADIF_VERSION)?.Value;

            if (!string.IsNullOrEmpty(versionStr))
            {
                if (!Version.TryParse(versionStr, out var version))
                    throw new Exception($"Invalid ADIF version: {versionStr}");
                config.AdifTargetVersion = version;
            }

            List<UserDefTag> userDefTags = [];

            // get user-defined tags
            foreach (var element in xDoc.Descendants(ELEMENT_USER_DEF_TAGS).Elements())
            {
                if (!element.Name.LocalName.Equals(ELEMENT_USER_DEF_TAG))
                    continue;

                var fieldId = element.Attribute(ATTRIBUTE_ID)?.Value;
                var dataType = element.Attribute(ATTRIBUTE_DATA_TYPE)?.Value;
                var enumStr = element.Attribute(ATTRIBUTE_ENUM)?.Value;
                var range = element.Attribute(ATTRIBUTE_RANGE)?.Value;
                var fieldName = element.Value;

                if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldId))
                {
                    if (!int.TryParse(fieldId, out var _fieldId))
                        throw new UserDefTagException($"Invalid user-defined field ID: {fieldId}");

                    userDefTags.Add(new UserDefTag(fieldName, _fieldId, dataType ?? string.Empty)
                    {
                        CustomOptions = [],
                        LowerBound = 0,
                        UpperBound = 0,
                    });
                }
            }

            config.UserDefTags = [.. userDefTags];

            List<AdifTagNameWithValue> defaults = [];

            // get defaults
            foreach (var element in xDoc.Descendants(ELEMENT_DEFAULTS).Elements())
            {
                if (!element.Name.LocalName.Equals(ELEMENT_TAG))
                    continue;

                var tagName = element.Attribute(ATTRIBUTE_NAME)?.Value;
                var defaultValue = element.Value;

                if (!string.IsNullOrEmpty(tagName) && !string.IsNullOrEmpty(defaultValue))
                {
                    tagName = tagName.ToUpper();

                    var tag = TagFactory.TagFromName(tagName) ??
                              userDefTags.FirstOrDefault(u => u.FieldName.Equals(tagName, StringComparison.OrdinalIgnoreCase)) ??
                              throw new Exception($"Invalid ADIF tag: {tagName}");

                    tag.SetValue(defaultValue);

                    if (tag.Value != null)
                        defaults.Add(new AdifTagNameWithValue(tag.Name, tag.Value));
                }
            }

            config.DefaultValues = [.. defaults];

            List<AdifTagNameWithValue> adds = [];

            // get tag adds
            foreach (var element in xDoc.Descendants(ELEMENT_ADD_TAGS).Elements())
            {
                if (!element.Name.LocalName.Equals(ELEMENT_TAG))
                    continue;

                var tagName = element.Attribute(ATTRIBUTE_NAME)?.Value;
                var tagValue = element.Value;

                if (!string.IsNullOrEmpty(tagName) && !string.IsNullOrEmpty(tagValue))
                {
                    tagName = tagName.ToUpper();

                    var tag = TagFactory.TagFromName(tagName) ??
                              userDefTags.FirstOrDefault(u => u.FieldName.Equals(tagName, StringComparison.OrdinalIgnoreCase)) ??
                              throw new Exception($"Invalid ADIF tag: {tagName}");

                    if (tag.IsUserDef && tag is UserDefTag userDefTag)
                        tag = new UserDefValueTag(userDefTag, tagValue);
                    else
                        tag.SetValue(tagValue);

                    adds.Add(new AdifTagNameWithValue(tag.Name, tag.Value));
                }
            }

            config.AddTags = [.. adds];

            List<AdifTagNameWithValue> replace = [];

            // get tag replacements
            foreach (var element in xDoc.Descendants(ELEMENT_REPLACE_TAGS).Elements())
            {
                if (!element.Name.LocalName.Equals(ELEMENT_TAG))
                    continue;

                var tagName = element.Attribute(ATTRIBUTE_NAME)?.Value;
                var tagValue = element.Value;

                if (!string.IsNullOrEmpty(tagName) && !string.IsNullOrEmpty(tagValue))
                {
                    tagName = tagName.ToUpper();

                    var tag = TagFactory.TagFromName(tagName) ??
                              userDefTags.FirstOrDefault(u => u.FieldName.Equals(tagName, StringComparison.OrdinalIgnoreCase)) ??
                              throw new Exception($"Invalid ADIF tag: {tagName}");

                    if (tag.IsUserDef && tag is UserDefTag userDefTag)
                        tag = new UserDefValueTag(userDefTag, tagValue);
                    else
                        tag.SetValue(tagValue);

                    replace.Add(new AdifTagNameWithValue(tag.Name, tag.Value));
                }
            }

            config.ReplaceTags = [.. replace];

            List<AdifTagNameWithValue> remove = [];

            // get tag replacements
            foreach (var element in xDoc.Descendants(ELEMENT_REMOVE_TAGS).Elements())
            {
                if (!element.Name.LocalName.Equals(ELEMENT_TAG))
                    continue;

                var tagName = element.Attribute(ATTRIBUTE_NAME)?.Value;
                var tagValue = element.Value;

                if (!string.IsNullOrEmpty(tagName))
                {
                    tagName = tagName.ToUpper();

                    var tag = TagFactory.TagFromName(tagName) ??
                              userDefTags.FirstOrDefault(u => u.FieldName.Equals(tagName, StringComparison.OrdinalIgnoreCase)) ??
                              throw new Exception($"Invalid ADIF tag: {tagName}");

                    if (tag.IsUserDef && tag is UserDefTag userDefTag)
                        tag = new UserDefValueTag(userDefTag, tagValue);
                    else if (!string.IsNullOrEmpty(tagValue))
                        tag.SetValue(tagValue);

                    remove.Add(new AdifTagNameWithValue(tag.Name, tag.Value));
                }
            }

            config.RemoveTags = [.. remove];

            List<SerialNumberGenerator> serialNumGens = [];

            // get serial number generators
            foreach (var element in xDoc.Descendants(ELEMENT_SERIAL_NUMBER_GENERATORS).Elements())
            {
                if (!element.Name.LocalName.Equals(ELEMENT_SERIAL_NUMBER_GENERATOR))
                    continue;

                var startVal = element.Attribute(ATTRIBUTE_START)?.Value;
                var curVal = element.Attribute(ATTRIBUTE_CURRENT)?.Value;
                var strLenVal = element.Attribute(ATTRIBUTE_STRING_LENGTH)?.Value;
                var name = element.Value;

                if (!string.IsNullOrEmpty(startVal))
                {
                    if (!int.TryParse(startVal, out var startNum))
                        throw new Exception($"Invalid serial number starting value: {startVal}");

                    int curNum = 1;

                    if (!string.IsNullOrEmpty(curVal) && !int.TryParse(curVal, out curNum))
                        throw new Exception($"Invalid serial number current value: {curVal}");

                    int strLen = 0;

                    if (!string.IsNullOrEmpty(strLenVal) && !int.TryParse(strLenVal, out strLen))
                        throw new Exception($"Invalid serial number string length: {strLenVal}");

                    serialNumGens.Add(new SerialNumberGenerator(startNum, curNum)
                    {
                        Name = name,
                        StringLength = strLen > 0 ? strLen : 0,
                    });
                }
            }

            config.SerialNumberGenerators = [.. serialNumGens];

            var flags = EmitFlags.None;

            // get options
            foreach (var element in xDoc.Descendants(ELEMENT_OPTIONS).Elements())
            {
                var value = element.Value;
                bool isYes = Values.ADIF_BOOLEAN_TRUE.Equals(value);

                switch (element.Name.LocalName)
                {
                    case ELEMENT_LOWERCASE_TAG_NAMES:
                        if (isYes)
                            flags |= EmitFlags.LowercaseTagNames;
                        break;

                    case ELEMENT_ADD_PROGRAM_HEADER_TAGS:
                        if (isYes)
                            flags |= EmitFlags.AddProgramHeaderTags;
                        break;

                    case ELEMENT_MIRROR_OPERATOR_STATION_CALLSIGN:
                        if (isYes)
                            flags |= EmitFlags.MirrorOperatorAndStationCallSign;
                        break;

                    case ELEMENT_ADD_CREATED_TIMESTAMP_TAG:
                        if (isYes)
                            flags |= EmitFlags.AddCreatedTimestamp;
                        break;

                    default:
                        throw new Exception($"Invalid configuration option: {element.Name.LocalName}");
                }
            }
            config.EmitFlags = flags;

            return config;
        }

        const string ATTRIBUTE_NAME = "Name";
        const string ELEMENT_TAG = "Tag";
        const string ELEMENT_DEFAULTS = "Defaults";
        const string ELEMENT_USER_DEF_TAGS = "UserDefTags";
        const string ELEMENT_USER_DEF_TAG = "UserDefTag";
        const string ELEMENT_REPLACE_TAGS = "ReplaceTags";
        const string ELEMENT_REMOVE_TAGS = "RemoveTags";
        const string ELEMENT_ADD_TAGS = "AddTags";
        const string ELEMENT_OPTIONS = "Options";
        const string ELEMENT_SERIAL_NUMBER_GENERATORS = "SerialNumberGenerators";
        const string ELEMENT_SERIAL_NUMBER_GENERATOR = "SerialNumberGenerator";
        const string ATTRIBUTE_START = "Start";
        const string ATTRIBUTE_CURRENT = "Current";
        const string ATTRIBUTE_STRING_LENGTH = "StringLength";
        const string ATTRIBUTE_ID = "ID";
        const string ATTRIBUTE_DATA_TYPE = "DataType";
        const string ATTRIBUTE_ENUM = "Enum";
        const string ATTRIBUTE_RANGE = "Range";
        const string ATTRIBUTE_ADIF_VERSION = "AdifVersion";
        const string ELEMENT_ROOT = "AdifNetConfig";
        const string ELEMENT_LOWERCASE_TAG_NAMES = "LowercaseTagNames";
        const string ELEMENT_ADD_PROGRAM_HEADER_TAGS = "AddProgramHeaderTags";
        const string ELEMENT_MIRROR_OPERATOR_STATION_CALLSIGN = "MirrorOperatorAndStationCallSign";
        const string ELEMENT_ADD_CREATED_TIMESTAMP_TAG = "AddCreatedTimestampTag";
    }
}
