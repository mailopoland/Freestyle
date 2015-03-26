namespace WcfService.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SuggestedReply
    {
        public int Id { get; set; }

        [Required]
        [StringLength(Global.RhymeMsgMaxLength, MinimumLength = Global.RhymeMsgMinLength)]
        public string ReplyText { get; set; }

        [Required]
        [Index("UniqueAuthorId-RhymeId", 2, IsUnique = true)]
        public int RhymeId { get; set; }

        [Required]
        [StringLength(128)]
        [Index("UniqueAuthorId-RhymeId", 1, IsUnique = true)]
        public string AuthorId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual Rhyme Rhyme { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}