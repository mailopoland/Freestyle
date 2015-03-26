namespace WcfService.Models.DTO.FromDb.Rhymes.Bases
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class IncompletedViewRhyme : BaseViewRhyme
    {
        [DataMember]
        public String ToEnd { get; set; }
    }
}