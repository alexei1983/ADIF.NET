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
    /// <param name="lines"></param>
    void ParseConfig(IEnumerable<string> lines)
    {
      if (lines != null)
      {
        var values = lines.Where(line => !string.IsNullOrWhiteSpace(line) && 
                                         !line.Trim().StartsWith("#"))
                          .Select(line => line.Split(new char[] { '=' }, 2, 0))
                          .ToDictionary(parts => parts[0].Trim(), parts => parts.Length > 1 ? CleanValue(parts[1].Trim()) : null);

        if (values.ContainsKey("my_call"))
          MyCall = values["my_call"];

        if (values.ContainsKey("my_grid_square"))
          MyGridSquare = values["my_grid_square"];

        if (values.ContainsKey("my_name"))
          MyName = values["my_name"];

        if (values.ContainsKey("my_state"))
          MyState = values["my_state"];

        if (values.ContainsKey("my_dxcc"))
        {
          var myDxccStr = values["my_dxcc"];
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
        if (values.ContainsKey("adif_target_version"))
        {
          var verStr = values["adif_target_version"];
          if (Version.TryParse(verStr, out Version adifVer))
            ADIFTargetVersion = adifVer;
        }

        var emitSettings = EmitFlags.None;

        // parse emit flags
        if (values.ContainsKey("add_program_headers_on_emit"))
        {
          var addPrgDtsOnEmitStr = values["add_program_headers_on_emit"];
          if (ADIFBoolean.TryParse(addPrgDtsOnEmitStr, out bool? result) && result.HasValue)
            emitSettings = emitSettings | EmitFlags.AddProgramHeaderTags;
        }
      }
    }

    string CleanValue(string value)
    {
      value = value ?? string.Empty;
      return value.Trim('"');
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
  }
}
