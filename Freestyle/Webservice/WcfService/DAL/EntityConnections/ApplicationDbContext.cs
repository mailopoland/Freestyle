namespace WcfService.DAL.EntityConnections
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using WcfService.Models.Domain;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private bool lazyLoadEnable { get { return true; } }

        public ApplicationDbContext()
            : base("RhymeWcfContext")
        {
            this.Configuration.LazyLoadingEnabled = lazyLoadEnable;
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = lazyLoadEnable;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();

            //rhym connections

            modelBuilder.Entity<SuggestedReply>()
                .HasRequired(s => s.Rhyme)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FavoritedRhyme>()
                .HasRequired(fr => fr.Rhyme)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.Rhyme)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FavoritedRhyme>().HasRequired(fr => fr.Rhyme).WithMany(r => r.FavoritedUsers).HasForeignKey(fr => fr.RhymeId);
            modelBuilder.Entity<Sentence>().HasRequired(s => s.Rhyme).WithMany(r => r.Sentences).HasForeignKey(s => s.RhymeId);
            modelBuilder.Entity<SuggestedReply>().HasRequired(s => s.Rhyme).WithMany(r => r.SuggestedReplies).HasForeignKey(s => s.RhymeId);
            modelBuilder.Entity<Vote>().HasRequired(v => v.Rhyme).WithMany(r => r.Votes).HasForeignKey(v => v.RhymeId);
            // application user connections


            modelBuilder.Entity<SuggestedReply>()
                .HasRequired(s => s.Author)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FavoritedRhyme>()
                .HasRequired(fr => fr.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rhyme>().HasRequired(r => r.Author).WithMany(au => au.Rhymes).HasForeignKey(r => r.AuthorId);
            modelBuilder.Entity<FavoritedRhyme>().HasRequired(fr => fr.User).WithMany(au => au.FavoritedRhymes).HasForeignKey(fr => fr.UserId);
            modelBuilder.Entity<Sentence>().HasOptional(s => s.AuthorReply).WithMany(au => au.WithUserReplySentences).HasForeignKey(s => s.AuthorReplyId);
            modelBuilder.Entity<SuggestedReply>().HasRequired(s => s.Author).WithMany(au => au.SuggestedReplies).HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<Vote>().HasRequired(v => v.User).WithMany(au => au.Votes).HasForeignKey(v => v.UserId);
        }

        public DbSet<Rhyme> Rhymes { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<SuggestedReply> SuggestedReplies { get; set; }
        public DbSet<FavoritedRhyme> FavoritedRhymes { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}