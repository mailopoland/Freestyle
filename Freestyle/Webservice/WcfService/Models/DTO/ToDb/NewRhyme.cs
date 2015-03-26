namespace WcfService.Models.DTO.ToDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class NewRhyme
    {
        [DataMember]
        public string UserKey{ get; set; }

        [DataMember]
        public string Title{ get; set; }

        [DataMember]
        public string Text{ get; set; }
    }
}