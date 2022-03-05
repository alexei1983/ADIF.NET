using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ADIF.NET.Helpers;
using ADIF.NET.Types;

namespace ADIF.NET {

  /// <summary>
  /// 
  /// </summary>
  public class Configuration {


    public EmitFlags EmitFlags { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string ConfigFile { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int MyDXCC { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string MyGridSquare { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string MyCall { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string MyName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string MyState { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool AddProgramHeadersOnEmit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Version ADIFTargetVersion { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fullPath"></param>
    public Configuration(string fullPath)
    {
      ConfigFile = fullPath;
      ParseConfig(GetLinesFromFile(ConfigFile));
    }

    /// <summary>
    /// 
    /// </summary>
    public Configuration()
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
    IEnumerable<string> GetLinesFromFile(string file)
    {
      var fi = new FileInfo(file);
      if (fi.Exists)
        return File.ReadLines(ConfigFile);

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public int GetSerialNumberGenerator(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException("Name is required.", nameof(name));

      var strVal = GetValue(values, $"serial_{name}");

      if (!string.IsNullOrEmpty(strVal))
      {
        if (int.TryParse(strVal, out int serial))
          return serial;
      }

      throw new ArgumentException($"No generator exists with name '{name}'");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lines"></param>
    void ParseConfig(IEnumerable<string> lines)
    {
      if (lines != null)
      {
        values = lines.Where(line => !string.IsNullOrWhiteSpace(line) && 
                                     !line.Trim().StartsWith(Values.COMMENT_INDICATOR.ToString()))
                      .Select(line => line.Split(new char[] { '=' }, 2, 0))
                      .ToDictionary(parts => parts[0].Trim(), parts => parts.Length > 1 ? CleanValue(parts[1].Trim()) : null);

        if (values.ContainsKey(MY_CALL_CONFIG))
          MyCall = values[MY_CALL_CONFIG];

        if (values.ContainsKey(MY_GRIDSQUARE_CONFIG))
          MyGridSquare = values[MY_GRIDSQUARE_CONFIG];

        if (values.ContainsKey(MY_NAME_CONFIG))
          MyName = values[MY_NAME_CONFIG];

        if (values.ContainsKey(MY_STATE_CONFIG))
          MyState = values[MY_STATE_CONFIG];

        if (values.ContainsKey(MY_DXCC_CONFIG))
        {
          var myDxccStr = values[MY_DXCC_CONFIG];
          if (int.TryParse(myDxccStr, out int myDxcc))
            MyDXCC = myDxcc;
        }

        // validate DXCC and state
        if (MyDXCC > 0)
        {
          if (!Values.CountryCodes.IsValid(MyDXCC.ToString()))
            throw new Exception($"Invalid DXCC entity in configuration: {MyDXCC}");

          if (!string.IsNullOrEmpty(MyState))
          {
            if (!DXCCHelper.ValidatePrimarySubdivision(MyDXCC, MyState))
              throw new Exception($"DXCC entity {MyDXCC} does not contain primary administrative subdivision '{MyState}'");
          }
        }

        // parse ADIF target version
        if (values.ContainsKey(ADIF_TARGET_VERSION_CONFIG))
        {
          var verStr = values[ADIF_TARGET_VERSION_CONFIG];
          if (Version.TryParse(verStr, out Version adifVer))
            ADIFTargetVersion = adifVer;
        }

        var emitSettings = EmitFlags.None;

        // parse emit flags
        if (values.ContainsKey(ADD_PROGRAM_HEADERS_CONFIG))
        {
          var addPrgDtsOnEmitStr = values[ADD_PROGRAM_HEADERS_CONFIG];
          if (ADIFBoolean.TryParse(addPrgDtsOnEmitStr, out bool? result) && result.HasValue && result.Value)
            emitSettings = emitSettings | EmitFlags.AddProgramHeaderTags;
        }

        if (values.ContainsKey(LOWERCASE_TAG_NAMES_CONFIG))
        {
          var lowercaseTagNamesStr = values[LOWERCASE_TAG_NAMES_CONFIG];
          if (ADIFBoolean.TryParse(lowercaseTagNamesStr, out bool? result) && result.HasValue && result.Value)
            emitSettings = emitSettings | EmitFlags.LowercaseTagNames;
        }

        if (values.ContainsKey(ADD_MY_GRIDSQUARE_CONFIG))
        {
          var addMyGridSqrStr = values[ADD_MY_GRIDSQUARE_CONFIG];
          if (ADIFBoolean.TryParse(addMyGridSqrStr, out bool? result) && result.HasValue && result.Value)
            emitSettings = emitSettings | EmitFlags.AddMyGridSquare;
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

    string CleanValue(string value)
    {
      value = value ?? string.Empty;
      return value.Trim('"');
    }

    string GetValue(IDictionary<string, string> values, string key)
    {
      var val = values.FirstOrDefault(kvp => kvp.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
      return val.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    string FindConfigurationFile(string fileName)
    {
      string[] paths = new string[] {
                Environment.GetFolderPath(Environment.SpecialFolder.System),
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Environment.CurrentDirectory,
                GetAssemblyDirectory()
    };

      foreach (var p in paths)
      {
        var path = $@"{p}\{fileName}";
        if (new FileInfo(path).Exists)
          return path;
      }

      foreach (var s in Environment.GetLogicalDrives())
      {
        var path = $@"{s}\{fileName}";

        if (new FileInfo(path).Exists)
          return path;
      }

      return null;
    }

    string GetAssemblyDirectory()
    {
        var codeBase = Assembly.GetExecutingAssembly().CodeBase;
        var uri = new UriBuilder(codeBase);
        return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetADIFHeaderText()
    {
      if (string.IsNullOrWhiteSpace(MyCall))
        return Values.DEFAULT_ADIF_HEADER_TEXT;
      else
        return $"{Values.DEFAULT_ADIF_HEADER_TEXT} for {MyCall}";
    }

    IDictionary<string, string> values;
  }
}
