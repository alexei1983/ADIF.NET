
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging station's antenna azimuth in degrees.
    /// </summary>
    public class AntAzTag : NumberTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.AntAz;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override double MaxValue => 360;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override double MinValue => 0;

        /// <summary>
        /// Whether or not values over the maximum are allowed on import.
        /// </summary>
        public override bool AllowValuesOverMaxOnImport => true;

        /// <summary>
        /// Creates a new ANT_AZ tag.
        /// </summary>
        public AntAzTag() { }

        /// <summary>
        /// Creates a new ANT_AZ tag.
        /// </summary>
        /// <param name="azimuth">Initial tag value.</param>
        public AntAzTag(double azimuth)
        {

            if (azimuth > MaxValue)
            {
                importValue = azimuth;
                azimuth %= MaxValue;
            }

            base.SetValue(azimuth);
        }

        readonly double? importValue;
    }
}
