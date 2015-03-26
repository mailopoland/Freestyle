namespace WcfService.Models.DTO.ToDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class RhymeWithSenToSave : RhymeToSave
    {
        [DataMember]
        public string Sentence { get; set; }
    }
}