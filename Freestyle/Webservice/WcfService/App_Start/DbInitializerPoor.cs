//*** -> means that data from production version was hided for security reason

namespace WcfService.AppStart
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;

    //only one user create rhymes
    public class DbInitializerPoor
    {
        public static string UserId;
        public static string UserName = "test12login@wp.com";
        public static string UserPassword = "***";
        public static string UserAppLogin = "test12loginApp";
       // public static string UserKey = DbInitializerPoor.UserAppLogin + "_0";

        public static string UserReplyId;
        public static string UserNameReply = "test12loginReply@gmail.com";
        public static string UserPasswordReply = "***";
        public static string UserAppLoginReply = "test12loginApp2Reply";
      //  public static string UserReplyKey = DbInitializerPoor.UserAppLoginReply + "_0";

        public static string UserReply2Id;
        public static string UserNameReply2 = "test12loginReply2@gmail.com";
        public static string UserPasswordReply2 = "***";
        public static string UserAppLoginReply2 = "test12loginApp2Reply2";
     //   public static string UserReply2Key = DbInitializerPoor.UserAppLoginReply2 + "_0";

        public static string UserNoDataId;
        public static string UserNameNoData = "noData0000@gmail.com";
        public static string UserPassNoData = "***";
        public static string UserAppLoginNoData = "AppLoginnoData0000";
     //   public static string UserKeyNoData = DbInitializerPoor.UserAppLoginNoData + "_1";

        public static void Seed(ApplicationDbContext context)
        {

            DateTime myDate = DateTime.Now;
            //trick to solve problem of unprecission read data by ef from db
            myDate = myDate.AddTicks(-(myDate.Ticks % TimeSpan.TicksPerSecond));

            var user = new ApplicationUser();
            user.UserName = DbInitializerPoor.UserName;
            user.ApplicationLogin = DbInitializerPoor.UserAppLogin;
            //user.UserKey = DbInitializerPoor.UserKey;

            var userReply = new ApplicationUser();
            userReply.UserName = DbInitializerPoor.UserNameReply;
            userReply.ApplicationLogin = DbInitializerPoor.UserAppLoginReply;
            //userReply.UserKey = DbInitializerPoor.UserReplyKey;

            var userReply2 = new ApplicationUser();
            userReply2.UserName = DbInitializerPoor.UserNameReply2;
            userReply2.ApplicationLogin = DbInitializerPoor.UserAppLoginReply2;
            //userReply2.UserKey = DbInitializerPoor.UserReply2Key;

            var userNoData = new ApplicationUser();
            userNoData.UserName = DbInitializerPoor.UserNameNoData;
            userNoData.ApplicationLogin = DbInitializerPoor.UserAppLoginNoData;
            //userNoData.UserKey = DbInitializerPoor.UserKeyNoData;

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userResult = userManager.Create(user, DbInitializerPoor.UserPassword);
            var userResultReply = userManager.Create(userReply, DbInitializerPoor.UserPasswordReply);
            var userResultReply2 = userManager.Create(userReply2, DbInitializerPoor.UserPasswordReply2);
            var userResultNoData = userManager.Create(userNoData, DbInitializerPoor.UserPassNoData);

            if (userResult.Succeeded && userResultReply.Succeeded && userResultReply2.Succeeded && userResultNoData.Succeeded)
            {
                UserId = user.Id;
                UserReplyId = userReply.Id;
                UserReply2Id = userReply2.Id;
                UserNoDataId = userNoData.Id;

                Rhyme rhym1 = new Rhyme
                {
                    Title = "Tytuł1",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-0.9),
                    ModifiedDate = myDate.AddDays(-0.9),
                    FinishedDate = null,
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        new Sentence{
                            AuthorReply = null,
                            CreatedDate = myDate.AddDays(-0.9),
                            SentenceText = "Początek zdania1",
                            ReplyText = null
                        }
                    },
                    SuggestedReplies = new List<SuggestedReply>
                    {
                        //the youngest
                        new SuggestedReply{
                            Author = userReply,
                            CreatedDate = myDate.AddDays(-0.7),
                            ReplyText = "Sugerowana odp1"
                        },
                        //older than above
                        new SuggestedReply{
                            Author = userReply2,
                            CreatedDate = myDate.AddDays(-0.8),
                            ReplyText = "Sugerowana odp1(2)"
                        }
                    }
                };
                Rhyme rhym2 = new Rhyme
                {
                    Title = "Tytuł2",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-0.8),
                    ModifiedDate = myDate.AddDays(-0.8),
                    FinishedDate = null,
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence{
                            AuthorReply = null,
                            CreatedDate = myDate.AddDays(-0.8),
                            SentenceText = "Początek zdania2",
                            ReplyText = null
                        }
                    },
                };
                Rhyme rhym3 = new Rhyme
                {
                    Title = "Tytuł3",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-0.8),
                    ModifiedDate = myDate.AddDays(-0.3),
                    FinishedDate = null,
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-0.8),
                            SentenceText = "Początek zdania1 w 3",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-0.7),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-0.6),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-0.5),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-0.4),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = null,
                            CreatedDate = myDate.AddDays(-0.3),
                            SentenceText = "Początek zdania6 w 3",
                            ReplyText = null
                        }

                    }
                };

                Rhyme rhym4 = new Rhyme
                {
                    Title = "Tytuł4",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-0.95),
                    ModifiedDate = myDate.AddDays(-0.95),
                    FinishedDate = null,
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence{
                            AuthorReply = null,
                            CreatedDate = myDate.AddDays(-0.95),
                            SentenceText = "Początek zdania4",
                            ReplyText = null
                        }
                    },
                    
                };

                Rhyme rhym5 = new Rhyme
                {
                    Title = "Skończona 1",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-1.8),
                    ModifiedDate = myDate.AddDays(-0.99),
                    FinishedDate = myDate.AddDays(-0.99),
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                    },

                };

                Rhyme rhym6 = new Rhyme
                {
                    Title = "Skończona 2",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-1.8),
                    ModifiedDate = myDate.AddDays(-0.95),
                    FinishedDate = myDate.AddDays(-0.95),
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                    },

                };

                Rhyme rhym7 = new Rhyme
                {
                    Title = "Skończona 3",
                    VotesValue = 0,
                    VotesAmount = 0,
                    CreatedDate = myDate.AddDays(-1.8),
                    ModifiedDate = myDate.AddDays(-0.9),
                    FinishedDate = myDate.AddDays(-0.9),
                    AuthorId = user.Id,
                    Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                    },

                };

                


                context.Rhymes.Add(rhym1);
                context.Rhymes.Add(rhym2);
                context.Rhymes.Add(rhym3);
                context.Rhymes.Add(rhym4);

                context.Rhymes.Add(rhym5);
                context.Rhymes.Add(rhym6);
                context.Rhymes.Add(rhym7);

                context.SaveChanges();
            }
        }
    }
}