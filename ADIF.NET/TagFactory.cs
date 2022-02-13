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
      { TagNames.CreditGranted, typeof(CreditGrantedTag) },
      { TagNames.CreditSubmitted, typeof(CreditSubmittedTag) },
      { TagNames.Distance, typeof(DistanceTag) },
      { TagNames.DXCC, typeof(DXCCTag) },
      { TagNames.Email, typeof(EmailTag) },
      { TagNames.EndHeader, typeof(EndHeaderTag) },
      { TagNames.EndRecord, typeof(EndRecordTag) },
      { TagNames.EqCall, typeof(EqCallTag) },
      { TagNames.EQSLReceivedDate, typeof(EQSLReceivedDateTag) },
      { TagNames.EQSLSentDate, typeof(EQSLSentDateTag) },
      { TagNames.EQSLReceivedStatus, typeof(EQSLReceivedStatusTag) },
      { TagNames.EQSLSentStatus, typeof(EQSLSentStatusTag) },
      { TagNames.Fists, typeof(FISTSTag) },
      { TagNames.FistsCc, typeof(FISTSCCTag) },
      { TagNames.ForceInit, typeof(ForceInitTag) },
      { TagNames.Freq, typeof(FreqTag) },
      { TagNames.FreqRx, typeof(FreqRxTag) },
      { TagNames.GridSquare, typeof(GridSquareTag) },
      { TagNames.GuestOp, typeof(GuestOpTag) },
      { TagNames.HrdLogQSOUploadDate, typeof(HRDLogQSOUploadDateTag) },
      { TagNames.HrdLogQSOUploadStatus, typeof(HRDLogQSOUploadStatusTag) },
      { TagNames.IOTA, typeof(IOTATag) },
      { TagNames.IOTAIslandId, typeof(IOTAIslandIdTag) },
      { TagNames.ITUZ, typeof(ITUZTag) },
      { TagNames.KIndex, typeof(KIndexTag) },
      { TagNames.Lat, typeof(LatTag) },
      { TagNames.Lon, typeof(LonTag) },
      { TagNames.LOTWQSLReceivedDate, typeof(LotwQSLReceivedDateTag) },
      { TagNames.LOTWQSLRcvdStatus, typeof(LOTWQSLRcvdTag) },
      { TagNames.LOTWQSLSentDate, typeof(LotwQSLSentDateTag) },
      { TagNames.LOTWQSLSentStatus, typeof(LOTWQSLSentTag) },
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
      { TagNames.MyCQZone, typeof(MyCQZoneTag) },
      { TagNames.MyDXCC, typeof(MyDXCCTag) },
      { TagNames.MyFists, typeof(MyFISTSTag) },
      { TagNames.MyGridSquare, typeof(MyGridSquareTag) },
      { TagNames.MyLat, typeof(MyLatTag) },
      { TagNames.MyLon, typeof(MyLonTag) },
      { TagNames.MyIOTA, typeof(MyIOTATag) },
      { TagNames.MyIOTAIslandId, typeof(MyIOTAIslandIdTag) },
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
      { TagNames.MyState, typeof(MyStateTag) },
      { TagNames.MyUSACACounties, typeof(MyUSACACountiesTag) },
      { TagNames.MyVUCCGrids, typeof(MyVUCCGridsTag) },
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
      { TagNames.QRZQSOUploadDate, typeof(QRZQSOUploadDateTag) },
      { TagNames.QRZQSOUploadStatus, typeof(QRZQSOUploadStatusTag) },
      { TagNames.QSLVia, typeof(QSLViaTag) },
      { TagNames.QSLSent, typeof(QSLSentTag) },
      { TagNames.QSLRcvd, typeof(QSLRcvdTag) },
      { TagNames.QSLRcvdVia, typeof(QSLRcvdViaTag) },
      { TagNames.QSOComplete, typeof(QSOCompleteTag) },
      { TagNames.QSODate, typeof(QSODateTag) },
      { TagNames.QSODateOff, typeof(QSODateOffTag) },
      { TagNames.QSLRcvdDate, typeof(QSLRvcdDateTag) },
      { TagNames.QSLSentDate, typeof(QSLSentDateTag) },
      { TagNames.QSLMsg, typeof(QSLMsgTag) },
      { TagNames.QSLMsgIntl, typeof(QSLMsgIntlTag) },
      { TagNames.QSORandom, typeof(QSORandomTag) },
      { TagNames.QTH, typeof(QTHTag) },
      { TagNames.QTHIntl, typeof(QTHIntlTag) },
      { TagNames.Rig, typeof(RigTag) },
      { TagNames.RigIntl, typeof(RigIntlTag) },
      { TagNames.RstRcvd, typeof(RstRcvdTag) },
      { TagNames.RstSent, typeof(RstSentTag) },
      { TagNames.RxPwr, typeof(RxPwrTag) },
      { TagNames.SatMode, typeof(SatModeTag) },
      { TagNames.SatName, typeof(SatNameTag) },
      { TagNames.Sfi, typeof(SFITag) },
      { TagNames.Sig, typeof(SigTag) },
      { TagNames.SigIntl, typeof(SigIntlTag) },
      { TagNames.SigInfo, typeof(SigInfoTag) },
      { TagNames.SigInfoIntl, typeof(SigInfoIntlTag) },
      { TagNames.SilentKey, typeof(SilentKeyTag) },
      { TagNames.SKCC, typeof(SKCCTag) },
      { TagNames.SOTARef, typeof(SOTARefTag) },
      { TagNames.Srx, typeof(SrxTag) },
      { TagNames.SrxString, typeof(SrxStringTag) },
      { TagNames.State, typeof(StateTag) },
      { TagNames.StationCallSign, typeof(StationCallSignTag) },
      { TagNames.Stx, typeof(StxTag) },
      { TagNames.StxString, typeof(StxStringTag) },
      { TagNames.Submode, typeof(SubmodeTag) },
      { TagNames.SWL, typeof(SWLTag) },
      { TagNames.TenTen, typeof(TenTenTag) },
      { TagNames.TimeOff, typeof(TimeOffTag) },
      { TagNames.TimeOn, typeof(TimeOnTag) },
      { TagNames.TxPwr, typeof(TxPwrTag) },
      { TagNames.UKSMG, typeof(UKSMGTag) },
      { TagNames.UserDef, typeof(UserDefTag) },
      { TagNames.VUCCGrids, typeof(VUCCGridsTag) },
      { TagNames.VEProv, typeof(VEProvTag) },
      { TagNames.Web, typeof(WebTag) }};

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
