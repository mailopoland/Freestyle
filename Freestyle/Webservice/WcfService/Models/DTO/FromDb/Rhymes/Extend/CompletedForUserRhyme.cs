namespace WcfService.Models.DTO.Out.Rhymes.Extends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;
    using WcfService.Models.DTO.Out.Rhymes.Helpers;

    public class CompletedForUserRhyme : CompletedViewRhyme
    {
        [DataMember]
        public UserBaseData Author { get; set; }
    }
}