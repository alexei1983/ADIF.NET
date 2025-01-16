
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's extended Maidenhead locator.
    /// </summary>
    /// <remarks>For a 10-character Maidenhead locator, this value supplements the GRIDSQUARE 
    /// field by containing characters 9 and 10. For a 12-character Maidenhead locator, this value 
    /// supplements the GRIDSQUARE field by containing characters 9, 10, 11 and 12.
    /// </remarks>
    public class GridSquareExtTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.GridSquareExt;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifGridSquareExt();

        /// <summary>
        /// Creates a new GRIDSQUARE_EXT tag.
        /// </summary>
        public GridSquareExtTag() { }

        /// <summary>
        /// Creates a new GRIDSQUARE_EXT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public GridSquareExtTag(string value) : base(value) { }

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
