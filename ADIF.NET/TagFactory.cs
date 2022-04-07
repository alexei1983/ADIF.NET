using System;
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

      { ADIFTags.Address, typeof(AddressTag) },
      { ADIFTags.AddressIntl, typeof(AddressIntlTag) },
      { ADIFTags.AIndex, typeof(AIndexTag) },
      { ADIFTags.AntEl, typeof(AntElTag) },
      { ADIFTags.AntPath, typeof(AntPathTag) },
      { ADIFTags.ADIFVer, typeof(ADIFVersionTag) },
      { ADIFTags.Age, typeof(AgeTag) },
      { ADIFTags.AntAz, typeof(AntAzTag) },
      { ADIFTags.ARRLSect, typeof(ARRLSectTag) },
      { ADIFTags.AwardGranted, typeof(AwardGrantedTag) },
      { ADIFTags.AwardSubmitted, typeof(AwardSubmittedTag) },
      { ADIFTags.Band, typeof(BandTag) },
      { ADIFTags.BandRx, typeof(BandRxTag) },
      { ADIFTags.Call, typeof(CallTag) },
      { ADIFTags.Check, typeof(CheckTag) },
      { ADIFTags.Class, typeof(ClassTag) },
      { ADIFTags.ClubLogQSOUploadDate, typeof(ClubLogQSOUploadDateTag) },
      { ADIFTags.ClubLogQSOUploadStatus, typeof(ClubLogQSOUploadStatusTag) },
      { ADIFTags.Cnty, typeof(CntyTag) },
      { ADIFTags.Comment, typeof(CommentTag) },
      { ADIFTags.CommentIntl, typeof(CommentIntlTag) },
      { ADIFTags.Continent, typeof(ContinentTag) },
      { ADIFTags.ContactedOp, typeof(ContactedOpTag) },
      { ADIFTags.ContestId, typeof(ContestIdTag) },
      { ADIFTags.Country, typeof(CountryTag) },
      { ADIFTags.CountryIntl, typeof(CountryIntlTag) },
      { ADIFTags.CQZ, typeof(CQZTag) },
      { ADIFTags.CreatedTimestamp, typeof(CreatedTimestampTag) },
      { ADIFTags.CreditGranted, typeof(CreditGrantedTag) },
      { ADIFTags.CreditSubmitted, typeof(CreditSubmittedTag) },
      { ADIFTags.DARCDOK, typeof(DARCDOKTag) },
      { ADIFTags.Distance, typeof(DistanceTag) },
      { ADIFTags.DXCC, typeof(DXCCTag) },
      { ADIFTags.Email, typeof(EmailTag) },
      { ADIFTags.EndHeader, typeof(EndHeaderTag) },
      { ADIFTags.EndRecord, typeof(EndRecordTag) },
      { ADIFTags.EqCall, typeof(EqCallTag) },
      { ADIFTags.EQSLReceivedDate, typeof(EQSLReceivedDateTag) },
      { ADIFTags.EQSLSentDate, typeof(EQSLSentDateTag) },
      { ADIFTags.EQSLReceivedStatus, typeof(EQSLReceivedStatusTag) },
      { ADIFTags.EQSLSentStatus, typeof(EQSLSentStatusTag) },
      { ADIFTags.Fists, typeof(FISTSTag) },
      { ADIFTags.FistsCc, typeof(FISTSCCTag) },
      { ADIFTags.ForceInit, typeof(ForceInitTag) },
      { ADIFTags.Freq, typeof(FreqTag) },
      { ADIFTags.FreqRx, typeof(FreqRxTag) },
      { ADIFTags.GridSquare, typeof(GridSquareTag) },
      { ADIFTags.GuestOp, typeof(GuestOpTag) },
      { ADIFTags.HrdLogQSOUploadDate, typeof(HRDLogQSOUploadDateTag) },
      { ADIFTags.HrdLogQSOUploadStatus, typeof(HRDLogQSOUploadStatusTag) },
      { ADIFTags.IOTA, typeof(IOTATag) },
      { ADIFTags.IOTAIslandId, typeof(IOTAIslandIdTag) },
      { ADIFTags.ITUZ, typeof(ITUZTag) },
      { ADIFTags.KIndex, typeof(KIndexTag) },
      { ADIFTags.Lat, typeof(LatTag) },
      { ADIFTags.Lon, typeof(LonTag) },
      { ADIFTags.LOTWQSLReceivedDate, typeof(LOTWQSLReceivedDateTag) },
      { ADIFTags.LOTWQSLRcvdStatus, typeof(LOTWQSLRcvdTag) },
      { ADIFTags.LOTWQSLSentDate, typeof(LOTWQSLSentDateTag) },
      { ADIFTags.LOTWQSLSentStatus, typeof(LOTWQSLSentTag) },
      { ADIFTags.MaxBursts, typeof(MaxBurstsTag) },
      { ADIFTags.Mode, typeof(ModeTag) },
      { ADIFTags.MsShower, typeof(MsShowerTag) },
      { ADIFTags.MyAntenna, typeof(MyAntennaTag) },
      { ADIFTags.MyAntennaIntl, typeof(MyAntennaIntlTag) },
      { ADIFTags.MyCity, typeof(MyCityTag) },
      { ADIFTags.MyCityIntl, typeof(MyCityIntlTag) },
      { ADIFTags.MyCountry, typeof(MyCountryTag) },
      { ADIFTags.MyCountryIntl, typeof(MyCountryIntlTag) },
      { ADIFTags.MyCnty, typeof(MyCntyTag) },
      { ADIFTags.MyCQZone, typeof(MyCQZoneTag) },
      { ADIFTags.MyDXCC, typeof(MyDXCCTag) },
      { ADIFTags.MyFists, typeof(MyFISTSTag) },
      { ADIFTags.MyGridSquare, typeof(MyGridSquareTag) },
      { ADIFTags.MyLat, typeof(MyLatTag) },
      { ADIFTags.MyLon, typeof(MyLonTag) },
      { ADIFTags.MyIOTA, typeof(MyIOTATag) },
      { ADIFTags.MyIOTAIslandId, typeof(MyIOTAIslandIdTag) },
      { ADIFTags.MyITUZone, typeof(MyITUZoneTag) },
      { ADIFTags.MyName, typeof(MyNameTag) },
      { ADIFTags.MyNameIntl, typeof(MyNameIntlTag) },
      { ADIFTags.MyPostalCode, typeof(MyPostalCodeTag) },
      { ADIFTags.MyPostalCodeIntl, typeof(MyPostalCodeIntlTag) },
      { ADIFTags.MyRig, typeof(MyRigTag) },
      { ADIFTags.MyRigIntl, typeof(MyRigIntlTag) },
      { ADIFTags.MySig, typeof(MySigTag) },
      { ADIFTags.MySigIntl, typeof(MySigIntlTag) },
      { ADIFTags.MySigInfo, typeof(MySigInfoTag) },
      { ADIFTags.MySigInfoIntl, typeof(MySigInfoIntlTag) },
      { ADIFTags.MySOTARef, typeof(MySOTARefTag) },
      { ADIFTags.MyState, typeof(MyStateTag) },
      { ADIFTags.MyStreet, typeof(MyStreetTag) },
      { ADIFTags.MyStreetIntl, typeof(MyStreetIntlTag) },
      { ADIFTags.MyUSACACounties, typeof(MyUSACACountiesTag) },
      { ADIFTags.MyVUCCGrids, typeof(MyVUCCGridsTag) },
      { ADIFTags.Name, typeof(NameTag) },
      { ADIFTags.NameIntl, typeof(NameIntlTag) },
      { ADIFTags.Notes, typeof(NotesTag) },
      { ADIFTags.NotesIntl, typeof(NotesIntlTag) },
      { ADIFTags.NrBursts, typeof(NrBurstsTag) },
      { ADIFTags.NrPings, typeof(NrPingsTag) },
      { ADIFTags.Operator, typeof(OperatorTag) },
      { ADIFTags.OwnerCallSign, typeof(OwnerCallSignTag) },
      { ADIFTags.Pfx, typeof(PfxTag) },
      { ADIFTags.Precedence, typeof(PrecedenceTag) },
      { ADIFTags.ProgramId, typeof(ProgramIdTag) },
      { ADIFTags.ProgramVersion, typeof(ProgramVersionTag) },
      { ADIFTags.PropMode, typeof(PropModeTag) },
      { ADIFTags.PublicKey, typeof(PublicKeyTag) },
      { ADIFTags.QRZQSOUploadDate, typeof(QRZQSOUploadDateTag) },
      { ADIFTags.QRZQSOUploadStatus, typeof(QRZQSOUploadStatusTag) },
      { ADIFTags.QSLVia, typeof(QSLViaTag) },
      { ADIFTags.QSLSent, typeof(QSLSentTag) },
      { ADIFTags.QSLSentVia, typeof(QSLSentViaTag) },
      { ADIFTags.QSLRcvd, typeof(QSLRcvdTag) },
      { ADIFTags.QSLRcvdVia, typeof(QSLRcvdViaTag) },
      { ADIFTags.QSOComplete, typeof(QSOCompleteTag) },
      { ADIFTags.QSODate, typeof(QSODateTag) },
      { ADIFTags.QSODateOff, typeof(QSODateOffTag) },
      { ADIFTags.QSLRcvdDate, typeof(QSLRvcdDateTag) },
      { ADIFTags.QSLSentDate, typeof(QSLSentDateTag) },
      { ADIFTags.QSLMsg, typeof(QSLMsgTag) },
      { ADIFTags.QSLMsgIntl, typeof(QSLMsgIntlTag) },
      { ADIFTags.QSORandom, typeof(QSORandomTag) },
      { ADIFTags.QTH, typeof(QTHTag) },
      { ADIFTags.QTHIntl, typeof(QTHIntlTag) },
      { ADIFTags.Region, typeof(RegionTag) },
      { ADIFTags.Rig, typeof(RigTag) },
      { ADIFTags.RigIntl, typeof(RigIntlTag) },
      { ADIFTags.RstRcvd, typeof(RstRcvdTag) },
      { ADIFTags.RstSent, typeof(RstSentTag) },
      { ADIFTags.RxPwr, typeof(RxPwrTag) },
      { ADIFTags.SatMode, typeof(SatModeTag) },
      { ADIFTags.SatName, typeof(SatNameTag) },
      { ADIFTags.Sfi, typeof(SFITag) },
      { ADIFTags.Sig, typeof(SigTag) },
      { ADIFTags.SigIntl, typeof(SigIntlTag) },
      { ADIFTags.SigInfo, typeof(SigInfoTag) },
      { ADIFTags.SigInfoIntl, typeof(SigInfoIntlTag) },
      { ADIFTags.SilentKey, typeof(SilentKeyTag) },
      { ADIFTags.SKCC, typeof(SKCCTag) },
      { ADIFTags.SOTARef, typeof(SOTARefTag) },
      { ADIFTags.Srx, typeof(SrxTag) },
      { ADIFTags.SrxString, typeof(SrxStringTag) },
      { ADIFTags.State, typeof(StateTag) },
      { ADIFTags.StationCallSign, typeof(StationCallSignTag) },
      { ADIFTags.Stx, typeof(StxTag) },
      { ADIFTags.StxString, typeof(StxStringTag) },
      { ADIFTags.Submode, typeof(SubmodeTag) },
      { ADIFTags.SWL, typeof(SWLTag) },
      { ADIFTags.TenTen, typeof(TenTenTag) },
      { ADIFTags.TimeOff, typeof(TimeOffTag) },
      { ADIFTags.TimeOn, typeof(TimeOnTag) },
      { ADIFTags.TxPwr, typeof(TxPwrTag) },
      { ADIFTags.UKSMG, typeof(UKSMGTag) },
      { ADIFTags.UserDef, typeof(UserDefTag) },
      { ADIFTags.USACACounties, typeof(USACACountiesTag) },
      { ADIFTags.VUCCGrids, typeof(VUCCGridsTag) },
      { ADIFTags.VEProv, typeof(VEProvTag) },
      { ADIFTags.Web, typeof(WebTag) }};

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public static ITag TagFromName(string tagName)
    {
      tagName = (tagName ?? string.Empty).ToUpper();

      var keyValuePair = TagMap.FirstOrDefault(kv => kv.Key.Equals(tagName));

      if (keyValuePair.Value != null)
        return Activator.CreateInstance(keyValuePair.Value) as ITag;

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="value"></param>
    public static ITag TagFromName(string tagName, object value)
    {
      ITag tag = TagFromName(tagName);

      if (tag != null)
        tag.SetValue(value);

      return tag;
    }
  }
}
