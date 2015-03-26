namespace WcfService.Models.DTO.FromDb.Rhymes.Bases
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using WcfService.MainHelpers;
    using WcfService.Models.DTO.FromDb.Rhymes.Helpers;

    [DataContract]
    public abstract class BaseViewRhyme
    {
        [DataMember]
        public int RhymeId { get; set; }

        //use only if we want to order by sth other that id (date of created)
        [DataMember(EmitDefaultValue = false)]
        public string CurValue { get; set; }

        [DataMember]
        /// <summary> return: 
        /// 0 => is author (author can't fav his rhymes), 
        /// 1 => is not fav, 
        /// 2 => is fav
        /// </summary>
        public int IsFavorited { get; set; }

        public BaseViewRhyme()
        {
            SentencesToShow = new List<SentenceToShow>();
        }
        
        [DataMember]
        public string Title {
            get { return title; }
            set { title = TextChecker.CheckText(value); }
        }

        [DataMember]
        public List<SentenceToShow> SentencesToShow;

        private string title;
    }
}