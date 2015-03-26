namespace WcfService.Models.Domain
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(Global.UserLoginMaxLength, MinimumLength = Global.UserLoginMinLength)]
        [Index(IsUnique = true)]
        public string ApplicationLogin { get; set; }

        public bool NoRespNoti { get; set; }

        public bool NoAcceptNoti { get; set; }

        public int NotiFreq { get; set; }

        //have to do it in fluent api
        public virtual ICollection<Sentence> WithUserReplySentences { get; set; }
        
        //have to do it in fluent api
        public virtual ICollection<SuggestedReply> SuggestedReplies { get; set; }

        //have to do it in fluent api
        public virtual ICollection<Rhyme> Rhymes { get; set; }

        //have to do it in fluent api
        public virtual ICollection<FavoritedRhyme> FavoritedRhymes { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}