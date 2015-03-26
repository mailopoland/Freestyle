namespace WcfService.Models.DTO.FromDb.Rhymes
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class NewRhymeReturn
    {
        [DataMember]
        public String ToEnd { get; set; }

        [DataMember]
        public int RhymeId { get; set; }
    }
}