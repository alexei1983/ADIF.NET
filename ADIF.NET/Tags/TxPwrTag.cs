﻿
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TxPwrTag : NumberTag, ITag {

    public override string Name => TagNames.TxPwr;

    public TxPwrTag() { }

    public TxPwrTag(double value) : base(value) { }
  }
}
