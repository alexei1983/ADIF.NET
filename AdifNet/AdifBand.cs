using org.goodspace.Data.Radio.Adif.Helpers;
using System.Dynamic;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Represents an amateur radio band.
    /// </summary>
    public class AdifBand
    {
        /// <summary>
        /// The name of the band.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The upper frequency of the band.
        /// </summary>
        public double UpperFrequency { get; set; }

        /// <summary>
        /// The lower frequency of the band.
        /// </summary>
        public double LowerFrequency { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifBand"/> class.
        /// </summary>
        /// <param name="value">Value from the database.</param>
        public AdifBand(dynamic value)
        {
            if (value is IDictionary<string, object?> dict)
            {
                if (dict.TryGetValue(nameof(Name), out object? bandName) && bandName is string _bandName)
                    Name = _bandName;

                if (dict.TryGetValue(nameof(UpperFrequency), out object? upperFreq) && upperFreq is double _upperFreq)
                    UpperFrequency = _upperFreq;

                if (dict.TryGetValue(nameof(LowerFrequency), out object? lowerFreq) && lowerFreq is double _lowerFreq)
                    LowerFrequency = _lowerFreq;
            }
            Name ??= string.Empty;
        }

        /// <summary>
        /// Retrieves all amateur radio bands.
        /// </summary>
        public static List<AdifBand> Get()
        {
            var bands = new List<AdifBand>();

            var data = SQLiteHelper.Instance.ReadData(Resources.SqlGetBands);
            foreach (var d in data)
            {
                var band = new AdifBand(d);
                if (band != null)
                    bands.Add(band);
            }

            return bands;
        }

        /// <summary>
        /// Determines whether or not the specified frequency is a valid amateur radio frequency.
        /// </summary>
        /// <param name="frequency">Frequency to validate.</param>
        public static bool IsAmateurFrequency(double frequency)
        {
            var data = SQLiteHelper.Instance.ReadData(Resources.SqlValidateFrequency,
                                                      new Dictionary<string, object?>() { { Resources.SqlParameterFrequency, frequency } });
            return data.Count > 0;
        }

        /// <summary>
        /// Retrieves the amateur radio band associated with the specified frequency.
        /// </summary>
        /// <param name="frequency">Frequency value.</param>
        public static AdifBand? Get(double frequency)
        {
            var data = SQLiteHelper.Instance.ReadData(Resources.SqlValidateFrequency,
                                                      new Dictionary<string, object?>() { { Resources.SqlParameterFrequency, frequency } });
            if (data.Count > 0 && data[0] is ExpandoObject dataObj)
                return new AdifBand(dataObj);

            return null;
        }

        /// <summary>
        /// Determines whether or not the specified frequency is in the specified band.
        /// </summary>
        /// <param name="band">Amateur radio band.</param>
        /// <param name="frequency">Frequency value.</param>
        /// <returns>True if the frequency is in the band, else false.</returns>
        public static bool IsFrequencyInBand(string band, double frequency)
        {
            var data = SQLiteHelper.Instance.ReadData(Resources.SqlValidateFrequencyBand,
                                                      new Dictionary<string, object?>() { { Resources.SqlParameterFrequency, frequency },
                                                                                          { Resources.SqlParameterName, band } });
            return data.Count > 0;
        }
    }
}
