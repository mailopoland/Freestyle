namespace WcfService.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public bool Value { get; set; }

        [Required]
        [Index("UniqueRhymeId-UserId", 1, IsUnique = true)]
        public int RhymeId { get; set; }

        [Required]
        [Index("UniqueRhymeId-UserId", 2, IsUnique = true)]
        public String UserId { get; set; }

        public virtual Rhyme Rhyme { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}