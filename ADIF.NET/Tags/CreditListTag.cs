using System;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class CreditListTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string ValueSeparator => Values.COMMA.ToString();

    /// <summary>
    /// 
    /// </summary>
    public override IADIFType ADIFType => new ADIFCreditList();

    /// <summary>
    /// 
    /// </summary>
    public override string TextValue
    {
      get {
        if (creditList != null)
          return creditList.ToString();

        return base.TextValue;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public CreditListTag() { 
      if (creditList == null)
        creditList = new CreditList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public CreditListTag(string value) : base(value) {
      if (creditList == null)
        creditList = new CreditList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      return ADIFCreditList.IsValidValue(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override void SetValue(string value)
    {
      if (creditList == null)
        creditList = new CreditList();

      if (ADIFCreditList.TryParse(value, out CreditList list))
      {
        foreach (var item in list)
        {
          if (!string.IsNullOrEmpty(item.Medium))
            creditList.Add(item.Credit, item.Medium);
          else
            creditList.Add(item.Credit);
        }

        base.SetValue(value);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="credit"></param>
    public void AddValue(string credit)
    {
      if (creditList == null)
        creditList = new CreditList();

      creditList.Add(credit);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="credit"></param>
    /// <param name="medium"></param>
    public void AddValue(string credit, string medium)
    {
      if (creditList == null)
        creditList = new CreditList();

      creditList.Add(credit, medium);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void ClearValue()
    {
      creditList = new CreditList();
      base.ClearValue();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="credit"></param>
    /// <returns></returns>
    public IEnumerable<string> GetMediums(string credit)
    {
      if (creditList == null)
        creditList = new CreditList();

      return creditList.GetMediums(credit);
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<string> GetCredits()
    {
      if (creditList == null)
        creditList = new CreditList();

      return creditList.GetCredits();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public CreditList GetCreditList()
    {
      return creditList;
    }

    CreditList creditList;

  }
}
