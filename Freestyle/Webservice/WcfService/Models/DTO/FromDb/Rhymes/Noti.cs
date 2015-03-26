namespace WcfService.Models.DTO.FromDb.Rhymes
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Noti
    {
        //it takes null when checking new resp is turned off
        [DataMember]
        public bool? IsNewResp { get; set; }
        //it takes null when checking new accept is turned off
        [DataMember]
        public bool? IsNewAccept { get; set; }
    }
}