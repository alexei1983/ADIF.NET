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
      { TagNames.AdifVer, typeof(AdifVersionTag) },
      { TagNames.Age, typeof(AgeTag) },
      { TagNames.AntAz, typeof(AntAzTag) },
      { TagNames.ArrlSect, typeof(ArrlSectTag) },
      { TagNames.Band, typeof(BandTag) },
      { TagNames.BandRx, typeof(BandRxTag) },
      { TagNames.Call, typeof(CallTag) },
      { TagNames.Check, typeof(CheckTag) },
      { TagNames.Class, typeof(ClassTag) },
      { TagNames.ClubLogQsoUploadDate, typeof(ClubLogQsoUploadDateTag) },
      { TagNames.ClubLogQsoUploadStatus, typeof(ClubLogQsoUploadStatusTag) },
      { TagNames.Cnty, typeof(CntyTag) },
      { TagNames.Comment, typeof(CommentTag) },
      { TagNames.CommentIntl, typeof(CommentIntlTag) },
      { TagNames.Continent, typeof(ContinentTag) },
      { TagNames.ContactedOp, typeof(ContactedOpTag) },
      { TagNames.ContestId, typeof(ContestIdTag) },
      { TagNames.Country, typeof(CountryTag) },
      { TagNames.CountryIntl, typeof(CountryIntlTag) },
      { TagNames.Cqz, typeof(CqzTag) },
      { TagNames.CreatedTimestamp, typeof(CreatedTimestampTag) },
      { TagNames.Distance, typeof(DistanceTag) },
      { TagNames.Dxcc, typeof(DxccTag) },
      { TagNames.Email, typeof(EmailTag) },
      { TagNames.EndHeader, typeof(EndHeaderTag) },
      { TagNames.EndRecord, typeof(EndRecordTag) },
      { TagNames.EqCall, typeof(EqCallTag) },
      { TagNames.EQslReceivedDate, typeof(EQslReceivedDateTag) },
      { TagNames.EQslSentDate, typeof(EQslSentDateTag) },
      { TagNames.EQslReceivedStatus, typeof(EQslReceivedStatusTag) },
      { TagNames.EQslSentStatus, typeof(EQslSentStatusTag) },
      { TagNames.Fists, typeof(FistsTag) },
      { TagNames.FistsCc, typeof(FistsCcTag) },
      { TagNames.Freq, typeof(FreqTag) },
      { TagNames.FreqRx, typeof(FreqRxTag) },
      { TagNames.GridSquare, typeof(GridSquareTag) },
      { TagNames.GuestOp, typeof(GuestOpTag) },
      { TagNames.HrdLogQsoUploadDate, typeof(HrdLogQsoUploadDateTag) },
      { TagNames.Iota, typeof(IotaTag) },
      { TagNames.IotaIslandId, typeof(IotaIslandIdTag) },
      { TagNames.Ituz, typeof(ItuzTag) },
      { TagNames.KIndex, typeof(KIndexTag) },
      { TagNames.Lat, typeof(LatTag) },
      { TagNames.Lon, typeof(LonTag) },
      { TagNames.LotwQslReceivedDate, typeof(LotwQslReceivedDateTag) },
      { TagNames.LotwQslSentDate, typeof(LotwQslSentDateTag) },
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
      { TagNames.MyCqZone, typeof(MyCqZoneTag) },
      { TagNames.MyDxcc, typeof(MyDxccTag) },
      { TagNames.MyFists, typeof(MyFistsTag) },
      { TagNames.MyGridSquare, typeof(MyGridSquareTag) },
      { TagNames.MyLat, typeof(MyLatTag) },
      { TagNames.MyLon, typeof(MyLonTag) },
      { TagNames.MyIota, typeof(MyIotaTag) },
      { TagNames.MyIotaIslandId, typeof(MyIotaIslandIdTag) },
      { TagNames.MyItuZone, typeof(MyItuZoneTag) },
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
      { TagNames.MySotaRef, typeof(MySotaRefTag) },
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
      { TagNames.QrzQsoUploadDate, typeof(QrzQsoUploadDateTag) },
      { TagNames.QsoDate, typeof(QsoDateTag) },
      { TagNames.QsoDateOff, typeof(QsoDateOffTag) },
      { TagNames.QsoReceivedDate, typeof(QsoReceivedDateTag) },
      { TagNames.QsoSentDate, typeof(QsoSentDateTag) },
      { TagNames.QsoRandom, typeof(QsoRandomTag) },
      { TagNames.Qth, typeof(QthTag) },
      { TagNames.QthIntl, typeof(QthIntlTag) },
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
      { TagNames.Skcc, typeof(SkccTag) },
      { TagNames.SotaRef, typeof(SotaRefTag) },
      { TagNames.Srx, typeof(SrxTag) },
      { TagNames.SrxString, typeof(SrxStringTag) },
      { TagNames.Stx, typeof(StxTag) },
      { TagNames.StxString, typeof(StxStringTag) },
      { TagNames.Swl, typeof(SwlTag) },
      { TagNames.TenTen, typeof(TenTenTag) },
      { TagNames.TimeOff, typeof(TimeOffTag) },
      { TagNames.TimeOn, typeof(TimeOnTag) },
      { TagNames.TxPwr, typeof(TxPwrTag) },
      { TagNames.UkSmg, typeof(UkSmgTag) },
      { TagNames.UserDef, typeof(UserDefTag) },
      { TagNames.Web, typeof(WebTag) }};

    public static ITag TagFromName(string tagName) {

      tagName = (tagName ?? string.Empty).ToUpper();

      var keyValuePair = TagMap.FirstOrDefault(kv => kv.Key.Equals(tagName));

      if (keyValuePair.Value != null)
        return Activator.CreateInstance(keyValuePair.Value) as ITag;

      return null;
      }
    }
  }
