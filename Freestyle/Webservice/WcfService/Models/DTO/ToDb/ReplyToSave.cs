namespace WcfService.Models.DTO.ToDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ReplyToSave
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string UserKey { get; set; }

        [DataMember]
        public int RhymeId { get; set; }

        //to check that user reply to correct sentence 
        //between get and send data by reader (user) last sentence can change
        [DataMember]
        public int LastSentId { get; set; }
    }
}