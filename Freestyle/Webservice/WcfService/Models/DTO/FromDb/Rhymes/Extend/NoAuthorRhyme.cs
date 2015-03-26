namespace WcfService.Models.DTO.FromDb.Rhymes.Extend
{
    using System.Runtime.Serialization;

    [DataContract]
    public class NoAuthorRhyme
    {
        [DataMember(EmitDefaultValue = false)]
        public CompletedWithAuthorViewRhyme Completed { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public WriteRespondUser Incompleted { get; set; }
    }
}