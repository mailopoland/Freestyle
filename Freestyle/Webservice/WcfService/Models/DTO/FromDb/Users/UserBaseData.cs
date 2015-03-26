namespace WcfService.Models.DTO.FromDb.Users
{
    using System.Runtime.Serialization;
    using WcfService.Models.Domain;

    [DataContract]
    public class UserBaseData
    {
        public UserBaseData() { }

        public UserBaseData(ApplicationUser user)
        {
            if (user != null)
            {
                Login = user.ApplicationLogin;
                Email = user.Email;
            }
        }

        [DataMember]
        public string Login { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; }
    }
}