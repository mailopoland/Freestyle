namespace WcfService.Models.DTO.FromDb.Rhymes.Helpers
{
    using System.Runtime.Serialization;
    using WcfService.MainHelpers;
    using WcfService.Models.DTO.FromDb.Users;

    [DataContract]
    public class SentenceToShow
    {
        public SentenceToShow()
        {
            User = new UserBaseData();
        }

        [DataMember]
        public string AuthorTxt {
            get { return authorTxt;  }
            set { authorTxt = TextChecker.CheckText(value); } 
        }

        [DataMember(EmitDefaultValue = false)]
        public string ReplyTxt {
            get { return replyTxt;  }
            set { replyTxt = TextChecker.CheckText(value); }
        }

        [DataMember(EmitDefaultValue = false)]
        public UserBaseData User { get; set; }

        private string authorTxt;
        private string replyTxt;
    }
}