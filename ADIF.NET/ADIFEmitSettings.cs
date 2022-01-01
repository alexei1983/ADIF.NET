using System;

namespace ADIF.NET {
  
  [Flags]
  public enum EmitFlags {

    None = 0,

    LowercaseTagNames = 2,

    AddCreatedTimestampIfNotPresent = 4,

    AddProgramIdIfNotPresent = 8,

    MirrorOperatorAndStationCallSign = 16

  }
}
