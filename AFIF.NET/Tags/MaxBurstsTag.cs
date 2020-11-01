﻿using System;
using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the maximum length of meteor scatter bursts heard by the logging station, in seconds.
  /// </summary>
  [DisplayName("Maximum length of meteor scatter bursts heard by the logging station, in seconds.")]
  public class MaxBurstsTag : NumberTag, ITag {

    public override string Name => TagNames.MaxBursts;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToDouble() >= 0;
      }
    }
  }
