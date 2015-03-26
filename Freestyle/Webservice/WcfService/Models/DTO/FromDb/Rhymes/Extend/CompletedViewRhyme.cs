namespace WcfService.Models.DTO.FromDb.Rhymes.Extend
{
    using System.Runtime.Serialization;
    using WcfService.Models.DTO.FromDb.Rhymes.Bases;

    [DataContract]
    public class CompletedViewRhyme : BaseViewRhyme
    {
        [DataMember]
        public int Points { get; set; }

        [DataMember]
        public bool CanVote { get; set; }
    }
}