using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ADIF.NET.Tags;
using ADIF.NET.Helpers;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET {

  /// <summary>
  /// Contains the ADIF tags describing a QSO.
  /// </summary>
  public class ADIFQSO : ADIFTagCollection, IFormattable {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public override ITag this[int index] {

      get => base[index];

      set
      {
        if (value is null)
          return;

        if (value.Header)
          throw new ArgumentException("Cannot insert header tag into QSO.");

        if (Contains(value.Name))
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
    public void AddDateOn(DateTime qsoDate)
    {
      Add(new QSODateTag(qsoDate));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDate"></param>
    public void SetDateOn(DateTime qsoDate)
    {
      AddOrReplace(new QSODateTag(qsoDate));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOn"></param>
    public void AddDateTimeOn(DateTime qsoDateTimeOn)
    {
      AddDateOn(qsoDateTimeOn);
      AddTimeOn(qsoDateTimeOn);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOn"></param>
    public void SetDateTimeOn(DateTime qsoDateTimeOn)
    {
      SetDateOn(qsoDateTimeOn);
      SetTimeOn(qsoDateTimeOn);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOff"></param>
    public void SetDateTimeOff(DateTime qsoDateTimeOff)
    {
      SetDateOff(qsoDateTimeOff);
      SetTimeOff(qsoDateTimeOff);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateTimeOff"></param>
    public void AddDateTimeOff(DateTime qsoDateTimeOff)
    {
      AddDateOff(qsoDateTimeOff);
      AddTimeOff(qsoDateTimeOff);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateOff"></param>
    public void AddDateOff(DateTime qsoDateOff)
    {
      Add(new QSODateOffTag(qsoDateOff));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoDateOff"></param>
    public void SetDateOff(DateTime qsoDateOff)
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
        throw new InvalidEnumerationOptionException($"Invalid mode: '{mode ?? string.Empty}'", mode);

      Add(new ModeTag(mode));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    public void SetMode(string mode)
    {
      if (!Values.Modes.IsValid(mode))
        throw new InvalidEnumerationOptionException($"Invalid mode: '{mode ?? string.Empty}'", mode);

      AddOrReplace(new ModeTag(mode));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="submode"></param>
    public void AddMode(string mode, string submode)
    {
      AddMode(mode);

      if (!string.IsNullOrEmpty(submode))
        Add(new SubmodeTag(submode));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="submode"></param>
    public void SetMode(string mode, string submode)
    {
      SetMode(mode);

      if (!string.IsNullOrEmpty(submode))
        AddOrReplace(new SubmodeTag(submode));
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
    /// 
    /// </summary>
    public string GetOperator()
    {
      return CoalesceTagValues<string>(TagNames.Operator, TagNames.GuestOp);
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetOwnerCall()
    {
      return CoalesceTagValues<string>(TagNames.OwnerCallSign, TagNames.EqCall);
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetState()
    {
      return CoalesceTagValues<string>(TagNames.State, TagNames.VEProv);
    }

    /// <summary>
    /// Adds a <see cref="CommentTag"/> to the current QSO.
    /// </summary>
    /// <param name="comment">Comment to add to the QSO.</param>
    public void AddComment(string comment)
    {
      if (comment == null)
        comment = string.Empty;

      if (comment.IsASCII())
        Add(new CommentTag(comment));
      else
        Add(new CommentIntlTag(comment));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="comment"></param>
    public void SetComment(string comment)
    {
      if (comment == null)
        comment = string.Empty;

      if (comment.IsASCII())
        AddOrReplace(new CommentTag(comment));
      else
        AddOrReplace(new CommentIntlTag(comment));
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetComment()
    {
      return CoalesceTagValues<string>(TagNames.CommentIntl, TagNames.Comment);
    }

    /// <summary>
    /// Retrieves the value of the first tag in the list that has a non-null value.
    /// </summary>
    /// <typeparam name="T">Type of the tag values.</typeparam>
    /// <param name="tagNames">Names of the tags whose values will be coalesced.</param>
    public T CoalesceTagValues<T>(params string[] tagNames)
    {
      if (tagNames == null || tagNames.Length < 1)
        throw new ArgumentException("At least one tag name is required.");

      foreach (var tagName in tagNames)
      {
        var tag = GetTag(tagName);

        if (tag == null)
          continue;

        if (!tag.HasValue())
          continue;

        if (tag.Value is T tagVal)
          return tagVal;

        continue;
      }

      return default(T);
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
    /// <param name="iota"></param>
    /// <param name="islandId"></param>
    public void AddIOTA(string iota, int islandId)
    {
      if (!string.IsNullOrEmpty(iota))
        Add(new IOTATag(iota));

      if (islandId > 0)
        Add(new IOTAIslandIdTag(islandId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iota"></param>
    public void AddIOTA(string iota)
    {
      AddIOTA(iota, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iota"></param>
    /// <param name="islandId"></param>
    public void SetIOTA(string iota, int islandId)
    {
      if (!string.IsNullOrEmpty(iota))
        AddOrReplace(new IOTATag(iota));

      if (islandId > 0)
        AddOrReplace(new IOTAIslandIdTag(islandId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iota"></param>
    public void SetIOTA(string iota)
    {
      SetIOTA(iota, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myIota"></param>
    /// <param name="myIslandId"></param>
    public void AddMyIOTA(string myIota, int myIslandId)
    {
      if (!string.IsNullOrEmpty(myIota))
        Add(new MyIOTATag(myIota));

      if (myIslandId > 0)
        Add(new MyIOTAIslandIdTag(myIslandId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myIota"></param>
    public void AddMyIOTA(string myIota)
    {
      AddMyIOTA(myIota, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myIota"></param>
    /// <param name="myIslandId"></param>
    public void SetMyIOTA(string myIota, int myIslandId)
    {
      if (!string.IsNullOrEmpty(myIota))
        AddOrReplace(new MyIOTATag(myIota));

      if (myIslandId > 0)
        AddOrReplace(new MyIOTAIslandIdTag(myIslandId));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myIota"></param>
    public void SetMyIOTA(string myIota)
    {
      SetMyIOTA(myIota, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cqZone"></param>
    /// <param name="ituZone"></param>
    public void AddZones(int ituZone, int cqZone)
    {
      var cqzTag = new CQZTag(cqZone);
      if (cqzTag.ValidateValue())
        Add(cqzTag);

      var ituzTag = new ITUZTag(ituZone);
      if (ituzTag.ValidateValue())
        Add(ituzTag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ituZone"></param>
    /// <param name="cqZone"></param>
    public void SetZones(int ituZone, int cqZone)
    {
      var cqzTag = new CQZTag(cqZone);
      if (cqzTag.ValidateValue())
        AddOrReplace(cqzTag);

      var ituzTag = new ITUZTag(ituZone);
      if (ituzTag.ValidateValue())
        AddOrReplace(ituzTag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myItuZone"></param>
    /// <param name="myCqZone"></param>
    public void AddMyZones(int myItuZone, int myCqZone)
    {
      var cqzTag = new MyCQZoneTag(myCqZone);
      if (cqzTag.ValidateValue())
        Add(cqzTag);

      var ituzTag = new MyITUZoneTag(myItuZone);
      if (ituzTag.ValidateValue())
        Add(ituzTag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myItuZone"></param>
    /// <param name="myCqZone"></param>
    public void SetMyZones(int myItuZone, int myCqZone)
    {
      var cqzTag = new MyCQZoneTag(myCqZone);
      if (cqzTag.ValidateValue())
        AddOrReplace(cqzTag);

      var ituzTag = new MyITUZoneTag(myItuZone);
      if (ituzTag.ValidateValue())
        AddOrReplace(ituzTag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public void AddLatLon(decimal latitude, decimal longitude)
    {
      Add(new LatTag(latitude));
      Add(new LonTag(longitude));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public void SetLatLon(decimal latitude, decimal longitude)
    {
      AddOrReplace(new LatTag(latitude));
      AddOrReplace(new LonTag(longitude));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myLatitude"></param>
    /// <param name="myLongitude"></param>
    public void AddMyLatLon(decimal myLatitude, decimal myLongitude)
    {
      Add(new MyLatTag(myLatitude));
      Add(new MyLonTag(myLongitude));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myLatitude"></param>
    /// <param name="myLongitude"></param>
    public void SetMyLatLon(decimal myLatitude, decimal myLongitude)
    {
      AddOrReplace(new MyLatTag(myLatitude));
      AddOrReplace(new MyLonTag(myLongitude));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="antenna"></param>
    /// <param name="azimuth"></param>
    /// <param name="elevation"></param>
    public void AddMyAntenna(string antenna, double azimuth, double elevation)
    {
      if (!string.IsNullOrEmpty(antenna))
      {
        if (antenna.IsASCII())
          Add(new MyAntennaTag(antenna));
        else
          Add(new MyAntennaIntlTag(antenna));
      }

      if (azimuth >= -90)
      {
        var antAzTag = new AntAzTag(azimuth);
        if (antAzTag.ValidateValue())
          Add(antAzTag);
      }

      if (elevation >= 0)
      {
        var antElTag = new AntElTag(elevation);
        if (antElTag.ValidateValue())
          Add(antElTag);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="antenna"></param>
    /// <param name="azimuth"></param>
    /// <param name="elevation"></param>
    public void SetMyAntenna(string antenna, double azimuth, double elevation)
    {
      if (!string.IsNullOrEmpty(antenna))
      {
        if (antenna.IsASCII())
          AddOrReplace(new MyAntennaTag(antenna));
        else
          AddOrReplace(new MyAntennaIntlTag(antenna));
      }

      if (azimuth >= -90)
      {
        var antAzTag = new AntAzTag(azimuth);
        if (antAzTag.ValidateValue())
          AddOrReplace(antAzTag);
      }

      if (elevation >= 0)
      {
        var antElTag = new AntElTag(elevation);
        if (antElTag.ValidateValue())
          AddOrReplace(antElTag);
      }
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
        throw new ArgumentException("Cannot set address: missing one or more required values.");

      var dxccEntity = Values.CountryCodes.GetValue(dxccCode);

      if (dxccEntity == null)
        throw new DXCCException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", dxccCode);

      if (!DXCCHelper.ValidatePrimarySubdivision(DXCCHelper.ConvertDXCC(dxccCode), state))
        throw new DXCCException($"DXCC entity {dxccCode} does not contain primary administrative subdivision '{state}'");

      var addressValue = $"{operatorName}{Values.CARRIAGE_RETURN}{Values.NEWLINE}{streetAddress}" + 
                         $"{Values.CARRIAGE_RETURN}{Values.NEWLINE}{city}, {state} {postalCode}" +
                         $"{Values.CARRIAGE_RETURN}{Values.NEWLINE}{countryName}";

      if (addressValue.IsASCII())
        AddOrReplace(new AddressTag(addressValue));
      else
        AddOrReplace(new AddressIntlTag(addressValue));

      SetPrimaryAdminSubdivision(dxccCode, state, false);

      if (city.IsASCII())
        AddOrReplace(new QTHTag(city));
      else
        AddOrReplace(new QTHIntlTag(city));

      if (countryName.IsASCII())
        AddOrReplace(new CountryTag(countryName));
      else
        AddOrReplace(new CountryIntlTag(countryName));

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
        throw new DXCCException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", dxccCode);

      if (!DXCCHelper.ValidatePrimarySubdivision(DXCCHelper.ConvertDXCC(dxccCode), primaryAdminSubdivisionCode))
        throw new DXCCException($"DXCC entity {dxccCode} does not contain primary administrative subdivision '{primaryAdminSubdivisionCode}'");

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
        throw new DXCCException($"Invalid DXCC entity: {dxccCode ?? string.Empty}", dxccCode);

      SetAddress(operatorName, streetAddress, city, state, postalCode, dxccEntity.DisplayName, dxccCode);
    }

    /// <summary>
    /// Sets the logging station's address details.
    /// </summary>
    /// <param name="myStreetAddress">The logging station's street.</param>
    /// <param name="myCity">The logging station's city.</param>
    /// <param name="myState">The logging station's primary administrative subdivision (e.g. state, province, territory, etc)</param>
    /// <param name="myPostalCode">The logging station's postal code.</param>
    /// <param name="myCountryName">The name of the logging station's country.</param>
    /// <param name="myDxccCode">The logging station's DXCC entity code.</param>
    public void SetMyAddress(string myStreetAddress, 
                             string myCity,
                             string myState,
                             string myPostalCode,
                             string myCountryName,
                             string myDxccCode)
    {
      if (string.IsNullOrEmpty(myStreetAddress) || string.IsNullOrEmpty(myCity) || 
        string.IsNullOrEmpty(myState) || string.IsNullOrEmpty(myPostalCode) ||
        string.IsNullOrEmpty(myCountryName) || string.IsNullOrEmpty(myDxccCode))
        throw new ArgumentException("Cannot set address: missing one or more required values.");

      var dxccEntity = Values.CountryCodes.GetValue(myDxccCode);

      if (dxccEntity == null)
        throw new DXCCException($"Invalid DXCC entity: {myDxccCode ?? string.Empty}", myDxccCode);

      if (!DXCCHelper.ValidatePrimarySubdivision(DXCCHelper.ConvertDXCC(myDxccCode), myState))
        throw new DXCCException($"DXCC entity {myDxccCode} does not contain primary administrative subdivision '{myState}'");

      AddOrReplace(new MyDXCCTag(dxccEntity.Code));

      if (myStreetAddress.IsASCII())
        AddOrReplace(new MyStreetTag(myStreetAddress));
      else
        AddOrReplace(new MyStreetIntlTag(myStreetAddress));

      if (myCity.IsASCII())
        AddOrReplace(new MyCityTag(myCity));
      else
        AddOrReplace(new MyCityIntlTag(myCity));

      AddOrReplace(new MyStateTag(myState));

      if (myCountryName.IsASCII())
        AddOrReplace(new MyCountryTag(myCountryName));
      else
        AddOrReplace(new MyCountryIntlTag(myCountryName));

      if (myPostalCode.IsASCII())
        AddOrReplace(new MyPostalCodeTag(myPostalCode));
      else
        AddOrReplace(new MyPostalCodeIntlTag(myPostalCode));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receivedOn"></param>
    /// <param name="receivedVia"></param>
    public void MarkQSLReceived(DateTime receivedOn, string receivedVia)
    {
      AddOrReplace(new QSLRcvdTag(Values.ADIF_BOOLEAN_TRUE));

      if (!string.IsNullOrEmpty(receivedVia))
      {
        if (!Values.Via.IsValid(receivedVia))
          throw new ArgumentException($"'{receivedVia}' is not a valid QSL means.");

        AddOrReplace(new QSLRcvdViaTag(receivedVia));
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
    /// 
    /// </summary>
    /// <param name="sentOn"></param>
    /// <param name="sentStatus"></param>
    public void MarkLOTWSent(DateTime sentOn, bool addQSLSentTag = false)
    {
      if (sentOn != DateTime.MinValue)
        AddOrReplace(new LOTWQSLSentDateTag(sentOn));

      AddOrReplace(new LOTWQSLSentTag(Values.ADIF_BOOLEAN_TRUE));

      if (addQSLSentTag)
        MarkQSLSent(sentOn, "E");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receivedOn"></param>
    /// <param name="addQSLRcvdTag"></param>
    public void MarkLOTWReceived(DateTime receivedOn, bool addQSLRcvdTag = false)
    {
      if (receivedOn != DateTime.MinValue)
        AddOrReplace(new LOTWQSLReceivedDateTag(receivedOn));

      AddOrReplace(new LOTWQSLRcvdTag(Values.ADIF_BOOLEAN_TRUE));

      if (addQSLRcvdTag)
        MarkQSLReceived(receivedOn, "E");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sentOn"></param>
    /// <param name="addQSLSentTag"></param>
    public void MarkEQSLSent(DateTime sentOn, bool addQSLSentTag = false)
    {
      if (sentOn != DateTime.MinValue)
        AddOrReplace(new EQSLSentDateTag(sentOn));

      AddOrReplace(new EQSLSentStatusTag(Values.ADIF_BOOLEAN_TRUE));

      if (addQSLSentTag)
        MarkQSLSent(sentOn, "E");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receivedOn"></param>
    /// <param name="addQSLRcvdTag"></param>
    public void MarkEQSLReceived(DateTime receivedOn, bool addQSLRcvdTag = false)
    {
      if (receivedOn != DateTime.MinValue)
        AddOrReplace(new EQSLReceivedDateTag(receivedOn));

      AddOrReplace(new EQSLReceivedStatusTag(Values.ADIF_BOOLEAN_TRUE));

      if (addQSLRcvdTag)
        MarkQSLReceived(receivedOn, "E");
    }

    /// <summary>
    /// 
    /// </summary>
    public void MarkQSOModifiedSinceUpload()
    {
      if (Contains(TagNames.QRZQSOUploadStatus))
        Replace(new QRZQSOUploadStatusTag("M"));

      if (Contains(TagNames.ClubLogQSOUploadStatus))
        Replace(new ClubLogQSOUploadStatusTag("M"));

      if (Contains(TagNames.HrdLogQSOUploadStatus))
        Replace(new HRDLogQSOUploadStatusTag("M"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="programId"></param>
    /// <param name="dataType"></param>
    /// <param name="value"></param>
    public void AddAppDefinedField(string fieldName, string programId, string dataType, object value)
    {
      if (string.IsNullOrEmpty(programId))
        throw new ArgumentException("Program ID is required.", nameof(programId));

      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("Field name is required.", nameof(fieldName));

      if (dataType == null)
        dataType = string.Empty;

      Add(new AppDefTag(fieldName, programId, dataType, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="dataType"></param>
    /// <param name="value"></param>
    public void AddAppDefinedField(string fieldName, string dataType, object value)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("Field name is required.", nameof(fieldName));

      if (dataType == null)
        dataType = string.Empty;

      Add(new AppDefTag(fieldName, Values.DEFAULT_PROGRAM_ID, dataType, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    public void AddAppDefinedField(string fieldName, object value)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("Field name is required.", nameof(fieldName));

      Add(new AppDefTag(fieldName, Values.DEFAULT_PROGRAM_ID, null, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="programId"></param>
    /// <param name="dataType"></param>
    /// <param name="value"></param>
    public void SetAppDefinedField(string fieldName, string programId, string dataType, object value)
    {
      if (string.IsNullOrEmpty(programId))
        throw new ArgumentException("Program ID is required.", nameof(programId));

      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("Field name is required.", nameof(fieldName));

      if (dataType == null)
        dataType = string.Empty;

      AddOrReplace(new AppDefTag(fieldName, programId, dataType, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="dataType"></param>
    /// <param name="value"></param>
    public void SetAppDefinedField(string fieldName, string dataType, object value)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("Field name is required.", nameof(fieldName));

      if (dataType == null)
        dataType = string.Empty;

      AddOrReplace(new AppDefTag(fieldName, Values.DEFAULT_PROGRAM_ID, dataType, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    public void SetAppDefinedField(string fieldName, object value)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("Field name is required.", nameof(fieldName));

      AddOrReplace(new AppDefTag(fieldName, Values.DEFAULT_PROGRAM_ID, null, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    public void AddUserDefinedTag(UserDefTag field, object value)
    {
      Add(new UserDefValueTag(field, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="field"></param>
    /// <param name="value"></param>
    public void SetUserDefinedTag(UserDefTag field, object value)
    {
      AddOrReplace(new UserDefValueTag(field, value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qso"></param>
    public void Merge(ADIFQSO qso)
    {
      if (qso == null || qso.Count < 1)
        return;

      foreach (var tag in qso)
      {
        if (!Contains(tag.Name))
          Add(tag);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qso"></param>
    public void MergeAndReplace(ADIFQSO qso)
    {
      if (qso == null || qso.Count < 1)
        return;

      foreach (var tag in qso)
        AddOrReplace(tag);
    }

    /// <summary>
    /// Determines whether or not the specified QSO matches the current QSO.
    /// </summary>
    /// <param name="qso">QSO to match against the current QSO.</param>
    /// <param name="maxQsoDateTimeDiff">Maximum difference allowed between the current QSO date/time and the specified QSO date/time.</param>
    public bool IsMatch(ADIFQSO qso, TimeSpan maxQsoDateTimeDiff)
    {
      if (qso == null || qso.Count < 1)
        return false;

      var op = qso.GetTagValue<string>(TagNames.Operator);
      var dateTimeOn  = qso.GetQSODateTimeOn();
      var mode = qso.GetTagValue<string>(TagNames.Mode);
      var call = qso.GetTagValue<string>(TagNames.Call);
      var band = qso.GetTagValue<string>(TagNames.Band);

      if (string.IsNullOrEmpty(op) || string.IsNullOrEmpty(call) || string.IsNullOrEmpty(band) ||
        string.IsNullOrEmpty(mode) || !dateTimeOn.HasValue)
        throw new Exception("Not enough info to determine if QSO matches.");

      var thisOp = GetTagValue<string>(TagNames.Operator);
      var thisDateTimeOn = GetQSODateTimeOn();
      var thisMode = GetTagValue<string>(TagNames.Mode);
      var thisCall = GetTagValue<string>(TagNames.Call);
      var thisBand = GetTagValue<string>(TagNames.Band);

      if (string.IsNullOrEmpty(thisOp) || string.IsNullOrEmpty(thisCall) || string.IsNullOrEmpty(thisBand) ||
        string.IsNullOrEmpty(thisMode) || !thisDateTimeOn.HasValue)
        throw new Exception("Not enough info to determine if QSO matches.");

      if (!string.Equals(op, thisOp, StringComparison.OrdinalIgnoreCase))
        return false;

      if (!string.Equals(call, thisCall, StringComparison.OrdinalIgnoreCase))
        return false;

      if (!string.Equals(band, thisBand, StringComparison.OrdinalIgnoreCase))
        return false;

      if (!string.Equals(mode, thisMode, StringComparison.OrdinalIgnoreCase))
        return false;

      var qsoDateTimeDiff = thisDateTimeOn.Value - dateTimeOn.Value;

      if (qsoDateTimeDiff.TotalMilliseconds > maxQsoDateTimeDiff.TotalMilliseconds)
        return false;

      return true;
    }

    /// <summary>
    /// Determines whether or not the current QSO matches the specified QSO from the contacted station.
    /// </summary>
    /// <param name="qso">QSO from the contacted station.</param>
    /// <param name="maxQsoDateTimeDiff">Maximum difference allowed between the current QSO date/time and 
    /// the contacted station QSO date/time.</param>
    public bool IsMatchReverse(ADIFQSO qso, TimeSpan maxQsoDateTimeDiff)
    {
      if (qso == null || qso.Count < 1)
        return false;

      var op = qso.GetTagValue<string>(TagNames.Operator);
      var dateTimeOn = qso.GetQSODateTimeOn();
      var mode = qso.GetTagValue<string>(TagNames.Mode);
      var call = qso.GetTagValue<string>(TagNames.Call);
      var band = qso.GetTagValue<string>(TagNames.Band);

      if (string.IsNullOrEmpty(op) || string.IsNullOrEmpty(call) || string.IsNullOrEmpty(band) ||
        string.IsNullOrEmpty(mode) || !dateTimeOn.HasValue)
        throw new Exception("Not enough info to determine if QSO matches.");

      var thisOp = GetTagValue<string>(TagNames.Operator);
      var thisDateTimeOn = GetQSODateTimeOn();
      var thisMode = GetTagValue<string>(TagNames.Mode);
      var thisCall = GetTagValue<string>(TagNames.Call);
      var thisBand = GetTagValue<string>(TagNames.Band);

      if (string.IsNullOrEmpty(thisOp) || string.IsNullOrEmpty(thisCall) || string.IsNullOrEmpty(thisBand) ||
        string.IsNullOrEmpty(thisMode) || !thisDateTimeOn.HasValue)
        throw new Exception("Not enough info to determine if QSO matches.");

      if (!string.Equals(thisCall, op, StringComparison.OrdinalIgnoreCase))
        return false;

      if (!string.Equals(call, thisOp, StringComparison.OrdinalIgnoreCase))
        return false;

      if (!string.Equals(band, thisBand, StringComparison.OrdinalIgnoreCase))
        return false;

      if (!string.Equals(mode, thisMode, StringComparison.OrdinalIgnoreCase))
        return false;

      var qsoDateTimeDiff = thisDateTimeOn.Value - dateTimeOn.Value;

      if (qsoDateTimeDiff.TotalMilliseconds > maxQsoDateTimeDiff.TotalMilliseconds)
        return false;

      return true;
    }

    /// <summary>
    /// Determines whether or not the current QSO matches the specified QSO from the contacted station.
    /// </summary>
    /// <param name="qso">QSO from the contacted station.</param>
    public bool IsMatchReverse(ADIFQSO qso)
    {
      return IsMatchReverse(qso, TimeSpan.FromMinutes(30));
    }

    /// <summary>
    /// Determines whether or not the specified QSO matches the current QSO.
    /// </summary>
    /// <param name="qso">QSO to match against the current QSO.</param>
    public bool IsMatch(ADIFQSO qso)
    {
      return IsMatch(qso, TimeSpan.FromMinutes(30));
    }

    /// <summary>
    /// Adds a <see cref="NameTag"/> to the current QSO.
    /// </summary>
    /// <param name="name">Personal name of the contacted station.</param>
    public void AddName(string name)
    {
      if (string.IsNullOrEmpty(name))
        return;

      if (name.IsASCII())
        Add(new NameTag(name));
      else
        Add(new NameIntlTag(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
      if (string.IsNullOrEmpty(name))
        return;

      if (name.IsASCII())
        AddOrReplace(new NameTag(name));
      else
        AddOrReplace(new NameIntlTag(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newCreditsGranted"></param>
    public void MergeCreditGranted(CreditList newCreditsGranted)
    {
      if (newCreditsGranted == null || newCreditsGranted.Count < 1)
        return;

      if (!(GetTag(TagNames.CreditGranted) is CreditListTag creditGranted))
        creditGranted = new CreditListTag();

      var existingList = creditGranted.GetCreditList();

      foreach (var credit in newCreditsGranted)
      {
        if (!existingList.Contains(credit))
          existingList.Add(credit.Credit, credit.Medium);
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
        throw new CreditListException($"Invalid credit string: '{newCreditsGranted}'", newCreditsGranted);

      MergeCreditGranted(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="awards"></param>
    public void MergeAwardGranted(IEnumerable<string> awards)
    {
      if (awards == null)
        return;

      if (!(GetTag(TagNames.AwardGranted) is SponsoredAwardListTag awardsGranted))
        awardsGranted = new SponsoredAwardListTag();

      var existingList = awardsGranted.GetValues()?.ToList();

      foreach (var award in awards)
      {
        if (!existingList.Contains(award))
          existingList.Add(award);
      }

      AddOrReplace(new AwardGrantedTag(existingList.ToArray()));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="awards"></param>
    public void MergeAwardGranted(string awards)
    {
      if (string.IsNullOrWhiteSpace(awards))
        return;

      var awardsArr = ADIFSponsoredAwardList.Parse(awards);

      MergeAwardGranted(awardsArr);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetClubLogDoNotUpload()
    {
      AddOrReplace(new ClubLogQSOUploadStatusTag(Values.ADIF_BOOLEAN_FALSE));
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetHRDDoNotUpload()
    {
      AddOrReplace(new HRDLogQSOUploadStatusTag(Values.ADIF_BOOLEAN_FALSE));
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetQRZDoNotUpload()
    {
      AddOrReplace(new QRZQSOUploadStatusTag(Values.ADIF_BOOLEAN_FALSE));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="satelliteName"></param>
    /// <param name="satelliteMode"></param>
    public void SetSatellite(string satelliteName, string satelliteMode)
    {
      if (!string.IsNullOrEmpty(satelliteName))
        AddOrReplace(new SatNameTag(satelliteName));

      if (!string.IsNullOrEmpty(satelliteMode))
        AddOrReplace(new SatModeTag(satelliteMode));

      AddOrReplace(new PropModeTag("SAT"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uploadTime"></param>
    /// <param name="status"></param>
    public void SetClubLogUploaded(DateTime uploadTime, string status)
    {
      if (uploadTime != DateTime.MinValue)
        AddOrReplace(new ClubLogQSOUploadDateTag(uploadTime));

      if (!string.IsNullOrEmpty(status) && Values.QSOUploadStatuses.IsValid(status))
        AddOrReplace(new ClubLogQSOUploadStatusTag(status));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uploadTime"></param>
    /// <param name="status"></param>
    public void SetHRDUploaded(DateTime uploadTime, string status)
    {
      if (uploadTime != DateTime.MinValue)
        AddOrReplace(new HRDLogQSOUploadDateTag(uploadTime));

      if (!string.IsNullOrEmpty(status) && Values.QSOUploadStatuses.IsValid(status))
        AddOrReplace(new HRDLogQSOUploadStatusTag(status));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uploadTime"></param>
    /// <param name="status"></param>
    public void SetQRZUploaded(DateTime uploadTime, string status)
    {
      if (uploadTime != DateTime.MinValue)
        AddOrReplace(new QRZQSOUploadDateTag(uploadTime));

      if (!string.IsNullOrEmpty(status) && Values.QSOUploadStatuses.IsValid(status))
        AddOrReplace(new QRZQSOUploadStatusTag(status));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sfi"></param>
    /// <param name="aIndex"></param>
    /// <param name="kIndex"></param>
    public void AddBandConditions(int sfi, int aIndex, int kIndex)
    {
      if (sfi >= 0)
        Add(new SFITag(sfi));

      if (aIndex >= 0)
        Add(new AIndexTag(aIndex));

      if (kIndex >= 0)
        Add(new KIndexTag(kIndex));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sfi"></param>
    /// <param name="aIndex"></param>
    /// <param name="kIndex"></param>
    public void SetBandConditions(int sfi, int aIndex, int kIndex)
    {
      if (sfi >= 0)
        AddOrReplace(new SFITag(sfi));

      if (aIndex >= 0)
        AddOrReplace(new AIndexTag(aIndex));

      if (kIndex >= 0)
        AddOrReplace(new KIndexTag(kIndex));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="showerName"></param>
    /// <param name="bursts"></param>
    /// <param name="pings"></param>
    public void SetMeteorScatter(string showerName, int bursts, int pings, int maxBursts)
    {
      if (!string.IsNullOrEmpty(showerName))
        AddOrReplace(new MsShowerTag(showerName));

      if (bursts >= 0)
        AddOrReplace(new NrBurstsTag(bursts));

      if (pings >= 0)
        AddOrReplace(new NrPingsTag(pings));

      if (maxBursts >= 0)
        AddOrReplace(new MaxBurstsTag(maxBursts));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rig"></param>
    /// <param name="watts"></param>
    public void SetEquipment(string rig, int watts)
    {
      if (!string.IsNullOrEmpty(rig))
      {
        if (rig.IsASCII())
          AddOrReplace(new RigTag(rig));
        else
          AddOrReplace(new RigIntlTag(rig));
      }

      if (watts > 0)
        AddOrReplace(new RxPwrTag(watts));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rig"></param>
    /// <param name="antenna"></param>
    /// <param name="watts"></param>
    public void SetMyEquipment(string rig, string antenna, int watts)
    {
      if (!string.IsNullOrEmpty(rig))
      {
        if (rig.IsASCII())
          AddOrReplace(new MyRigTag(rig));
        else
          AddOrReplace(new MyRigIntlTag(rig));
      }

      if (!string.IsNullOrEmpty(antenna))
      {
        if (antenna.IsASCII())
          AddOrReplace(new MyAntennaTag(antenna));
        else
          AddOrReplace(new MyAntennaIntlTag(antenna));
      }

      if (watts > 0)
        AddOrReplace(new TxPwrTag(watts));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sig"></param>
    /// <param name="sigInfo"></param>
    public void SetSIG(string sig, string sigInfo)
    {
      if (!string.IsNullOrEmpty(sig))
      {
        if (sig.IsASCII())
          AddOrReplace(new SigTag(sig));
        else
          AddOrReplace(new SigIntlTag(sig));
      }

      if (!string.IsNullOrEmpty(sigInfo))
      {
        if (sigInfo.IsASCII())
          AddOrReplace(new SigInfoTag(sigInfo));
        else
          AddOrReplace(new SigInfoIntlTag(sigInfo));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sotaRef"></param>
    /// <param name="mySotaRef"></param>
    public void SetSummitToSummit(string sotaRef, string mySotaRef)
    {
      if (!string.IsNullOrEmpty(sotaRef))
        AddOrReplace(new SOTARefTag(sotaRef));

      if (!string.IsNullOrEmpty(mySotaRef))
        AddOrReplace(new MySOTARefTag(mySotaRef));
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetSIG()
    {
      return CoalesceTagValues<string>(TagNames.SigIntl, TagNames.Sig);
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetSIGInfo()
    {
      return CoalesceTagValues<string>(TagNames.SigInfoIntl, TagNames.SigInfo);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mySig"></param>
    /// <param name="mySigInfo"></param>
    public void SetMySIG(string mySig, string mySigInfo)
    {
      if (!string.IsNullOrEmpty(mySig))
      {
        if (mySig.IsASCII())
          AddOrReplace(new MySigTag(mySig));
        else
          AddOrReplace(new MySigIntlTag(mySig));
      }

      if (!string.IsNullOrEmpty(mySigInfo))
      {
        if (mySigInfo.IsASCII())
          AddOrReplace(new MySigInfoTag(mySigInfo));
        else
          AddOrReplace(new MySigInfoIntlTag(mySigInfo));
      }
    }

    /// <summary>
    /// Retrieves the date and time that the QSO started.
    /// </summary>
    public DateTime? GetQSODateTimeOn()
    {
      var qsoDateTag = GetTag(TagNames.QSODate);
      var timeOnTag = GetTag(TagNames.TimeOn);

      DateTime? qsoDate = null;
      DateTime? timeOn = null;

      if (qsoDateTag != null)
        qsoDate = qsoDateTag.Value as DateTime?;

      if (timeOnTag != null)
        timeOn = timeOnTag.Value as DateTime?;

      if (qsoDate.HasValue && timeOn.HasValue)
        return new DateTime(qsoDate.Value.Year,
                            qsoDate.Value.Month,
                            qsoDate.Value.Day,
                            timeOn.Value.Hour,
                            timeOn.Value.Minute,
                            timeOn.Value.Second,
                            DateTimeKind.Utc);
      else if (qsoDate.HasValue)
        return new DateTime(qsoDate.Value.Year,
                            qsoDate.Value.Month,
                            qsoDate.Value.Day,
                            0,
                            0,
                            0,
                            DateTimeKind.Utc);

      return null;
    }

    /// <summary>
    /// Retrieves the date and time that the QSO ended.
    /// </summary>
    public DateTime? GetQSODateTimeOff()
    {
      var qsoDateOffTag = GetTag(TagNames.QSODateOff);
      var timeOffTag = GetTag(TagNames.TimeOff);

      DateTime? qsoDateOff = null;
      DateTime? timeOff = null;

      if (qsoDateOffTag != null)
        qsoDateOff = qsoDateOffTag.Value as DateTime?;

      if (timeOffTag != null)
      {
        timeOff = timeOffTag.Value as DateTime?;
        if (!qsoDateOff.HasValue)
        {
          qsoDateOffTag = GetTag(TagNames.QSODate);

          if (qsoDateOffTag != null)
            qsoDateOff = qsoDateOffTag.Value as DateTime?;
        }
      }

      if (qsoDateOff.HasValue && timeOff.HasValue)
        return new DateTime(qsoDateOff.Value.Year,
                            qsoDateOff.Value.Month,
                            qsoDateOff.Value.Day,
                            timeOff.Value.Hour,
                            timeOff.Value.Minute,
                            timeOff.Value.Second,
                            DateTimeKind.Utc);
      else if (qsoDateOff.HasValue)
        return new DateTime(qsoDateOff.Value.Year,
                            qsoDateOff.Value.Month,
                            qsoDateOff.Value.Day,
                            0,
                            0,
                            0,
                            DateTimeKind.Utc);

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public TimeSpan GetQSODuration()
    {
      var dateTimeOn = GetQSODateTimeOn();
      var dateTimeOff = GetQSODateTimeOff();

      if (dateTimeOn.HasValue && dateTimeOff.HasValue)
      {
        if (dateTimeOff.Value < dateTimeOn.Value)
          throw new Exception("QSO ending time is less than QSO starting time.");

        return dateTimeOff.Value - dateTimeOn.Value;
      }

      return TimeSpan.Zero;
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
    public void ValidatePrimarySubdivision()
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
    public void ValidateLatLon()
    {
      var latTag = GetTag(TagNames.Lat);
      var lonTag = GetTag(TagNames.Lon);

      TagValidationHelper.ValidateLatLong(latTag, lonTag);

      var myLatTag = GetTag(TagNames.MyLat);
      var myLonTag = GetTag(TagNames.MyLon);

      TagValidationHelper.ValidateLatLong(myLatTag, myLonTag);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ValidateSecondarySubdivision()
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

      if (Contains(tag.Name))
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

    /// <summary>
    /// Returns a string representation of the current <see cref="ADIFQSO"/>.
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ADIFQSO"/>.
    /// </summary>
    /// <param name="format">Format string.</param>
    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a string representation of the current <see cref="ADIFQSO"/>.
    /// </summary>
    /// <param name="format">Format string.</param>
    /// <param name="provider">Culture-specific format provider.</param>
    public string ToString(string format, IFormatProvider provider)
    {
      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format)
      {
        case "G":
        case "C":
          return $"Tag/Field Count: {Count}";

        case "A":
        case "a":
          var val = string.Empty;
          foreach (var tag in this)
          {
            if (!(tag is EndRecordTag))
              val += $"{tag.ToString(format, provider)}";      
          }

          val += new EndRecordTag().ToString(format, provider);

          return val;

        default:
          throw new FormatException($"Format string '{format}' is not valid.");
      }
    }
  }
}
