//*** -> means that data from production version was hided for security reason

namespace WcfService.AppStart
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;

    public class DbInitializer : System.Data.Entity.DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public static string UserId;
        public static string UserName = "test12email@wp.com";
        public static string UserPassword = "***";
        public static string UserAppLogin = "test12loginApp";
        public static string UserEmail = UserName;
        public static int UserNotiFreq = 1;

        public static string UserReplyId;
        public static string UserNameReply = "mmmmmmmmmmmmmmmmmmmmmmmmmmmmmm@gmail.com";
        public static string UserPasswordReply = "***";
        public static string UserAppLoginReply = "toMek";
        public static string UserReplyEmail = UserNameReply;

        public static string UserReply2Id;
        public static string UserNameReply2 = "test12loginReply2@gmail.com";
        public static string UserPasswordReply2 = "***";
        public static string UserAppLoginReply2 = "test12loginApp2Reply2";

        public static string UserReply3Id;
        public static string UserNameReply3 = "test12loginReply3@gmail.com";
        public static string UserPasswordReply3 = "***";
        public static string UserAppLoginReply3 = "test12loginApp2Reply3";

        public static string UserNoDataId;
        public static string UserNameNoData = "noData0000@gmail.com";
        public static string UserPassNoData = "***";
        public static string UserAppLoginNoData = "AppLoginnoData0000";

        protected override void Seed(ApplicationDbContext context)
        {
            try
            {
                DateTime myDate = DateTime.Now;
                //trick to solve problem of unprecission read data by ef from db
                myDate = myDate.AddTicks(-(myDate.Ticks % TimeSpan.TicksPerSecond));

                var user = new ApplicationUser();
                user.UserName = DbInitializer.UserName;
                user.ApplicationLogin = DbInitializer.UserAppLogin;
                user.Email = DbInitializer.UserEmail;
                user.NotiFreq = DbInitializer.UserNotiFreq;

                var userReply = new ApplicationUser();
                userReply.UserName = DbInitializer.UserNameReply;
                userReply.ApplicationLogin = DbInitializer.UserAppLoginReply;
                userReply.Email = DbInitializer.UserReplyEmail;

                var userReply2 = new ApplicationUser();
                userReply2.UserName = DbInitializer.UserNameReply2;
                userReply2.ApplicationLogin = DbInitializer.UserAppLoginReply2;

                var userReply3 = new ApplicationUser();
                userReply3.UserName = DbInitializer.UserNameReply3;
                userReply3.ApplicationLogin = DbInitializer.UserAppLoginReply3;

                var userNoData = new ApplicationUser();
                userNoData.UserName = DbInitializer.UserNameNoData;
                userNoData.ApplicationLogin = DbInitializer.UserAppLoginNoData;

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var userResult = userManager.Create(user, DbInitializer.UserPassword);
                var userResultReply = userManager.Create(userReply, DbInitializer.UserPasswordReply);
                var userResultReply2 = userManager.Create(userReply2, DbInitializer.UserPasswordReply2);
                var userResultReply3 = userManager.Create(userReply3, DbInitializer.UserPasswordReply3);
                var userResultNoData = userManager.Create(userNoData, DbInitializer.UserPassNoData);

                
                if (userResult.Succeeded && userResultReply.Succeeded && userResultReply2.Succeeded && userResultNoData.Succeeded && userResultReply3.Succeeded)
                {
                    UserId = user.Id;
                    UserReplyId = userReply.Id;
                    UserReply2Id = userReply2.Id;
                    UserReply3Id = userReply3.Id;
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
                            CreatedDate = myDate.AddDays(-0.8),
                            ReplyText = "Sugerowana odp1(2)"
                        },
                        //middle
                        new SuggestedReply{
                            Author = userReply3,
                            CreatedDate = myDate.AddDays(-0.75),
                            ReplyText = "Środkowy tekst"
                        },
                        //older than above
                        new SuggestedReply{
                            Author = userReply2,
                            CreatedDate = myDate.AddDays(-0.7),
                            ReplyText = "Sugerowana odp1"
                        },
                    }
                    };
                    Rhyme rhym1_2 = new Rhyme
                    {
                        Title = "Jedyna_odp",
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
                            SentenceText = "Początek joo1",
                            ReplyText = null
                        }
                    },
                        SuggestedReplies = new List<SuggestedReply>
                    {
                        //the youngest
                        new SuggestedReply{
                            Author = userReply,
                            CreatedDate = myDate.AddDays(-0.8),
                            ReplyText = "jedyna odp"
                        },
                        
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
                        Showed = true,
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
                        Showed = true,
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
                        Showed = true,
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
                    //the same finish date like rhym5_x
                    Rhyme rhym5 = new Rhyme
                    {
                        Title = "Skończona 1",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.92),
                        FinishedDate = myDate.AddDays(-0.92),
                        AuthorId = user.Id,
                        Showed = true,
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

                    //the same finish date like rhym5_x
                    Rhyme rhym5_2 = new Rhyme
                    {
                        Title = "Skończona 1_2",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.92),
                        FinishedDate = myDate.AddDays(-0.92),
                        AuthorId = user.Id,
                        Showed = true,
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
                    //the same finish date like rhym5_x
                    Rhyme rhym5_3 = new Rhyme
                    {
                        Title = "Skończona 1_3",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.92),
                        FinishedDate = myDate.AddDays(-0.92),
                        AuthorId = user.Id,
                        Showed = true,
                        SuggestedReplies = new List<SuggestedReply>(){
                            new SuggestedReply{
                                Author = userReply,
                                CreatedDate = myDate.AddDays(-0.9),
                                ReplyText = "To już na nic tutaj"
                            }
                        },
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

                    //the same finish date like rhym5_x
                    Rhyme rhym5_4 = new Rhyme
                    {
                        Title = "Skończona 1_4",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.92),
                        FinishedDate = myDate.AddDays(-0.92),
                        AuthorId = user.Id,
                        Showed = true,
                        SuggestedReplies = new List<SuggestedReply>(){
                            new SuggestedReply{
                                Author = userReply,
                                CreatedDate = myDate.AddDays(-0.9),
                                ReplyText = "To już na nic tutaj"
                            }
                        },
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
                        Showed = true,
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

                    Rhyme rhym6_2 = new Rhyme
                    {
                        Title = "Skończona 2_2",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.95),
                        FinishedDate = myDate.AddDays(-0.95),
                        AuthorId = user.Id,
                        Showed = true,
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
                        Showed = true,
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

                    Rhyme rhym7_last1 = new Rhyme
                    {
                        Title = "Skończona 3_last1",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.85),
                        FinishedDate = myDate.AddDays(-0.85),
                        AuthorId = user.Id,
                        Showed = true,
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

                    Rhyme rhym7_last2 = new Rhyme
                    {
                        Title = "Skończona 3_last2",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.85),
                        FinishedDate = myDate.AddDays(-0.85),
                        AuthorId = user.Id,
                        Showed = true,
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

                    Rhyme rhym8 = new Rhyme
                    {
                        Title = "Tytuł4",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-0.95),
                        ModifiedDate = myDate.AddDays(-0.95),
                        FinishedDate = null,
                        AuthorId = userReply.Id,
                        Showed = true,
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

                    Rhyme rhym9 = new Rhyme
                    {
                        Title = "Tytuł3 user odpwiedzial",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-0.8),
                        ModifiedDate = myDate.AddDays(-0.3),
                        FinishedDate = null,
                        AuthorId = userReply2.Id,
                        Showed = true,
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
                            AuthorReply = user,
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

                    Rhyme rhym9_2 = new Rhyme
                    {
                        Title = "Tytuł3 user odpwiedzial2",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-0.8),
                        ModifiedDate = myDate.AddDays(-0.3),
                        FinishedDate = null,
                        AuthorId = userReply2.Id,
                        Showed = true,
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
                            AuthorReply = user,
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
                            AuthorReply = user,
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

                    Rhyme rhym10 = new Rhyme
                    {
                        Title = "Tytułowo i w ogle",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-0.95),
                        ModifiedDate = myDate.AddDays(-0.95),
                        FinishedDate = null,
                        AuthorId = userReply.Id,
                        Showed = true,
                        Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence{
                            AuthorReply = null,
                            CreatedDate = myDate.AddDays(-0.95),
                            SentenceText = "Początek to fyu",
                            ReplyText = null
                        }
                    },

                    };

                    Rhyme rhym11 = new Rhyme
                    {
                        Title = "Skończona 1nie usera",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.99),
                        FinishedDate = myDate.AddDays(-0.99),
                        AuthorId = userReply2.Id,
                        Showed = true,
                        Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3nie usera",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "nie useraPoczątek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "nie useraPoczątek zdania2 w 3",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "nie useraPoczątek zdania3 w 3",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "nie useraPoczątek zdania4 w 3",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "nie useraPoczątek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                    },

                    };

                    Rhyme rhym12 = new Rhyme
                    {
                        Title = "Skończona 2nie usera",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.95),
                        FinishedDate = myDate.AddDays(-0.95),
                        AuthorId = userReply2.Id,
                        Showed = true,
                        Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3nie usera",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania2 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                    },

                    };

                    Rhyme rhym13 = new Rhyme
                    {
                        Title = "Skończona 2nie usera",
                        VotesValue = -1,
                        VotesAmount = 1,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.95),
                        FinishedDate = myDate.AddDays(-0.95),
                        AuthorId = userReply2.Id,
                        Showed = true,
                        Votes = new List<Vote>{
                            new Vote{
                               User = user,
                               Value = false
                            }
                        },
                        Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3nie usera2",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = user,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3nie us2era",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3nie u214sera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3nie us12era",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania54 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania232 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania312 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                    },

                    };

                    Rhyme rhym14 = new Rhyme
                    {
                        Title = "Skoń 3 nie usera starsza acpt",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.9),
                        FinishedDate = myDate.AddDays(-0.9),
                        AuthorId = userReply2.Id,
                        Showed = true,
                        Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3nie usera",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = user,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania2 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                    },

                    };

                    Rhyme rhym15 = new Rhyme
                    {
                        Title = "Skoń 3 nie usera nowsza acpt",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.9),
                        FinishedDate = myDate.AddDays(-0.9),
                        AuthorId = userReply2.Id,
                        Showed = true,
                        Sentences = new List<Sentence>
                    {
                        //older than 1
                        new Sentence
                        {
                            AuthorReply = user,
                            CreatedDate = myDate.AddDays(-1.8),
                            SentenceText = "Początek zdania1 w 3nie usera",
                            ReplyText = "odpowiedź1"
                        },
                        new Sentence
                        {
                            AuthorReply = user,
                            CreatedDate = myDate.AddDays(-1.7),
                            SentenceText = "Początek zdania2 w 3nie usera",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.6),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.5),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.4),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.3),
                            SentenceText = "Początek zdania2 w 3nie usera ooo :)",
                            ReplyText = "odpowiedź2"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.2),
                            SentenceText = "Początek zdania3 w 3nie usera",
                            ReplyText = "odpowiedź3"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1.1),
                            SentenceText = "Początek zdania4 w 3nie usera",
                            ReplyText = "odpowiedź4"
                        },
                        new Sentence
                        {
                            AuthorReply = userReply,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania5 w 3nie usera",
                            ReplyText = "odpowiedź5"
                        },
                    },

                    };

                    Rhyme rhym16 = new Rhyme
                    {
                        Title = "Takie, że już mogę kończyć",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-1),
                        FinishedDate = null,
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
                            AuthorReply = userReply2,
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
                            AuthorReply = userReply3,
                            CreatedDate = myDate.AddDays(-1.05),
                            SentenceText = "Początek zdania5 w 3",
                            ReplyText = "odpowiedź5"
                        },
                        new Sentence
                        {
                            AuthorReply = null,
                            CreatedDate = myDate.AddDays(-1),
                            SentenceText = "Początek zdania6 w 3",
                            ReplyText = null
                        },
                    },
                        SuggestedReplies = new List<SuggestedReply>
                    {
                        //the youngest
                        new SuggestedReply{
                            Author = userReply,
                            CreatedDate = myDate.AddDays(-0.7),
                            ReplyText = "Sugerowana odp1"
                        },
                        //middle
                        new SuggestedReply{
                            Author = userReply3,
                            CreatedDate = myDate.AddDays(-0.75),
                            ReplyText = "Środkowy tekst"
                        },
                        //older than above
                        new SuggestedReply{
                            Author = userReply2,
                            CreatedDate = myDate.AddDays(-0.8),
                            ReplyText = "Sugerowana odp1(2)"
                        },
                        
                    }
                    };

                    Rhyme rhym17 = new Rhyme
                    {
                        Title = "Skończone zgodnie z toEnd",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.1),
                        FinishedDate = myDate.AddDays(-0.1),
                        AuthorId = user.Id,
                        Showed = true,
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
                                AuthorReply = userReply2,
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
                                AuthorReply = userReply3,
                                CreatedDate = myDate.AddDays(-1.05),
                                SentenceText = "Początek zdania5 w 3",
                                ReplyText = "odpowiedź5"
                            },
                            new Sentence
                            {
                                AuthorReply = userReply,
                                CreatedDate = myDate.AddDays(-1),
                                SentenceText = "Początek zdania6 w 3",
                                ReplyText = "odpowiedź końcowa"
                            },
                        }
                    };

                    Rhyme rhym18 = new Rhyme
                    {
                        Title = "Nieskończone nie usera",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.15),
                        FinishedDate = null,
                        AuthorId = userReply3.Id,
                        Showed = true,
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
                                AuthorReply = userReply2,
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
                                AuthorReply = user,
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
                                AuthorReply = user,
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
                                AuthorReply = user,
                                CreatedDate = myDate.AddDays(-1.05),
                                SentenceText = "Początek zdania5 w 3",
                                ReplyText = "odpowiedź5"
                            },
                            new Sentence
                            {
                                AuthorReply = null,
                                CreatedDate = myDate.AddDays(-1),
                                SentenceText = "Początek zdania6 w 3",
                                ReplyText = null
                            },
                            
                        },
                        SuggestedReplies = new List<SuggestedReply>
                            {
                                new SuggestedReply{
                                    Author = user,
                                    CreatedDate = myDate.AddDays(-0.7),
                                    ReplyText = "Sugerowana odp usera"
                                },
                            }
                    };


                    Rhyme rhym19 = new Rhyme
                    {
                        Title = "Nieskoń nie u i u moze odp",
                        VotesValue = 0,
                        VotesAmount = 0,
                        CreatedDate = myDate.AddDays(-1.8),
                        ModifiedDate = myDate.AddDays(-0.18),
                        FinishedDate = null,
                        AuthorId = userReply3.Id,
                        Showed = true,
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
                                AuthorReply = userReply2,
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
                                AuthorReply = user,
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
                                AuthorReply = user,
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
                                AuthorReply = user,
                                CreatedDate = myDate.AddDays(-1.05),
                                SentenceText = "Początek zdania5 w 3",
                                ReplyText = "odpowiedź5"
                            },
                            new Sentence
                            {
                                AuthorReply = null,
                                CreatedDate = myDate.AddDays(-1),
                                SentenceText = "Początek zdania6 w 3",
                                ReplyText = null
                            },
                            
                        },
                        SuggestedReplies = new List<SuggestedReply>
                            {
                                new SuggestedReply{
                                    Author = userReply,
                                    CreatedDate = myDate.AddDays(-0.7),
                                    ReplyText = "Sugerowana odp usera"
                                },
                            }
                    };

                    context.Rhymes.Add(rhym1);
                    context.Rhymes.Add(rhym1_2);
                    context.Rhymes.Add(rhym2);
                    context.Rhymes.Add(rhym3);
                    context.Rhymes.Add(rhym4);

                    context.Rhymes.Add(rhym5);
                    context.Rhymes.Add(rhym5_2);
                    context.Rhymes.Add(rhym5_3);
                    context.Rhymes.Add(rhym5_4);
                    context.Rhymes.Add(rhym6);
                    context.Rhymes.Add(rhym6_2);
                    context.Rhymes.Add(rhym7);
                    context.Rhymes.Add(rhym7_last1);
                    context.Rhymes.Add(rhym7_last2);
                    context.Rhymes.Add(rhym8);
                    context.Rhymes.Add(rhym9);
                    context.Rhymes.Add(rhym9_2);
                    context.Rhymes.Add(rhym10);

                    context.Rhymes.Add(rhym11);
                    context.Rhymes.Add(rhym12);
                    context.Rhymes.Add(rhym13);
                    context.Rhymes.Add(rhym14);
                    context.Rhymes.Add(rhym15);
                    context.Rhymes.Add(rhym16);
                    context.Rhymes.Add(rhym17);

                    context.Rhymes.Add(rhym18);
                    context.Rhymes.Add(rhym19);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}