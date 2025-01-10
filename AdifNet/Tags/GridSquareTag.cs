
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's 2-character, 4-character, 6-character, or 8-character Maidenhead Grid Square.
    /// </summary>
    public class GridSquareTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.GridSquare;

        /// <summary>
        /// Creates a new GRIDSQUARE tag.
        /// </summary>
        public GridSquareTag() { }

        /// <summary>
        /// Creates a new GRIDSQUARE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public GridSquareTag(string value) : base(value) { }

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

                return strVal.IsAdifGridSquare();
            }
            return false;
        }
    }
}
