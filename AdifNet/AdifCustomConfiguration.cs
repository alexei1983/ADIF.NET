using System.Reflection;
using org.goodspace.Data.Radio.Adif.Helpers;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// 
    /// </summary>
    public class AdifCustomConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public EmitFlags EmitFlags { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ConfigFile { get; set; }

        /// <summary>
        /// Preset DXCC country code for the logging station.
        /// </summary>
        public int? MyDxcc { get; set; }

        /// <summary>
        /// Preset grid square for the logging station.
        /// </summary>
        public string? MyGridSquare { get; set; }

        /// <summary>
        /// Preset callsign for the logging station.
        /// </summary>
        public string? MyCall { get; set; }

        /// <summary>
        /// Preset personal name for the logging station.
        /// </summary>
        public string? MyName { get; set; }

        /// <summary>
        /// Preset primary administrative subdivision for the logging station.
        /// </summary>
        public string? MyState { get; set; }

        /// <summary>
        /// Whether or not to add PROGRAMID and PROGRAMVERSION header tags when generating ADIF or ADX.
        /// </summary>
        public bool AddProgramHeadersOnEmit { get; set; }

        /// <summary>
        /// Target ADIF version.
        /// </summary>
        public Version? AdifTargetVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        public AdifCustomConfiguration(string fullPath)
        {
            ConfigFile = Path.GetFullPath(fullPath);
            ParseConfig(GetLinesFromFile(ConfigFile));
        }

        /// <summary>
        /// 
        /// </summary>
        public AdifCustomConfiguration()
        {
            var path = FindConfigurationFile(Values.ADIF_NET_CONFIG_FILE_NAME);
            if (!string.IsNullOrEmpty(path))
            {
                ConfigFile = path;
                ParseConfig(GetLinesFromFile(ConfigFile));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        static IEnumerable<string> GetLinesFromFile(string file)
        {
            var fi = new FileInfo(file);
            if (fi.Exists)
                return File.ReadLines(fi.FullName);

            throw new FileNotFoundException($"ADIF custom configuration file not found: {file}", file);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public SerialNumberGenerator GetSerialNumberGenerator(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required.", nameof(name));

            var startStrVal = GetValue(values, $"serial_{name}_start");
            var currentStrVal = GetValue(values, $"serial_{name}_current");

            int startSerial = 1, currentSerial = 1;

            if (!string.IsNullOrEmpty(startStrVal))
            {
                if (!int.TryParse(startStrVal, out startSerial))
                    throw new Exception($"Invalid starting serial number for generator '{name}': {startStrVal}");
            }

            if (!string.IsNullOrEmpty(currentStrVal))
            {
                if (!int.TryParse(currentStrVal, out currentSerial))
                    throw new Exception($"Invalid current serial number for generator '{name}': {currentStrVal}");
            }

            return new SerialNumberGenerator(startSerial, currentSerial);

            throw new ArgumentException($"No generator exists with name '{name}'", nameof(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        void ParseConfig(IEnumerable<string> lines)
        {
            if (lines.Any())
            {
                values = lines.Where(line => !string.IsNullOrWhiteSpace(line) &&
                                             !line.Trim().StartsWith(Values.COMMENT_INDICATOR.ToString()))
                              .Select(line => line.Split(separator, 2, 0))
                              .ToDictionary(parts => parts[0].Trim(), parts => parts.Length > 1 ? CleanValue(parts[1].Trim()) : string.Empty);

                if (values.TryGetValue(MY_CALL_CONFIG, out string? call))
                    MyCall = call;

                if (values.TryGetValue(MY_GRIDSQUARE_CONFIG, out string? gridSquare))
                    MyGridSquare = gridSquare;

                if (values.TryGetValue(MY_NAME_CONFIG, out string? name))
                    MyName = name;

                if (values.TryGetValue(MY_STATE_CONFIG, out string? state))
                    MyState = state;

                if (values.TryGetValue(MY_DXCC_CONFIG, out string? dxcc))
                {
                    if (int.TryParse(dxcc, out int myDxcc))
                        MyDxcc = myDxcc;
                }

                // validate DXCC and state
                if (MyDxcc.HasValue && MyDxcc.Value > 0)
                {
                    if (!Values.CountryCodes.IsValid(MyDxcc.ToString()))
                        throw new Exception($"Invalid DXCC entity in configuration: {MyDxcc}");

                    if (!string.IsNullOrEmpty(MyState) && MyDxcc.HasValue)
                    {
                        if (!DxccHelper.ValidatePrimarySubdivision(MyDxcc.Value, MyState))
                            throw new Exception($"DXCC entity {MyDxcc} does not contain primary administrative subdivision '{MyState}'");
                    }
                }

                // parse ADIF target version
                if (values.TryGetValue(ADIF_TARGET_VERSION_CONFIG, out string? version))
                {
                    if (Version.TryParse(version, out Version? adifVer))
                        AdifTargetVersion = adifVer;
                }

                var emitSettings = EmitFlags.None;

                // parse emit flags
                if (values.TryGetValue(ADD_PROGRAM_HEADERS_CONFIG, out string? addProgHeaders))
                {
                    if (AdifBoolean.TryParse(addProgHeaders, out bool? result) && result.HasValue && result.Value)
                        emitSettings |= EmitFlags.AddProgramHeaderTags;
                }

                if (values.TryGetValue(LOWERCASE_TAG_NAMES_CONFIG, out string? lowerTags))
                {
                    if (AdifBoolean.TryParse(lowerTags, out bool? result) && result.HasValue && result.Value)
                        emitSettings |= EmitFlags.LowercaseTagNames;
                }

                if (values.TryGetValue(ADD_MY_GRIDSQUARE_CONFIG, out string? addGridSquare))
                {
                    if (AdifBoolean.TryParse(addGridSquare, out bool? result) && result.HasValue && result.Value)
                        emitSettings |= EmitFlags.AddMyGridSquare;
                }

                EmitFlags = emitSettings;
            }
        }

        const string ADD_PROGRAM_HEADERS_CONFIG = "add_program_headers_on_emit";
        const string LOWERCASE_TAG_NAMES_CONFIG = "emit_lowercase_tag_names";
        const string ADD_MY_GRIDSQUARE_CONFIG = "add_my_gridsquare_to_qsos";
        const string ADIF_TARGET_VERSION_CONFIG = "adif_target_version";
        const string MY_DXCC_CONFIG = "my_dxcc";
        const string MY_STATE_CONFIG = "my_state";
        const string MY_NAME_CONFIG = "my_name";
        const string MY_GRIDSQUARE_CONFIG = "my_grid_square";
        const string MY_CALL_CONFIG = "my_call";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string CleanValue(string value)
        {
            value ??= string.Empty;
            return value.Trim('"');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        static string GetValue(IDictionary<string, string> values, string key)
        {
            var val = values.FirstOrDefault(kvp => kvp.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return val.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static IEnumerable<string> GetSearchPaths()
        {
            string[] paths = [
                      Environment.GetFolderPath(Environment.SpecialFolder.System),
                      Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                      Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                      Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                      Environment.CurrentDirectory,
                      GetAssemblyDirectory()
          ];

            foreach (var path in paths)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var _path = Path.GetFullPath(path);
                    if (!string.IsNullOrEmpty(_path))
                        yield return _path;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        static string? FindConfigurationFile(string fileName)
        {
            var paths = GetSearchPaths();

            foreach (var p in paths)
            {
                var path = Path.Combine(p, fileName);
                if (new FileInfo(path).Exists)
                    return path;
            }

            foreach (var s in Environment.GetLogicalDrives())
            {
                var path = Path.Combine(s, fileName);
                if (new FileInfo(path).Exists)
                    return path;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var uri = new UriBuilder(codeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path)) ?? Environment.CurrentDirectory;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetAdifHeaderText()
        {
            if (string.IsNullOrWhiteSpace(MyCall))
                return Values.DEFAULT_ADIF_HEADER_TEXT;
            else
                return $"{Values.DEFAULT_ADIF_HEADER_TEXT} for {MyCall}";
        }

        Dictionary<string, string> values = [];
        static readonly char[] separator = ['='];
    }
}
