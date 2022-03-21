using System;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class CreditListTag : StringTag, ITag {

    public override string ValueSeparator => Values.COMMA.ToString();

    public override IADIFType ADIFType => new ADIFCreditList();

    public override string TextValue
    {
      get {
        if (creditList != null)
          return creditList.ToString();

        return base.TextValue;
      }
    }

    public CreditListTag() { 
      if (creditList == null)
        creditList = new CreditList();
    }

    public CreditListTag(string value) : base(value) {
      if (creditList == null)
        creditList = new CreditList();
    }

    public override bool ValidateValue(object value)
    {
      return base.ValidateValue(value) && ADIFCreditList.TryParse(value.ToString(), out _);
    }

    public override void SetValue(string value)
    {
      if (creditList == null)
        creditList = new CreditList();

      if (ADIFCreditList.TryParse(value, out CreditList list))
      {
        foreach (var item in list)
          creditList.Add(item.Credit, item.Medium);

        base.SetValue(value);
      }
    }

    public void AddValue(string credit)
    {
      if (creditList == null)
        creditList = new CreditList();

      creditList.Add(credit);
    }

    public void AddValue(string credit, string medium)
    {
      if (creditList == null)
        creditList = new CreditList();

      creditList.Add(credit, medium);
    }

    public override void ClearValue()
    {
      creditList = new CreditList();
      base.ClearValue();
    }

    public IEnumerable<string> GetMediums(string credit)
    {
      if (creditList == null)
        creditList = new CreditList();

      return creditList.GetMediums(credit);
    }

    public IEnumerable<string> GetCredits()
    {
      if (creditList == null)
        creditList = new CreditList();

      return creditList.Select(c => c.Credit);
    }

    public CreditList GetCreditList()
    {
      return creditList;
    }

    CreditList creditList;

  }
}
