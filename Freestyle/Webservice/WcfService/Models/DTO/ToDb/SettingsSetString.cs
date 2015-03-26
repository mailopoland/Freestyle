namespace WcfService.Models.DTO.ToDb
{
    using System.Runtime.Serialization;
    using WcfService.MainHelpers;

    [DataContract]
    public class SettingsSetString
    {
        [DataMember]
        public string UserKey { get; set; }

        [DataMember]
        public string Data {
            get { return data;  }
            //is censored to prevent see for example for two users the same login
            set { data = TextChecker.CheckText(value); } 
        }

        private string data;
    }
}