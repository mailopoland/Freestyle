using System.Runtime.Serialization;

namespace WcfService.Models.DTO.ToDb
{
    [DataContract]
    public class UserLogIn
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Pass { get; set; }

        [DataMember]
        public string ClientVer { get; set; }
    }
}