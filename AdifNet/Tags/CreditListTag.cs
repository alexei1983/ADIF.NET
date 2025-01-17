﻿using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditListTag : StringTag, ITag
    {
        /// <summary>
        /// 
        /// </summary>
        public override string ValueSeparator => AdifConstants.Comma.ToString();

        /// <summary>
        /// 
        /// </summary>
        public override IAdifType AdifType => new AdifCreditList();

        /// <summary>
        /// 
        /// </summary>
        public override string TextValue
        {
            get
            {
                if (creditList != null)
                    return creditList.ToString();

                return base.TextValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CreditListTag()
        {
            creditList ??= [];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public CreditListTag(string value) : base(value)
        {
            creditList ??= [];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            return new AdifCreditList().IsValidValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void SetValue(string value)
        {
            creditList ??= [];

            if (new AdifCreditList().TryParse(value, out CreditList? list))
            {
                foreach (var item in list ?? [])
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
            creditList ??= [];
            creditList.Add(credit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credit"></param>
        /// <param name="medium"></param>
        public void AddValue(string credit, string medium)
        {
            creditList ??= [];
            creditList.Add(credit, medium);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClearValue()
        {
            creditList = [];
            base.ClearValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public IEnumerable<string> GetMediums(string credit)
        {
            creditList ??= [];
            return creditList.GetMediums(credit);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> GetCredits()
        {
            creditList ??= [];
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
