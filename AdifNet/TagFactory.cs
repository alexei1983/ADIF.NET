
using org.goodspace.Data.Radio.Adif.Tags;

namespace org.goodspace.Data.Radio.Adif
{

    /// <summary>
    /// 
    /// </summary>
    public static class TagFactory
    {
        /// <summary>
        /// Maps tag names to tag types.
        /// </summary>
        static readonly Dictionary<string, Type> TagMap = new() {

      { AdifTags.Address, typeof(AddressTag) },
      { AdifTags.AddressIntl, typeof(AddressIntlTag) },
      { AdifTags.AIndex, typeof(AIndexTag) },
      { AdifTags.AntEl, typeof(AntElTag) },
      { AdifTags.AntPath, typeof(AntPathTag) },
      { AdifTags.AdifVer, typeof(AdifVersionTag) },
      { AdifTags.Age, typeof(AgeTag) },
      { AdifTags.AntAz, typeof(AntAzTag) },
      { AdifTags.ArrlSect, typeof(ARRLSectTag) },
      { AdifTags.AwardGranted, typeof(AwardGrantedTag) },
      { AdifTags.AwardSubmitted, typeof(AwardSubmittedTag) },
      { AdifTags.Band, typeof(BandTag) },
      { AdifTags.BandRx, typeof(BandRxTag) },
      { AdifTags.Call, typeof(CallTag) },
      { AdifTags.Check, typeof(CheckTag) },
      { AdifTags.Class, typeof(ClassTag) },
      { AdifTags.ClubLogQsoUploadDate, typeof(ClubLogQsoUploadDateTag) },
      { AdifTags.ClubLogQsoUploadStatus, typeof(ClubLogQsoUploadStatusTag) },
      { AdifTags.Cnty, typeof(CntyTag) },
      { AdifTags.Comment, typeof(CommentTag) },
      { AdifTags.CommentIntl, typeof(CommentIntlTag) },
      { AdifTags.Continent, typeof(ContinentTag) },
      { AdifTags.ContactedOp, typeof(ContactedOpTag) },
      { AdifTags.ContestId, typeof(ContestIdTag) },
      { AdifTags.Country, typeof(CountryTag) },
      { AdifTags.CountryIntl, typeof(CountryIntlTag) },
      { AdifTags.Cqz, typeof(CQZTag) },
      { AdifTags.CreatedTimestamp, typeof(CreatedTimestampTag) },
      { AdifTags.CreditGranted, typeof(CreditGrantedTag) },
      { AdifTags.CreditSubmitted, typeof(CreditSubmittedTag) },
      { AdifTags.DarcDok, typeof(DarcDokTag) },
      { AdifTags.Distance, typeof(DistanceTag) },
      { AdifTags.Dxcc, typeof(DXCCTag) },
      { AdifTags.Email, typeof(EmailTag) },
      { AdifTags.EndHeader, typeof(EndHeaderTag) },
      { AdifTags.EndRecord, typeof(EndRecordTag) },
      { AdifTags.EqCall, typeof(EqCallTag) },
      { AdifTags.EQslReceivedDate, typeof(EQslReceivedDateTag) },
      { AdifTags.EQslSentDate, typeof(EQslSentDateTag) },
      { AdifTags.EQslReceivedStatus, typeof(EQslReceivedStatusTag) },
      { AdifTags.EQslSentStatus, typeof(EQslSentStatusTag) },
      { AdifTags.Fists, typeof(FistsTag) },
      { AdifTags.FistsCc, typeof(FistsCcTag) },
      { AdifTags.ForceInit, typeof(ForceInitTag) },
      { AdifTags.Freq, typeof(FreqTag) },
      { AdifTags.FreqRx, typeof(FreqRxTag) },
      { AdifTags.GridSquare, typeof(GridSquareTag) },
      { AdifTags.GuestOp, typeof(GuestOpTag) },
      { AdifTags.HrdLogQsoUploadDate, typeof(HrdLogQsoUploadDateTag) },
      { AdifTags.HrdLogQsoUploadStatus, typeof(HrdLogQsoUploadStatusTag) },
      { AdifTags.Iota, typeof(IotaTag) },
      { AdifTags.IotaIslandId, typeof(IotaIslandIdTag) },
      { AdifTags.Ituz, typeof(ItuzTag) },
      { AdifTags.KIndex, typeof(KIndexTag) },
      { AdifTags.Lat, typeof(LatTag) },
      { AdifTags.Lon, typeof(LonTag) },
      { AdifTags.LotwQslReceivedDate, typeof(LotwQslReceivedDateTag) },
      { AdifTags.LotwQslRcvdStatus, typeof(LotwQslRcvdTag) },
      { AdifTags.LotwQslSentDate, typeof(LotwQslSentDateTag) },
      { AdifTags.LotwQslSentStatus, typeof(LotwQslSentTag) },
      { AdifTags.MaxBursts, typeof(MaxBurstsTag) },
      { AdifTags.Mode, typeof(ModeTag) },
      { AdifTags.MorseKeyInfo, typeof(MorseKeyInfoTag) },
      { AdifTags.MorseKeyType, typeof(MorseKeyTypeTag) },
      { AdifTags.MsShower, typeof(MsShowerTag) },
      { AdifTags.MyAntenna, typeof(MyAntennaTag) },
      { AdifTags.MyAntennaIntl, typeof(MyAntennaIntlTag) },
      { AdifTags.MyCity, typeof(MyCityTag) },
      { AdifTags.MyCityIntl, typeof(MyCityIntlTag) },
      { AdifTags.MyCountry, typeof(MyCountryTag) },
      { AdifTags.MyCountryIntl, typeof(MyCountryIntlTag) },
      { AdifTags.MyCnty, typeof(MyCntyTag) },
      { AdifTags.MyCqZone, typeof(MyCQZoneTag) },
      { AdifTags.MyDarcDok, typeof(MyDarcDokTag) },
      { AdifTags.MyDxcc, typeof(MyDXCCTag) },
      { AdifTags.MyFists, typeof(MyFISTSTag) },
      { AdifTags.MyGridSquare, typeof(MyGridSquareTag) },
      { AdifTags.MyLat, typeof(MyLatTag) },
      { AdifTags.MyLon, typeof(MyLonTag) },
      { AdifTags.MyIota, typeof(MyIotaTag) },
      { AdifTags.MyIotaIslandId, typeof(MyIotaIslandIdTag) },
      { AdifTags.MyItuZone, typeof(MyItuZoneTag) },
      { AdifTags.MyMorseKeyInfo, typeof(MyMorseKeyInfoTag) },
      { AdifTags.MyMorseKeyType, typeof(MyMorseKeyTypeTag) },
      { AdifTags.MyName, typeof(MyNameTag) },
      { AdifTags.MyNameIntl, typeof(MyNameIntlTag) },
      { AdifTags.MyPostalCode, typeof(MyPostalCodeTag) },
      { AdifTags.MyPostalCodeIntl, typeof(MyPostalCodeIntlTag) },
      { AdifTags.MyRig, typeof(MyRigTag) },
      { AdifTags.MyRigIntl, typeof(MyRigIntlTag) },
      { AdifTags.MySig, typeof(MySigTag) },
      { AdifTags.MySigIntl, typeof(MySigIntlTag) },
      { AdifTags.MySigInfo, typeof(MySigInfoTag) },
      { AdifTags.MySigInfoIntl, typeof(MySigInfoIntlTag) },
      { AdifTags.MySotaRef, typeof(MySotaRefTag) },
      { AdifTags.MyState, typeof(MyStateTag) },
      { AdifTags.MyStreet, typeof(MyStreetTag) },
      { AdifTags.MyStreetIntl, typeof(MyStreetIntlTag) },
      { AdifTags.MyUsacaCounties, typeof(MyUsacaCountiesTag) },
      { AdifTags.MyVuccGrids, typeof(MyVuccGridsTag) },
      { AdifTags.Name, typeof(NameTag) },
      { AdifTags.NameIntl, typeof(NameIntlTag) },
      { AdifTags.Notes, typeof(NotesTag) },
      { AdifTags.NotesIntl, typeof(NotesIntlTag) },
      { AdifTags.NrBursts, typeof(NrBurstsTag) },
      { AdifTags.NrPings, typeof(NrPingsTag) },
      { AdifTags.Operator, typeof(OperatorTag) },
      { AdifTags.OwnerCallSign, typeof(OwnerCallSignTag) },
      { AdifTags.Pfx, typeof(PfxTag) },
      { AdifTags.Precedence, typeof(PrecedenceTag) },
      { AdifTags.ProgramId, typeof(ProgramIdTag) },
      { AdifTags.ProgramVersion, typeof(ProgramVersionTag) },
      { AdifTags.PropMode, typeof(PropModeTag) },
      { AdifTags.PublicKey, typeof(PublicKeyTag) },
      { AdifTags.QrzQsoUploadDate, typeof(QrzQsoUploadDateTag) },
      { AdifTags.QrzQsoUploadStatus, typeof(QrzQsoUploadStatusTag) },
      { AdifTags.QslVia, typeof(QslViaTag) },
      { AdifTags.QslSent, typeof(QslSentTag) },
      { AdifTags.QslSentVia, typeof(QslSentViaTag) },
      { AdifTags.QslRcvd, typeof(QslRcvdTag) },
      { AdifTags.QslRcvdVia, typeof(QslRcvdViaTag) },
      { AdifTags.QsoComplete, typeof(QsoCompleteTag) },
      { AdifTags.QsoDate, typeof(QsoDateTag) },
      { AdifTags.QsoDateOff, typeof(QsoDateOffTag) },
      { AdifTags.QslRcvdDate, typeof(QslRvcdDateTag) },
      { AdifTags.QslSentDate, typeof(QslSentDateTag) },
      { AdifTags.QslMsg, typeof(QslMsgTag) },
      { AdifTags.QslMsgIntl, typeof(QslMsgIntlTag) },
      { AdifTags.QsoRandom, typeof(QsoRandomTag) },
      { AdifTags.Qth, typeof(QthTag) },
      { AdifTags.QthIntl, typeof(QthIntlTag) },
      { AdifTags.Region, typeof(RegionTag) },
      { AdifTags.Rig, typeof(RigTag) },
      { AdifTags.RigIntl, typeof(RigIntlTag) },
      { AdifTags.RstRcvd, typeof(RstRcvdTag) },
      { AdifTags.RstSent, typeof(RstSentTag) },
      { AdifTags.RxPwr, typeof(RxPwrTag) },
      { AdifTags.SatMode, typeof(SatModeTag) },
      { AdifTags.SatName, typeof(SatNameTag) },
      { AdifTags.Sfi, typeof(SfiTag) },
      { AdifTags.Sig, typeof(SigTag) },
      { AdifTags.SigIntl, typeof(SigIntlTag) },
      { AdifTags.SigInfo, typeof(SigInfoTag) },
      { AdifTags.SigInfoIntl, typeof(SigInfoIntlTag) },
      { AdifTags.SilentKey, typeof(SilentKeyTag) },
      { AdifTags.Skcc, typeof(SkccTag) },
      { AdifTags.SotaRef, typeof(SotaRefTag) },
      { AdifTags.Srx, typeof(SrxTag) },
      { AdifTags.SrxString, typeof(SrxStringTag) },
      { AdifTags.State, typeof(StateTag) },
      { AdifTags.StationCallSign, typeof(StationCallSignTag) },
      { AdifTags.Stx, typeof(StxTag) },
      { AdifTags.StxString, typeof(StxStringTag) },
      { AdifTags.SubMode, typeof(SubModeTag) },
      { AdifTags.Swl, typeof(SwlTag) },
      { AdifTags.TenTen, typeof(TenTenTag) },
      { AdifTags.TimeOff, typeof(TimeOffTag) },
      { AdifTags.TimeOn, typeof(TimeOnTag) },
      { AdifTags.TxPwr, typeof(TxPwrTag) },
      { AdifTags.UkSmg, typeof(UkSmgTag) },
      { AdifTags.UserDef, typeof(UserDefTag) },
      { AdifTags.UsacaCounties, typeof(UsacaCountiesTag) },
      { AdifTags.VuccGrids, typeof(VuccGridsTag) },
      { AdifTags.VeProv, typeof(VeProvTag) },
      { AdifTags.Web, typeof(WebTag) }};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        public static ITag? TagFromName(string tagName)
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
        public static ITag? TagFromName(string tagName, object value)
        {
            ITag? tag = TagFromName(tagName);
            tag?.SetValue(value);
            return tag;
        }
    }
}
