using System.Runtime.Serialization;
using WcfService.Models.DTO.FromDb.Users;

namespace WcfService.Models.DTO.FromDb.Rhymes.Extend
{
    [DataContract]
    public class CompletedWithAuthorViewRhyme : CompletedViewRhyme
    {
        [DataMember]
        public UserBaseData Author { get; set; }
    }
}