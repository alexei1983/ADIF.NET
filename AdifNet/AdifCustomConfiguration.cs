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
    public class AdifNetConfigurationParser
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
        /// <exception cref="Exception"></exception>
        public AdifCustomConfiguration Parse()
        {
            if (xDoc == null || xDoc.Root == null || !xDoc.Descendants().Any())
                throw new Exception("No configuration found to parse.");

            if (!xDoc.Root.Name.LocalName.Equals("AdifNetConfig"))
                throw new Exception("Not an AdifNet configuration file.");

            var config = new AdifCustomConfiguration();

            var versionStr = xDoc.Root.Attribute("AdifVersion")?.Value;

            if (!string.IsNullOrEmpty(versionStr))
            {
                if (!Version.TryParse(versionStr, out var version))
                    throw new Exception($"Invalid ADIF version: {versionStr}");
                config.AdifTargetVersion = version;
            }

            List<UserDefTag> userDefTags = [];

            // get user-defined tags
            foreach (var element in xDoc.Descendants("UserDefTags").Elements())
            {
                if (!element.Name.LocalName.Equals("UserDefTag"))
                    continue;

                var fieldId = element.Attribute("ID")?.Value;
                var dataType = element.Attribute("DataType")?.Value;
                var enumStr = element.Attribute("Enum")?.Value;
                var range = element.Attribute("Range")?.Value;
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
            foreach (var element in xDoc.Descendants("Defaults").Elements())
            {
                if (!element.Name.LocalName.Equals("Tag"))
                    continue;

                var tagName = element.Attribute("Name")?.Value;
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
            foreach (var element in xDoc.Descendants("AddTags").Elements())
            {
                if (!element.Name.LocalName.Equals("Tag"))
                    continue;

                var tagName = element.Attribute("Name")?.Value;
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
            foreach (var element in xDoc.Descendants("ReplaceTags").Elements())
            {
                if (!element.Name.LocalName.Equals("Tag"))
                    continue;

                var tagName = element.Attribute("Name")?.Value;
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
            foreach (var element in xDoc.Descendants("RemoveTags").Elements())
            {
                if (!element.Name.LocalName.Equals("Tag"))
                    continue;

                var tagName = element.Attribute("Name")?.Value;
                var tagValue = element.Value;

                if (!string.IsNullOrEmpty(tagName))
                {
                    tagName = tagName.ToUpper();

                    var tag = TagFactory.TagFromName(tagName) ??
                              userDefTags.FirstOrDefault(u => u.FieldName.Equals(tagName, StringComparison.OrdinalIgnoreCase)) ??
                              throw new Exception($"Invalid ADIF tag: {tagName}");

                    if (tag.IsUserDef && tag is UserDefTag userDefTag)
                        tag = new UserDefValueTag(userDefTag, tagValue);
                    else if(!string.IsNullOrEmpty(tagValue))
                        tag.SetValue(tagValue);

                    remove.Add(new AdifTagNameWithValue(tag.Name, tag.Value));
                }
            }

            config.RemoveTags = [.. remove];

            List<SerialNumberGenerator> serialNumGens = [];

            // get serial number generators
            foreach (var element in xDoc.Descendants("SerialNumberGenerators").Elements())
            {
                if (!element.Name.LocalName.Equals("SerialNumberGenerator"))
                    continue;

                var startVal = element.Attribute("Start")?.Value;
                var curVal = element.Attribute("Current")?.Value;
                var strLenVal = element.Attribute("StringLength")?.Value;
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
            foreach (var element in xDoc.Descendants("Options").Elements())
            {
                var value = element.Value;
                bool isYes = Values.ADIF_BOOLEAN_TRUE.Equals(value);

                switch (element.Name.LocalName)
                {
                    case "LowercaseTagNames":
                        if (isYes)
                            flags |= EmitFlags.LowercaseTagNames;
                        break;

                    case "AddProgramHeaderTags":
                        if (isYes)
                            flags |= EmitFlags.AddProgramHeaderTags;
                        break;

                    case "MirrorOperatorAndStationCallSign":
                        if (isYes)
                            flags |= EmitFlags.MirrorOperatorAndStationCallSign;
                        break;

                    case "AddCreatedTimestampTag":
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
    }
}
