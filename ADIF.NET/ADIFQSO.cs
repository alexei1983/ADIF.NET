using System;
using System.Linq;
using ADIF.NET.Tags;
using ADIF.NET.Helpers;
using ADIF.NET.Types;

namespace ADIF.NET {

  /// <summary>
  /// Contains the ADIF tags describing a QSO.
  /// </summary>
  public class ADIFQSO : ADIFTagCollection {

    public override ITag this[int index] {

      get => base[index];

      set
      {
        if (value is null)
          return;

        if (value.Header)
          throw new ArgumentException("Cannot insert header tag into QSO.");

        if (Contains(value.GetType()))
          throw new ArgumentException($"QSO already contains tag '{value.Name}'");

        base[index] = value;
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFQSO"/> class.
    /// </summary>
    public ADIFQSO() : base() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFQSO"/> class.
    /// </summary>
    /// <param name="tags">Tags to add to the QSO.</param>
    public ADIFQSO(params ITag[] tags) : this()
    {
      AddRange(tags);
    }

    /// <summary>
    /// Adds multiple tags to the current <see cref="ADIFQSO"/>.
    /// </summary>
    /// <param name="tags">Tags to add to the QSO.</param>
    public override void AddRange(params ITag[] tags)
    {
      if (tags != null)
        foreach (var tag in tags)
          Add(tag);
    }

    /// <summary>
    /// Adds a single tag to the current <see cref="ADIFQSO"/>.
    /// </summary>
    /// <param name="tag">Tag to add to the QSO.</param>
    public override void Add(ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Cannot insert header tag into QSO.");

      if (Contains(tag.Name))
        throw new ArgumentException($"QSO already contains tag '{tag.Name}'");

      base.Add(tag);
    }

    /// <summary>
    /// Adds a <see cref="CallTag"/> to the current QSO.
    /// </summary>
    /// <param name="call">Callsign to add as the contacted station.</param>
    public void AddCall(string call)
    {
      Add(new CallTag(call));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="call"></param>
    public void SetCall(string call)
    {
      AddOrReplace(new CallTag(call));
    }

    /// <summary>
    /// Adds a <see cref="QSODateTag"/> to the current QSO.
    /// </summary>
    /// <param name="qsoDate">Date to add as the QSO date.</param>
    public void AddQSODate(DateTime qsoDate)
    {
      Add(new QSODateTag(qsoDate));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDate"></param>
    public void SetQSODate(DateTime qsoDate)
    {
      AddOrReplace(new QSODateTag(qsoDate));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOn"></param>
    public void AddQSODateTimeOn(DateTime qsoDateTimeOn)
    {
      AddQSODate(qsoDateTimeOn);
      AddTimeOn(qsoDateTimeOn);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOn"></param>
    public void SetQSODateTimeOn(DateTime qsoDateTimeOn)
    {
      SetQSODate(qsoDateTimeOn);
      SetTimeOn(qsoDateTimeOn);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOff"></param>
    public void SetQSODateTimeOff(DateTime qsoDateTimeOff)
    {
      SetQSODateOff(qsoDateTimeOff);
      SetTimeOff(qsoDateTimeOff);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOff"></param>
    public void AddQSODateTimeOff(DateTime qsoDateTimeOff)
    {
      AddQSODateOff(qsoDateTimeOff);
      AddTimeOff(qsoDateTimeOff);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateOff"></param>
    public void AddQSODateOff(DateTime qsoDateOff)
    {
      Add(new QSODateOffTag(qsoDateOff));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateOff"></param>
    public void SetQSODateOff(DateTime qsoDateOff)
    {
      AddOrReplace(new QSODateOffTag(qsoDateOff));
    }

    /// <summary>
    /// Adds a <see cref="TimeOffTag"/> to the current QSO.
    /// </summary>
    /// <param name="timeOff">Time to add as the QSO time-off.</param>
    public void AddTimeOff(DateTime timeOff)
    {
      Add(new TimeOffTag(timeOff));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeOff"></param>
    public void SetTimeOff(DateTime timeOff)
    {
      AddOrReplace(new TimeOffTag(timeOff));
    }

    /// <summary>
    /// Adds a <see cref="TimeOnTag"/> to the current QSO.
    /// </summary>
    /// <param name="timeOn">Time to add as the QSO time-on.</param>
    public void AddTimeOn(DateTime timeOn)
    {
      Add(new TimeOnTag(timeOn));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeOn"></param>
    public void SetTimeOn(DateTime timeOn)
    {
      AddOrReplace(new TimeOnTag(timeOn));
    }

    /// <summary>
    /// Adds a <see cref="ModeTag"/> to the current QSO.
    /// </summary>
    /// <param name="mode">Mode to add to the current QSO.</param>
    public void AddMode(string mode)
    {
      if (!Values.Modes.IsValid(mode))
        throw new ArgumentException($"Invalid mode: '{mode ?? string.Empty}'", nameof(mode));

      Add(new ModeTag(mode));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    public void SetMode(string mode)
    {
      if (!Values.Modes.IsValid(mode))
        throw new ArgumentException($"Invalid mode: '{mode ?? string.Empty}'", nameof(mode));

      AddOrReplace(new ModeTag(mode));
    }

    /// <summary>
    /// Adds a <see cref="BandTag"/> to the current QSO.
    /// </summary>
    /// <param name="band">Band to add to the current QSO.</param>
    public void AddBand(string band)
    {
      if (!Values.Bands.IsValid(band))
        throw new ArgumentException($"Invalid band: '{band}'");

      Add(new BandTag(band));
    }

    /// <summary>
    /// Sets the band for the current QSO.
    /// </summary>
    /// <param name="band">Band value.</param>
    public void SetBand(string band)
    {
      if (!Values.Bands.IsValid(band))
        throw new ArgumentException($"Invalid band: '{band}'");

      AddOrReplace(new BandTag(band));
    }

    /// <summary>
    /// Adds an <see cref="OperatorTag"/> to the current QSO.
    /// </summary>
    /// <param name="operatorCall">Callsign to add to the QSO as the operator.</param>
    public void AddOperator(string operatorCall)
    {
      Add(new OperatorTag(operatorCall));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatorCall"></param>
    public void SetOperator(string operatorCall)
    {
      AddOrReplace(new OperatorTag(operatorCall));
    }

    /// <summary>
    /// Adds a <see cref="CommentTag"/> to the current QSO.
    /// </summary>
    /// <param name="comment">Comment to add to the QSO.</param>
    public void AddComment(string comment)
    {
      Add(new CommentTag(comment));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="comment"></param>
    public void SetComment(string comment)
    {
      AddOrReplace(new CommentTag(comment));
    }

    /// <summary>
    /// Adds a <see cref="FreqTag"/> to the current QSO, optionally setting the band.
    /// </summary>
    /// <param name="frequency">Frequency to set for the current QSO.</param>
    /// <param name="setBand">Whether or not to set the band for the current QSO based on the frequency.</param>
    public void AddFreq(double frequency, bool addBand = false)
    {
      Add(new FreqTag(frequency));

      if (addBand)
      {
        var band = Band.Get(frequency);
        if (band != null)
          AddBand(band.Name);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="setBand"></param>
    public void SetFreq(double frequency, bool setBand = false)
    {
      AddOrReplace(new FreqTag(frequency));

      if (setBand)
      {
        var band = Band.Get(frequency);
        if (band != null)
          AddOrReplace(new BandTag(band.Name));
      }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    /// <param name="suffix"></param>
    public void SetRstRcvd(int readability, int strength, int tone, string suffix)
    {
      TagValidationHelper.ValidateRst(readability, strength, tone, suffix);
      AddOrReplace(new RstRcvdTag($"{readability}{strength}{(tone > 0 ? tone.ToString() : string.Empty)}{suffix ?? string.Empty}"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    /// <param name="suffix"></param>
    public void AddRstRcvd(int readability, int strength, int tone, string suffix)
    {
      if (!Contains(TagNames.RstRcvd))
        SetRstRcvd(readability, strength, tone, suffix);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    public void AddRstRcvd(int readability, int strength, int tone)
    {
       AddRstRcvd(readability, strength, tone, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    public void AddRstRcvd(int readability, int strength)
    {
      AddRstRcvd(readability, strength, -1, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public void AddRstRcvd(int db)
    {
      Add(new RstRcvdTag(db.ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public void SetRstRcvd(int db)
    {
      AddOrReplace(new RstRcvdTag(db.ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public void AddRstSent(int db)
    {
      Add(new RstSentTag(db.ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public void SetRstSent(int db)
    {
      AddOrReplace(new RstSentTag(db.ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    public void SetRstRcvd(int readability, int strength, int tone)
    {
      SetRstRcvd(readability, strength, tone, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    public void SetRstRcvd(int readability, int strength)
    {
      SetRstRcvd(readability, strength, -1, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    /// <param name="suffix"></param>
    public void SetRstSent(int readability, int strength, int tone, string suffix)
    {
      TagValidationHelper.ValidateRst(readability, strength, tone, suffix);
      AddOrReplace(new RstSentTag($"{readability}{strength}{(tone > 0 ? tone.ToString() : string.Empty)}{suffix ?? string.Empty}"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    /// <param name="suffix"></param>
    public void AddRstSent(int readability, int strength, int tone, string suffix)
    {
      if (!Contains(TagNames.RstSent))
        SetRstSent(readability, strength, tone, suffix);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    public void AddRstSent(int readability, int strength, int tone)
    {
      AddRstSent(readability, strength, tone, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    public void AddRstSent(int readability, int strength)
    {
      AddRstSent(readability, strength, -1, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    /// <param name="tone"></param>
    public void SetRstSent(int readability, int strength, int tone)
    {
      SetRstSent(readability, strength, tone, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readability"></param>
    /// <param name="strength"></param>
    public void SetRstSent(int readability, int strength)
    {
      SetRstSent(readability, strength, -1, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="decimalLatitude"></param>
    public void AddLat(decimal decimalLatitude)
    {
      Add(new LatTag(ADIFLocation.FromDecimalDegrees(decimalLatitude, LocationType.Latitude).ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="decimalLongitude"></param>
    public void AddLon(decimal decimalLongitude)
    {
      Add(new LonTag(ADIFLocation.FromDecimalDegrees(decimalLongitude, LocationType.Longitude).ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="decimalMyLatitude"></param>
    public void AddMyLat(decimal decimalMyLatitude)
    {
      Add(new MyLatTag(ADIFLocation.FromDecimalDegrees(decimalMyLatitude, LocationType.Latitude).ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="decimalMyLongitude"></param>
    public void AddMyLon(decimal decimalMyLongitude)
    {
      Add(new MyLonTag(ADIFLocation.FromDecimalDegrees(decimalMyLongitude, LocationType.Longitude).ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatorName"></param>
    /// <param name="streetAddress"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="postalCode"></param>
    /// <param name="countryName"></param>
    /// <param name="dxccCode"></param>
    public void SetAddress(string operatorName,
                           string streetAddress,
                           string city,
                           string state,
                           string postalCode,
                           string countryName,
                           string dxccCode)
    {
      if (string.IsNullOrEmpty(operatorName) || string.IsNullOrEmpty(streetAddress) ||
        string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(postalCode) ||
        string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(dxccCode))
        throw new ArgumentException("Cannot add address: missing one or more required values.");

      var dxccEntity = Values.CountryCodes.GetValue(dxccCode);

      if (dxccEntity == null)
        throw new ArgumentException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", nameof(dxccCode));

      if (!DXCCHelper.ValidatePrimarySubdivision(DXCCHelper.ConvertDXCC(dxccCode), state))
        throw new Exception($"DXCC entity {dxccCode} does not contain primary administrative subdivision '{state}'");

      var addressValue = $"{operatorName}{Values.LINE_ENDING}{streetAddress}{Values.LINE_ENDING}{city}, {state} {postalCode}" +
                         $"{Values.LINE_ENDING}{countryName}";

      var addressTag = new AddressTag(addressValue);

      SetPrimaryAdminSubdivision(dxccCode, state, false);
      AddOrReplace(addressTag);
      AddOrReplace(new QTHTag(city));
      AddOrReplace(new CountryTag(countryName));
      SetName(operatorName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxccCode"></param>
    /// <param name="primaryAdminSubdivisionCode"></param>
    public void SetPrimaryAdminSubdivision(string dxccCode, string primaryAdminSubdivisionCode, bool setCountryNameFromDxcc = false)
    {
      var dxccEntity = Values.CountryCodes.GetValue(dxccCode);

      if (dxccEntity == null)
        throw new ArgumentException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", nameof(dxccCode));

      if (!DXCCHelper.ValidatePrimarySubdivision(DXCCHelper.ConvertDXCC(dxccCode), primaryAdminSubdivisionCode))
        throw new Exception($"DXCC entity {dxccCode} does not contain primary administrative subdivision '{primaryAdminSubdivisionCode}'");

      AddOrReplace(new StateTag(primaryAdminSubdivisionCode));
      AddOrReplace(new DXCCTag(dxccCode));

      if (setCountryNameFromDxcc)
        AddOrReplace(new CountryTag(dxccEntity.DisplayName));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatorName"></param>
    /// <param name="streetAddress"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="postalCode"></param>
    /// <param name="dxccCode"></param>
    public void SetAddress(string operatorName,
                           string streetAddress,
                           string city,
                           string state,
                           string postalCode,
                           string dxccCode)
    {
      if (string.IsNullOrEmpty(dxccCode))
        throw new ArgumentException("DXCC code is required.", nameof(dxccCode));

      var dxccEntity = Values.CountryCodes.GetValue(dxccCode);

      if (dxccEntity == null)
        throw new ArgumentException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", nameof(dxccCode));

      SetAddress(operatorName, streetAddress, city, state, postalCode, dxccEntity.DisplayName, dxccCode);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="postalCode"></param>
    /// <param name="countryName"></param>
    /// <param name="dxccCode"></param>
    public void SetMyAddress(string city, string state, string postalCode, string countryName, string dxccCode)
    {
      if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(postalCode) ||
        string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(dxccCode))
        throw new ArgumentException("Cannot set address: missing one or more required values.");

      var dxccEntity = Values.CountryCodes.GetValue(dxccCode);

      if (dxccEntity == null)
        throw new ArgumentException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", nameof(dxccCode));

      if (!DXCCHelper.ValidatePrimarySubdivision(DXCCHelper.ConvertDXCC(dxccCode), state))
        throw new Exception($"DXCC entity {dxccCode} does not contain primary administrative subdivision '{state}'");

      AddOrReplace(new MyDXCCTag(dxccEntity.Code));
      AddOrReplace(new MyCityTag(city));
      AddOrReplace(new MyStateTag(state));
      AddOrReplace(new MyCountryTag(countryName));
      AddOrReplace(new MyPostalCodeTag(postalCode));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receivedOn"></param>
    /// <param name="via"></param>
    public void MarkQSLReceived(DateTime receivedOn, string via)
    {
      AddOrReplace(new QSLRcvdTag(Values.ADIF_BOOLEAN_TRUE));

      if (!string.IsNullOrEmpty(via))
      {
        if (!Values.Via.IsValid(via))
          throw new ArgumentException($"'{via}' is not a valid QSL means.");

        AddOrReplace(new QSLRcvdViaTag(via));
      }

      if (receivedOn != DateTime.MinValue)
        AddOrReplace(new QSLRvcdDateTag(receivedOn));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receivedOn"></param>
    public void MarkQSLReceived(DateTime receivedOn)
    {
      MarkQSLReceived(receivedOn, null);
    }

    /// <summary>
    /// 
    /// </summary>
    public void MarkQSLReceived()
    {
      MarkQSLReceived(DateTime.MinValue, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sentOn"></param>
    /// <param name="sentVia"></param>
    public void MarkQSLSent(DateTime sentOn, string sentVia)
    {
      AddOrReplace(new QSLSentTag(Values.ADIF_BOOLEAN_TRUE));

      if (!string.IsNullOrEmpty(sentVia))
      {
        if (!Values.Via.IsValid(sentVia))
          throw new ArgumentException($"'{sentVia}' is not a valid QSL means.");

        AddOrReplace(new QSLSentViaTag(sentVia));
      }

      if (sentOn != DateTime.MinValue)
        AddOrReplace(new QSLSentDateTag(sentOn));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sentOn"></param>
    public void MarkQSLSent(DateTime sentOn)
    {
      MarkQSLSent(sentOn, null);
    }

    /// <summary>
    /// 
    /// </summary>
    public void MarkQSLSent()
    {
      MarkQSLSent(DateTime.MinValue, null);
    }


    /// <summary>
    /// Adds a <see cref="NameTag"/> to the current QSO.
    /// </summary>
    /// <param name="name">Personal name of the contacted station.</param>
    public void AddName(string name)
    {
      Add(new NameTag(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
      AddOrReplace(new NameTag(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newCreditsGranted"></param>
    public void MergeCreditGranted(CreditList newCreditsGranted)
    {
      if (newCreditsGranted == null || newCreditsGranted.Count < 1)
        return;

      var creditGranted = GetTag(TagNames.CreditGranted) as CreditListTag;

      if (creditGranted == null)
        creditGranted = new CreditListTag();

      var existingList = creditGranted.GetCreditList();

      foreach (var credit in newCreditsGranted)
      {
        if (!existingList.Contains(credit))
          existingList.Add(credit);
      }

      AddOrReplace(new CreditGrantedTag(existingList.ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newCreditsGranted"></param>
    public void MergeCreditGranted(string newCreditsGranted)
    {
      if (string.IsNullOrEmpty(newCreditsGranted))
        return;

      if (!ADIFCreditList.TryParse(newCreditsGranted, out CreditList result))
        throw new ArgumentException($"Invalid credit string: '{newCreditsGranted}'", nameof(newCreditsGranted));

      MergeCreditGranted(result);
    }

    /// <summary>
    /// Retrieves the date and time that the QSO started.
    /// </summary>
    public DateTime? GetQSODateTimeOn()
    {
      DateTime? result = null;

      if (Contains(typeof(QSODateTag)))
      {
        var qsoDate = this.FirstOrDefault(q => q.GetType().Equals(typeof(QSODateTag)))?
                                                          .Value as DateTime?;

        if (qsoDate.HasValue && Contains(typeof(TimeOnTag)))
        {
          var timeOn = this.FirstOrDefault(q => q.GetType().Equals(typeof(TimeOnTag)))?
                                                           .Value as DateTime?;
          if (timeOn.HasValue)
          {
            result = new DateTime(qsoDate.Value.Year,
                                  qsoDate.Value.Month,
                                  qsoDate.Value.Day,
                                  timeOn.Value.Hour,
                                  timeOn.Value.Minute,
                                  timeOn.Value.Second);
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Retrieves the date and time that the QSO ended.
    /// </summary>
    public DateTime? GetQSODateTimeOff()
    {
      DateTime? result = null;

      if (Contains(typeof(QSODateOffTag)))
      {
        var qsoDate = this.FirstOrDefault(q => q.GetType().Equals(typeof(QSODateOffTag)))?
                                                          .Value as DateTime?;

        if (qsoDate.HasValue && Contains(typeof(TimeOffTag)))
        {
          var timeOn = this.FirstOrDefault(q => q.GetType().Equals(typeof(TimeOffTag)))?
                                                           .Value as DateTime?;
          if (timeOn.HasValue)
          {
            result = new DateTime(qsoDate.Value.Year,
                                  qsoDate.Value.Month,
                                  qsoDate.Value.Day,
                                  timeOn.Value.Hour,
                                  timeOn.Value.Minute,
                                  timeOn.Value.Second);
          }
        }
      }
      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ValidateFrequencyBand()
    {
      var freqTag = GetTag(TagNames.Freq) as FreqTag;
      var bandTag = GetTag(TagNames.Band) as BandTag;

      TagValidationHelper.ValidateFrequencyBand(freqTag, bandTag);

      var freqRxTag = GetTag(TagNames.FreqRx) as FreqRxTag;
      var bandRxTag = GetTag(TagNames.BandRx) as BandRxTag;

      TagValidationHelper.ValidateFrequencyBand(freqRxTag, bandRxTag);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ValidatePrimaryAdminSubdivision()
    {
      var dxccTag = GetTag(TagNames.DXCC);
      var primarySubTag = GetTag(TagNames.State) ?? GetTag(TagNames.VEProv);

      TagValidationHelper.ValidatePrimaryAdminSubdivision(dxccTag, primarySubTag);

      var myDxccTag = GetTag(TagNames.MyDXCC);
      var myPrimarySubTag = GetTag(TagNames.MyState);

      TagValidationHelper.ValidatePrimaryAdminSubdivision(myDxccTag, myPrimarySubTag);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ValidateSecondaryAdminSubdivision()
    {
      var dxccTag = GetTag(TagNames.DXCC);
      var primarySubTag = GetTag(TagNames.State) ?? GetTag(TagNames.VEProv);
      var secondarySubTag = GetTag(TagNames.Cnty);

      TagValidationHelper.ValidateAdminSubdivisions(dxccTag, primarySubTag, secondarySubTag);

      var myDxccTag = GetTag(TagNames.MyDXCC);
      var myPrimarySubTag = GetTag(TagNames.MyState);
      var mySecondarySubTag = GetTag(TagNames.MyCnty);

      TagValidationHelper.ValidateAdminSubdivisions(myDxccTag, myPrimarySubTag, mySecondarySubTag);
    }

    /// <summary>
    /// Inserts a tag into the current <see cref="ADIFQSO"/> at the specified index.
    /// </summary>
    /// <param name="index">Index at which the tag will be inserted.</param>
    /// <param name="tag">Tag that will be inserted into the QSO.</param>
    public override void Insert(int index, ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Cannot insert header tag into QSO.");

      if (Contains(tag.GetType()))
        throw new ArgumentException($"QSO already contains tag '{tag.Name}'");

      base.Insert(index, tag);
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ValidateValues(bool throwOnInvalid = false)
    {
      foreach (var tag in this)
      {
        if (!tag.ValidateValue())
        {
          if (throwOnInvalid)
            throw new Exception($"Invalid value for ADIF tag '{tag.Name}': {tag.TextValue}");

          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// Determines whether or not the current <see cref="ADIFQSO"/> contains the specified tags.
    /// </summary>
    /// <param name="tagNames">Names of tags to check.</param>
    public bool HasTags(params string[] tagNames)
    {
      if (tagNames == null)
        throw new ArgumentNullException(nameof(tagNames), "Tag name(s) are required.");

      foreach (var tagName in tagNames)
        if (!Contains(tagName))
          return false;

      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagTypes"></param>
    public bool HasTags(params Type[] tagTypes)
    {
      if (tagTypes == null)
        throw new ArgumentNullException(nameof(tagTypes), "Tag type(s) are required.");

      foreach (var tagType in tagTypes)
        if (!Contains(tagType))
          return false;

      return true;
    }
  }
}
