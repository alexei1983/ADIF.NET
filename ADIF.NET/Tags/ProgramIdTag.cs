﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class ProgramIdTag : StringTag, ITag {

    public override string Name => TagNames.ProgramId;
    public override bool Header => true;
    
    public ProgramIdTag() { }

    public ProgramIdTag(string value) {
      SetValue(value);
      }
    }
  }
