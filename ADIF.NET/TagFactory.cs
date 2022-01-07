﻿using System;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// 
  /// </summary>
  public static class TagFactory {

    /// <summary>
    /// Maps tag names to tag types.
    /// </summary>
    static readonly Dictionary<string, Type> TagMap = new Dictionary<string, Type>() {

      { TagNames.Address, typeof(AddressTag) },
      { TagNames.AddressIntl, typeof(AddressIntlTag) },
      { TagNames.AIndex, typeof(AIndexTag) },
      { TagNames.AntEl, typeof(AntElTag) },
      { TagNames.AntPath, typeof(AntPathTag) },
      { TagNames.ADIFVer, typeof(ADIFVersionTag) },
      { TagNames.Age, typeof(AgeTag) },
      { TagNames.AntAz, typeof(AntAzTag) },
      { TagNames.ARRLSect, typeof(ARRLSectTag) },
      { TagNames.Band, typeof(BandTag) },
      { TagNames.BandRx, typeof(BandRxTag) },
      { TagNames.Call, typeof(CallTag) },
      { TagNames.Check, typeof(CheckTag) },
      { TagNames.Class, typeof(ClassTag) },
      { TagNames.ClubLogQSOUploadDate, typeof(ClubLogQSOUploadDateTag) },
      { TagNames.ClubLogQSOUploadStatus, typeof(ClubLogQSOUploadStatusTag) },
      { TagNames.Cnty, typeof(CntyTag) },
      { TagNames.Comment, typeof(CommentTag) },
      { TagNames.CommentIntl, typeof(CommentIntlTag) },
      { TagNames.Continent, typeof(ContinentTag) },
      { TagNames.ContactedOp, typeof(ContactedOpTag) },
      { TagNames.ContestId, typeof(ContestIdTag) },
      { TagNames.Country, typeof(CountryTag) },
      { TagNames.CountryIntl, typeof(CountryIntlTag) },
      { TagNames.CQZ, typeof(CQZTag) },
      { TagNames.CreatedTimestamp, typeof(CreatedTimestampTag) },
      { TagNames.Distance, typeof(DistanceTag) },
      { TagNames.DXCC, typeof(DxccTag) },
      { TagNames.Email, typeof(EmailTag) },
      { TagNames.EndHeader, typeof(EndHeaderTag) },
      { TagNames.EndRecord, typeof(EndRecordTag) },
      { TagNames.EqCall, typeof(EqCallTag) },
      { TagNames.EQSLReceivedDate, typeof(EQSLReceivedDateTag) },
      { TagNames.EQSLSentDate, typeof(EQSLSentDateTag) },
      { TagNames.EQSLReceivedStatus, typeof(EQSLReceivedStatusTag) },
      { TagNames.EQSLSentStatus, typeof(EQSLSentStatusTag) },
      { TagNames.Fists, typeof(FistsTag) },
      { TagNames.FistsCc, typeof(FistsCcTag) },
      { TagNames.Freq, typeof(FreqTag) },
      { TagNames.FreqRx, typeof(FreqRxTag) },
      { TagNames.GridSquare, typeof(GridSquareTag) },
      { TagNames.GuestOp, typeof(GuestOpTag) },
      { TagNames.HrdLogQSOUploadDate, typeof(HrdLogQSOUploadDateTag) },
      { TagNames.HrdLogQSOUploadStatus, typeof(HrdLogQSOUploadStatusTag) },
      { TagNames.IOTA, typeof(IOTATag) },
      { TagNames.IOTAIslandId, typeof(IOTAIslandIdTag) },
      { TagNames.ITUZ, typeof(ITUZTag) },
      { TagNames.KIndex, typeof(KIndexTag) },
      { TagNames.Lat, typeof(LatTag) },
      { TagNames.Lon, typeof(LonTag) },
      { TagNames.LotwQSLReceivedDate, typeof(LotwQSLReceivedDateTag) },
      { TagNames.LotwQSLSentDate, typeof(LotwQSLSentDateTag) },
      { TagNames.MaxBursts, typeof(MaxBurstsTag) },
      { TagNames.Mode, typeof(ModeTag) },
      { TagNames.MsShower, typeof(MsShowerTag) },
      { TagNames.MyAntenna, typeof(MyAntennaTag) },
      { TagNames.MyAntennaIntl, typeof(MyAntennaIntlTag) },
      { TagNames.MyCity, typeof(MyCityTag) },
      { TagNames.MyCityIntl, typeof(MyCityIntlTag) },
      { TagNames.MyCountry, typeof(MyCountryTag) },
      { TagNames.MyCountryIntl, typeof(MyCountryIntlTag) },
      { TagNames.MyCnty, typeof(MyCntyTag) },
      { TagNames.MyCQZone, typeof(MyCqZoneTag) },
      { TagNames.MyDXCC, typeof(MyDxccTag) },
      { TagNames.MyFists, typeof(MyFISTSTag) },
      { TagNames.MyGridSquare, typeof(MyGridSquareTag) },
      { TagNames.MyLat, typeof(MyLatTag) },
      { TagNames.MyLon, typeof(MyLonTag) },
      { TagNames.MyIOTA, typeof(MyIotaTag) },
      { TagNames.MyIOTAIslandId, typeof(MyIotaIslandIdTag) },
      { TagNames.MyITUZone, typeof(MyITUZoneTag) },
      { TagNames.MyName, typeof(MyNameTag) },
      { TagNames.MyNameIntl, typeof(MyNameIntlTag) },
      { TagNames.MyPostalCode, typeof(MyPostalCodeTag) },
      { TagNames.MyPostalCodeIntl, typeof(MyPostalCodeIntlTag) },
      { TagNames.MyRig, typeof(MyRigTag) },
      { TagNames.MyRigIntl, typeof(MyRigIntlTag) },
      { TagNames.MySig, typeof(MySigTag) },
      { TagNames.MySigIntl, typeof(MySigIntlTag) },
      { TagNames.MySigInfo, typeof(MySigInfoTag) },
      { TagNames.MySigInfoIntl, typeof(MySigInfoIntlTag) },
      { TagNames.MySOTARef, typeof(MySOTARefTag) },
      { TagNames.Name, typeof(NameTag) },
      { TagNames.NameIntl, typeof(NameIntlTag) },
      { TagNames.NrBursts, typeof(NrBurstsTag) },
      { TagNames.NrPings, typeof(NrPingsTag) },
      { TagNames.Operator, typeof(OperatorTag) },
      { TagNames.OwnerCallSign, typeof(OwnerCallSignTag) },
      { TagNames.Pfx, typeof(PfxTag) },
      { TagNames.Precedence, typeof(PrecedenceTag) },
      { TagNames.ProgramId, typeof(ProgramIdTag) },
      { TagNames.ProgramVersion, typeof(ProgramVersionTag) },
      { TagNames.PropMode, typeof(PropModeTag) },
      { TagNames.PublicKey, typeof(PublicKeyTag) },
      { TagNames.QrzQSOUploadDate, typeof(QRZQSOUploadDateTag) },
      { TagNames.QSODate, typeof(QSODateTag) },
      { TagNames.QSODateOff, typeof(QSODateOffTag) },
      { TagNames.QSOReceivedDate, typeof(QSOReceivedDateTag) },
      { TagNames.QSOSentDate, typeof(QSOSentDateTag) },
      { TagNames.QSORandom, typeof(QSORandomTag) },
      { TagNames.QTH, typeof(QTHTag) },
      { TagNames.QTHIntl, typeof(QTHIntlTag) },
      { TagNames.Rig, typeof(RigTag) },
      { TagNames.RigIntl, typeof(RigIntlTag) },
      { TagNames.RxPwr, typeof(RxPwrTag) },
      { TagNames.SatMode, typeof(SatModeTag) },
      { TagNames.SatName, typeof(SatNameTag) },
      { TagNames.Sfi, typeof(SfiTag) },
      { TagNames.Sig, typeof(SigTag) },
      { TagNames.SigIntl, typeof(SigIntlTag) },
      { TagNames.SigInfo, typeof(SigInfoTag) },
      { TagNames.SigInfoIntl, typeof(SigInfoIntlTag) },
      { TagNames.SilentKey, typeof(SilentKeyTag) },
      { TagNames.SKCC, typeof(SKCCTag) },
      { TagNames.SOTARef, typeof(SOTARefTag) },
      { TagNames.Srx, typeof(SrxTag) },
      { TagNames.SrxString, typeof(SrxStringTag) },
      { TagNames.StationCallSign, typeof(StationCallSignTag) },
      { TagNames.Stx, typeof(StxTag) },
      { TagNames.StxString, typeof(StxStringTag) },
      { TagNames.SWL, typeof(SWLTag) },
      { TagNames.TenTen, typeof(TenTenTag) },
      { TagNames.TimeOff, typeof(TimeOffTag) },
      { TagNames.TimeOn, typeof(TimeOnTag) },
      { TagNames.TxPwr, typeof(TxPwrTag) },
      { TagNames.UKSMG, typeof(UKSMGTag) },
      { TagNames.UserDef, typeof(UserDefTag) },
      { TagNames.Web, typeof(WebTag) }};

    public static ITag TagFromName(string tagName)
    {

      tagName = (tagName ?? string.Empty).ToUpper();

      var keyValuePair = TagMap.FirstOrDefault(kv => kv.Key.Equals(tagName));

      if (keyValuePair.Value != null)
        return Activator.CreateInstance(keyValuePair.Value) as ITag;

      return null;
    }

    public static ITag TagFromName(string tagName, object value)
    {
      ITag tag = TagFromName(tagName);

      if (tag != null)
        tag.SetValue(value);

      return tag;
    }
  }
}
