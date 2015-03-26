namespace WcfService.Models.DTO.ToDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ReqRhyme
    {
        [DataMember]
        public string UserKey{ get; set;}

        [DataMember]
        public int RhymeId{ get; set; }

        [DataMember]
        public string CurValue { get; set; }

        [DataMember]
        public bool IsNext { get; set; }
    }
}