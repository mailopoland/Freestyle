namespace WcfService.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sentence
    {
        public int Id { get; set; }

        [Required]
        [StringLength(Global.RhymeMsgMaxLength, MinimumLength = Global.RhymeMsgMinLength)]
        public string SentenceText { get; set; }

        [StringLength(Global.RhymeMsgMaxLength, MinimumLength = Global.RhymeMsgMinLength)]
        public string ReplyText { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Index]
        [Required]
        public int RhymeId { get; set; }

        [StringLength(128)]
        public string AuthorReplyId { get; set; }

        //inform user that this he's respond was accepted
        public bool Showed { get; set; }

        public virtual ApplicationUser AuthorReply { get; set; }

        public virtual Rhyme Rhyme { get; set; }
    }
}