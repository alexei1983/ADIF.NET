using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's extended Maidenhead locator.
    /// </summary>
    /// <remarks>For a 10-character Maidenhead locator, this value supplements the MY_GRIDSQUARE 
    /// field by containing characters 9 and 10. For a 12-character Maidenhead locator, this value 
    /// supplements the MY_GRIDSQUARE field by containing characters 9, 10, 11 and 12.
    /// </remarks>
    public class MyGridSquareExtTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyGridSquareExt;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifGridSquareExt();

        /// <summary>
        /// Creates a new MY_GRIDSQUARE_EXT tag.
        /// </summary>
        public MyGridSquareExtTag() { }

        /// <summary>
        /// Creates a new MY_GRIDSQUARE_EXT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyGridSquareExtTag(string value) : base(value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            if (value is null)
                return true;

            if (value is string strVal)
            {
                if (string.IsNullOrEmpty(strVal))
                    return true;

                return AdifType.TryParse(strVal, out _);
            }
            return false;
        }
    }
}
