namespace WcfService.Models.DTO.ToDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    [DataContract]
    public class SettingsSetBool
    {
        [DataMember]
        public string UserKey { get; set; }

        [DataMember]
        public bool Data { get; set; }
    }
}