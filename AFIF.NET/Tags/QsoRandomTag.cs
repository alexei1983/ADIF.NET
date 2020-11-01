﻿using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether the QSO was random or scheduled.
  /// </summary>
  [DisplayName("Indicates whether the QSO was random or scheduled.")]
  public class QsoRandomTag : BooleanTag, ITag {

    public override string Name => TagNames.QsoRandom;
    }
  }
