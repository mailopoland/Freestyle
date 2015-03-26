namespace WcfService.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rhyme
    {
        /*
        public Rhyme()
        {
            Sentences = new List<Sentence>();
            FavoritedUsers = new List<FavoritedRhyme>();
            SuggestedReplies = new List<SuggestedReply>();
        }
        */

        public int Id { get; set; }

        [StringLength(Global.RhymeTitleMaxLength, MinimumLength = Global.RhymeTitleMinLength)]
        [Required]
        public string Title { get; set; }

        [Required]
        public int VotesValue { get; set; }

        /// <summary>
        /// Number of users who votes for this rhyme
        /// </summary>
        [Required]
        public int VotesAmount { get; set; }

        [Index]
        public DateTime? FinishedDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        //date of update by author - when update dataset always set it 
        //it solves currency problem (tell to do rollback if more tran 1 trans update the same rhyme)
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(128)]
        public string AuthorId { get; set; }

        //true if it was showed after add new reply
        public bool Showed { get; set; }

        public virtual ApplicationUser Author { get; set; }


        //have to do it in fluent api
        public virtual ICollection<Sentence> Sentences { get; set; }

        //have to do it in fluent api
        //have information about conaconation table (user and his fav rhyme)
        public virtual ICollection<FavoritedRhyme> FavoritedUsers { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<SuggestedReply> SuggestedReplies { get; set; }
    }
}