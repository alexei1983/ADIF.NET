﻿using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the logging station's equipment.
  /// </summary>
  [DisplayName("The description of the logging station's equipment.")]
  public class MyRigIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyRigIntl;
    }
  }
