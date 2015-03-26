namespace WcfService.Models.DTO.FromDb.Rhymes.Helpers
{
    using System.Runtime.Serialization;
    using WcfService.MainHelpers;
    using WcfService.Models.DTO.FromDb.Users;

    [DataContract]
    public class ReplyToAccepted
    {
        [DataMember]
        public int ReplyId { get; set; }

        [DataMember]
        public string ReplyTxt {
            get { return replyTxt; }
            set { replyTxt = TextChecker.CheckText(value); }
        }

        [DataMember]
        public UserBaseData User { get; set; }

        private string replyTxt;
    }
}