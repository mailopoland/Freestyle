namespace WcfService.Models.DTO.Users
{
    using System.Runtime.Serialization;

    [DataContract]
    public class LogInBaseData
    {
        [DataMember]
        public string UserKey { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool NoRespNoti { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool NoAcceptNoti { get; set; }

        [DataMember]
        public int NotiFreq { get; set; }
    }
}