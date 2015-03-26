//ChooseAnswerAuthorActivite
namespace WcfService.Models.DTO.FromDb.Rhymes.Extend
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using WcfService.Models.DTO.FromDb.Rhymes.Bases;
    using WcfService.Models.DTO.FromDb.Rhymes.Helpers;

    [DataContract]
    public class ChooseWriteSentence : IncompletedViewRhyme
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ReplyToAccepted> SuggestedReplies;
    }
}