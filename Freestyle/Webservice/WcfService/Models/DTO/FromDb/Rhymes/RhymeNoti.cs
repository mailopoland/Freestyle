namespace WcfService.Models.DTO.Out.Rhymes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    [DataContract]
    public class RhymeNoti
    {
        [DataMember]
        public int RhymeId { get; set; }
    }
}