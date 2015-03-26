namespace WcfService.Models.DTO.FromDb.Rhymes.Extend
{
    using System.Runtime.Serialization;
    using WcfService.Models.DTO.FromDb.Rhymes.Bases;
    using WcfService.Models.DTO.FromDb.Users;

    [DataContract]
    public class WriteRespondUser : IncompletedViewRhyme
    {
        [DataMember]
        public UserBaseData Author { get; set; }

        //it give possibility to check that user respond to correct sentence
        [DataMember]
        public int LastSentId { get; set; }

        //usefull if user download his rhymes in category: "with my rhymes incompleted"
        //(possible that his respond is waiting but before one of his responds was accepted)
        //return (set) only if true
        [DataMember(EmitDefaultValue = false)]
        public bool IsResponded { get; set; }        
    }
}