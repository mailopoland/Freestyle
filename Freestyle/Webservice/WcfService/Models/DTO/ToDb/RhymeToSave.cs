namespace WcfService.Models.DTO.ToDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RhymeToSave
    {
        [DataMember]
        public string UserKey { get; set; }

        [DataMember]
        public int RhymeId { get; set; }

        [DataMember]
        public int ReplyId { get; set; }
    }
}