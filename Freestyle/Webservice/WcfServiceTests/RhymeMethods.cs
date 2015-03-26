namespace WcfServiceTests
{
    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WcfService;
    using WcfService.AppStart;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.FromDb.Rhymes;
    using WcfService.Models.DTO.ToDb;
    using WcfService.Repositories;
    using WcfServiceTests.Helpers;

    [TestClass]
    public class RhymeMethods : BaseTestCreatorAbstract
    {

        private RhymeRepository repo;
        private string userName;
        private string userPassword;
        private string userId;


        [TestInitialize()]
        public new virtual void Init()
        {
            base.Init();
            
            userName = DbInitializer.UserName;
            userPassword = DbInitializer.UserPassword;
            var user = userManager.Find(userName, userPassword);
            repo = new RhymeRepository(user.Id + correctClientVer,context);
            userId = user.Id;
        }

        [TestCleanup()]
        public new virtual void Close()
        {
             base.Close();
        }

        [TestMethod]
        public void CreateSimpleRhyme()
        {
            string title = "Fajny tytuł";
            string sentence = "Jakieś tam zdanie";
            string userKey;
            int newRhymeCurVal;
            List<Sentence> sentenceResult;
            NewRhymeReturn newRhymeResult;
            NewRhyme newRhyme;
            List<NewRhyme> rhymeResult;
            NewRhyme oneRhymeResult;
            string userName = DbInitializer.UserName;
            string userPassword = DbInitializer.UserPassword;

            userKey = userManager.Find(userName, userPassword).Id;
            newRhyme = new NewRhyme
            {
                UserKey = userKey,
                Title = title,
                Text = sentence
            };
            
            newRhymeResult = repo.CreateNewRhyme(newRhyme);
            newRhymeCurVal = newRhymeResult.RhymeId;
            sentenceResult = context.Sentences
                .Where(s => s.Rhyme.Id == newRhymeCurVal)
                .OrderByDescending(s => s.CreatedDate).ToList();
            rhymeResult = context.Rhymes
                .Where(r => r.Id == newRhymeCurVal)
                .Select(r => new NewRhyme
                {
                    UserKey = r.Author.Id,
                    Title = r.Title
                }).ToList();

            oneRhymeResult = rhymeResult[0];
            oneRhymeResult.Text = sentenceResult[0].SentenceText;

            Assert.IsTrue(newRhymeCurVal > 0);
            Assert.IsTrue(rhymeResult.Count < 2);
            Assert.IsTrue(sentenceResult.Count < 2);
            Assert.AreEqual(newRhyme.UserKey, oneRhymeResult.UserKey);
            Assert.AreEqual(newRhyme.Title, oneRhymeResult.Title);
            Assert.AreEqual(newRhyme.Text, oneRhymeResult.Text);
            Assert.AreEqual((Global.MinMsgAmount - 1).ToString(), newRhymeResult.ToEnd);

        }

        [TestMethod]
        public void GetRhymeAuthorIncomplSortId_Any()
        {
            using (var repo2 = new RhymeRepository(DbInitializer.UserNoDataId + correctClientVer, context))
            {
                Assert.IsNull(repo2.GetRhymeAuthorIncomplSortId(1, false));
            }
        }

        [TestMethod]
        public void GetRhymeAuthorIncomplSortId_Last()
        {
            //ordered ascending
            var rhymeExp = context.Rhymes
                .Where(r => r.FinishedDate == null
                && r.AuthorId == userId)
                .OrderByDescending(r => r.Id).FirstOrDefault();

            Assert.IsNotNull(rhymeExp, "Wrong downloaded obj to compare from db");

            var rhymeDown = repo.GetRhymeAuthorIncomplSortId(int.Parse(Global.DefaultPrevId), false);

            ModelsComparer.ChooseWriteSentenceComp(rhymeExp, rhymeDown);

            rhymeDown = repo.GetRhymeAuthorIncomplSortId(int.Parse(Global.DefaultPrevId), true);

            ModelsComparer.ChooseWriteSentenceComp(rhymeExp, rhymeDown);
        }

        [TestMethod]
        public void GetRhymeAuthorIncomplSortId_All()
        {
            //ordered ascending
            var rhymesExpected = context.Rhymes
                .Where(r => r.FinishedDate == null
                && r.AuthorId == userId)
                .OrderBy(r => r.Id).ToList();

            //for sure that rhymesExpected was downloaded correct
            Assert.IsTrue(rhymesExpected.Count > 2, "Wrong downloaded obj to compare from db");

            //prev (again first)
            var rhymeDown = repo.GetRhymeAuthorIncomplSortId(rhymesExpected.First().Id, false);

            foreach (var rhymeExp in rhymesExpected)
            {
                ModelsComparer.ChooseWriteSentenceComp(rhymeExp, rhymeDown);
                //next
                rhymeDown = repo.GetRhymeAuthorIncomplSortId(rhymeExp.Id, true);
            }

            rhymesExpected = rhymesExpected.OrderByDescending(r => r.Id).ToList();
            //next (again last)
            rhymeDown = repo.GetRhymeAuthorIncomplSortId(rhymesExpected.First().Id, true);

            foreach (var rhymeExp in rhymesExpected)
            {
                ModelsComparer.ChooseWriteSentenceComp(rhymeExp, rhymeDown);
                //prev
                rhymeDown = repo.GetRhymeAuthorIncomplSortId(rhymeExp.Id, false);
            }
        }

        [TestMethod]
        public void GetRhymesNoAuthorIncomplSortId_Any()
        {
            //have to get it first to init poor db (otherwise userId == null)
            ApplicationDbContext contextPoorInit = contextPoor;
            using (var repo2 = new RhymeRepository(DbInitializerPoor.UserId + correctClientVer, contextPoorInit))
            {
                var el = repo2.GetRhymeNoAuthorIncomplSortId(1, false);
                Assert.IsNull(el);
            }
        }

        [TestMethod]
        public void GetRhymeNoAuthorIncomplSortId_Last()
        {
            //ordered ascending
            var rhymeExp = context.Rhymes
                .Where(r => r.FinishedDate == null
                && r.AuthorId != userId
                && r.SuggestedReplies.All(sr => sr.AuthorId != userId))
                .OrderByDescending(r => r.Id).FirstOrDefault();

            Assert.IsNotNull(rhymeExp, "Wrong downloaded obj to compare from db");

            var rhymeDown = repo.GetRhymeNoAuthorIncomplSortId(int.Parse(Global.DefaultPrevId), false);

            ModelsComparer.WriteRespondUserComp(rhymeExp, rhymeDown);

            rhymeDown = repo.GetRhymeNoAuthorIncomplSortId(int.Parse(Global.DefaultPrevId), true);

            ModelsComparer.WriteRespondUserComp(rhymeExp, rhymeDown);
        }

        [TestMethod]
        public void GetRhymeNoAuthorIncomplSortId_WantedFirstOrLastWhichWasFinished()
        {
            //ordered ascending
            var rhymesExpected = context.Rhymes
               .Where(r => r.FinishedDate == null
               && r.AuthorId != userId
               && r.SuggestedReplies.All(sr => sr.AuthorId != userId))
               .OrderBy(r => r.Id).ToList();

            //for sure that rhymesExpected was downloaded correct
            Assert.IsTrue(rhymesExpected.Count > 2, "Wrong downloaded obj to compare from db");
            Assert.AreNotEqual(rhymesExpected.First().Id, rhymesExpected.Last().Id, "Wrong downloaded objs to compare from db: count have to > 1");
            int forFirst = rhymesExpected.First().Id + 1;
            int forLast = rhymesExpected.Last().Id - 1;
            Assert.AreNotEqual(forFirst, 0, "Wrong downloaded obj to compare from db: first.Id should be != 1");
            var first = repo.GetRhymeNoAuthorIncomplSortId(forFirst, false);
            var last = repo.GetRhymeNoAuthorIncomplSortId(forLast, true);
            ModelsComparer.WriteRespondUserComp(rhymesExpected.First(), first);
            ModelsComparer.WriteRespondUserComp(rhymesExpected.Last(), last);
        }

        [TestMethod]
        public void GetRhymeNoAuthorIncomplSortId_All()
        {
            //ordered ascending
            var rhymesExpected = context.Rhymes
                .Where(r => r.FinishedDate == null 
                && r.AuthorId!= userId
                && r.SuggestedReplies.All(sr => sr.AuthorId != userId))
                .OrderBy(r => r.Id).ToList();

            //for sure that rhymesExpected was downloaded correct
            Assert.IsTrue(rhymesExpected.Count > 2, "Wrong downloaded obj to compare from db");

            //prev (again first)
            var rhymeDown = repo.GetRhymeNoAuthorIncomplSortId(rhymesExpected.First().Id, false);

            foreach(var rhymeExp in rhymesExpected){
                ModelsComparer.WriteRespondUserComp(rhymeExp, rhymeDown);
                //next
                rhymeDown = repo.GetRhymeNoAuthorIncomplSortId(rhymeExp.Id, true);
            }

            rhymesExpected = rhymesExpected.OrderByDescending(r => r.Id).ToList();
            //next (again last)
            rhymeDown = repo.GetRhymeNoAuthorIncomplSortId(rhymesExpected.First().Id, true);
            
            foreach(var rhymeExp in rhymesExpected){
                ModelsComparer.WriteRespondUserComp(rhymeExp, rhymeDown);
                //prev
                rhymeDown = repo.GetRhymeNoAuthorIncomplSortId(rhymeExp.Id, false);
            }

        }

        

        [TestMethod]
        public void GetRhymeAuthorComplSortFinishDate_Any()
        {
            using (var repo2 = new RhymeRepository(DbInitializer.UserNoDataId + correctClientVer, context))
            {
                Assert.IsNull(repo2.GetRhymeAuthorComplSortFinishDate(1,DateTime.MinValue, false));
            }
        }

        [TestMethod]
        public void GetRhymeAuthorComplSortFinishDate_Last()
        {
            var rhymeExp = context.Rhymes
                .Where(r => r.FinishedDate != null
                && r.AuthorId == userId)
                .OrderByDescending(r => r.FinishedDate).ThenByDescending(r => r.Id).FirstOrDefault();

            Assert.IsNotNull(rhymeExp, "Wrong downloaded obj to compare from db");

            var rhymeDown = repo.GetRhymeAuthorComplSortFinishDate(int.Parse(Global.DefaultPrevId), DateTime.Now, false);
            ModelsComparer.CompletedViewRhymeFinishDateComp(rhymeExp, rhymeDown);
            rhymeDown = repo.GetRhymeAuthorComplSortFinishDate(int.Parse(Global.DefaultPrevId), DateTime.Now, true);
            ModelsComparer.CompletedViewRhymeFinishDateComp(rhymeExp, rhymeDown);
        }

        [TestMethod]
        public void GetRhymeAuthorComplSortFinishDate_All()
        {
            //ordered ascending
            var rhymesExpected = context.Rhymes
                .Where(r => r.FinishedDate != null
                && r.AuthorId == userId)
                .OrderBy(r => r.FinishedDate).ThenBy(r => r.Id).ToList();

            //List<string> testList = new List<string>();
            //rhymesExpected.ForEach(r => testList.Add(r.FinishedDate.Value.ToString()));


            //for sure that rhymesExpected was downloaded correct
            Assert.IsTrue(rhymesExpected.Count > 2, "Wrong downloaded obj to compare from db");

            //prev (again first)
            var rhymeDown = repo.GetRhymeAuthorComplSortFinishDate(rhymesExpected.First().Id, rhymesExpected.First().FinishedDate, false);



            foreach (var rhymeExp in rhymesExpected)
            {
                ModelsComparer.CompletedViewRhymeFinishDateComp(rhymeExp, rhymeDown);
                //next
                rhymeDown = repo.GetRhymeAuthorComplSortFinishDate(rhymeExp.Id, rhymeExp.FinishedDate, true);
            }

            rhymesExpected = rhymesExpected.OrderByDescending(r => r.FinishedDate).ThenByDescending(r => r.Id).ToList();
            //next (again last)
            rhymeDown = repo.GetRhymeAuthorComplSortFinishDate(rhymesExpected.First().Id, rhymesExpected.First().FinishedDate, true);

            foreach (var rhymeExp in rhymesExpected)
            {
                ModelsComparer.CompletedViewRhymeFinishDateComp(rhymeExp, rhymeDown);
                //prev
                rhymeDown = repo.GetRhymeAuthorComplSortFinishDate(rhymeExp.Id, rhymeExp.FinishedDate, false);
            }

        }

        [TestMethod]
        public void FinishRhyme_Correct()
        {
            Rhyme downRhymeBefore = context.Rhymes
                .Include(r => r.SuggestedReplies)
                .Where(r => r.FinishedDate == null
                    && 2 * r.Sentences.Count >= Global.MinMsgAmount 
                    && r.SuggestedReplies.Count > 0
                    && r.AuthorId == userId)
                .First();
            SuggestedReply selReply = downRhymeBefore.SuggestedReplies.First();
            SuggestedReply saveReply = new SuggestedReply
            {
                AuthorId = selReply.AuthorId,
                ReplyText = selReply.ReplyText
            };
            RhymeToSave argToSave = new RhymeToSave{
                ReplyId = selReply.Id,
                RhymeId = downRhymeBefore.Id,
                //it is not important in test so give id for possible future changes
                UserKey = userId
            };
            DateTime before = nowDate;
            repo.FinishRhyme(argToSave);
            DateTime after = nowDate;
            Rhyme downRhymeAfter = context.Rhymes.Find(downRhymeBefore.Id);

            Assert.AreEqual(0, downRhymeAfter.SuggestedReplies.Count, "Not deleted suggested replies");
            Assert.IsNotNull(downRhymeAfter.FinishedDate, "Not set finishDate");
            Assert.IsTrue(before <= downRhymeAfter.FinishedDate, "Too small finishDate");
            Assert.IsTrue(after >= downRhymeAfter.FinishedDate, "Too big finishDate");
            Assert.AreEqual(downRhymeAfter.ModifiedDate, downRhymeAfter.FinishedDate, "ModifedDate wrong value");
            downRhymeAfter.Sentences = downRhymeAfter.Sentences.OrderBy(s => s.CreatedDate).ToList();
            Sentence lastSentence = downRhymeAfter.Sentences.Last();
            Assert.AreEqual(saveReply.ReplyText, lastSentence.ReplyText, "Wrong save replyText");
            Assert.AreEqual(saveReply.AuthorId, lastSentence.AuthorReplyId, "Wrong save replyAuthor");
          }

        [TestMethod]
        public void FinishRhyme_WrongToEndValue()
        {
            Rhyme downRhymeBefore = context.Rhymes
                .Include(r => r.SuggestedReplies)
                .Where(r => r.FinishedDate == null
                    && 2 * r.Sentences.Count < Global.MinMsgAmount
                    && r.SuggestedReplies.Count > 0
                    && r.AuthorId == userId)
                .First();
            SuggestedReply selReply = downRhymeBefore.SuggestedReplies.First();
            RhymeToSave argToSave = new RhymeToSave
            {
                ReplyId = selReply.Id,
                RhymeId = downRhymeBefore.Id,
                //it is not important in test so give id for possible future changes
                UserKey = userId + correctClientVer
            };
            repo.FinishRhyme(argToSave);
            Rhyme downRhymeAfter = context.Rhymes.Find(downRhymeBefore.Id);
            Assert.IsNull(downRhymeAfter.FinishedDate);
        }

        [TestMethod]
        public void FinishRhyme_WrongAuthor()
        {
            Rhyme downRhymeBefore = context.Rhymes
                .Include(r => r.SuggestedReplies)
                .Where(r => r.FinishedDate == null
                    && 2 * r.Sentences.Count >= Global.MinMsgAmount
                    && r.SuggestedReplies.Count > 0
                    && r.AuthorId == userId)
                .First();
            SuggestedReply selReply = downRhymeBefore.SuggestedReplies.First();
            RhymeToSave argToSave = new RhymeToSave
            {
                ReplyId = selReply.Id,
                RhymeId = downRhymeBefore.Id,
                UserKey = userId + correctClientVer
            };
            using (var repo2 = new RhymeRepository(DbInitializer.UserNoDataId + correctClientVer, context))
            {
                repo2.FinishRhyme(argToSave);
                Rhyme downRhymeAfter = context.Rhymes.Find(downRhymeBefore.Id);
                Assert.IsNull(downRhymeAfter.FinishedDate);
            }
            
        }

        [TestMethod]
        public void FinishRhyme_WrongSuggestedReply()
        {
            Rhyme downRhymeBefore = context.Rhymes
                .Include(r => r.SuggestedReplies)
                .Where(r => r.FinishedDate == null
                    && 2* r.Sentences.Count >= Global.MinMsgAmount
                    && r.SuggestedReplies.Count > 0
                    && r.AuthorId == userId)
                .First();
            SuggestedReply selReply = context.SuggestedReplies.Where(sr => sr.RhymeId != downRhymeBefore.Id).First();
            RhymeToSave argToSave = new RhymeToSave
            {
                ReplyId = selReply.Id,
                RhymeId = downRhymeBefore.Id,
                //it is not important in test so give id for possible future changes
                UserKey = userId
            };

            repo.FinishRhyme(argToSave);
            Rhyme downRhymeAfter = context.Rhymes.Find(downRhymeBefore.Id);
            Assert.IsNull(downRhymeAfter.FinishedDate);
        }

        [TestMethod]
        public void FinishRhyme_WrongFinishDate()
        {
            Rhyme downRhymeBefore = context.Rhymes
                .Include(r => r.SuggestedReplies)
                .Where(r => r.FinishedDate != null
                    && 2 * r.Sentences.Count >= Global.MinMsgAmount
                    && r.SuggestedReplies.Count > 0
                    && r.AuthorId == userId)
                .FirstOrDefault();
            Assert.IsNotNull(downRhymeBefore, "downRhymeBefore shouldn't be null");
            DateTime? saveDate = new DateTime?((DateTime)downRhymeBefore.FinishedDate);
            SuggestedReply selReply = context.SuggestedReplies.Where(sr => sr.RhymeId == downRhymeBefore.Id).FirstOrDefault();
            Assert.IsNotNull(selReply, "selReply shouldn't be null");
            RhymeToSave argToSave = new RhymeToSave
            {
                ReplyId = selReply.Id,
                RhymeId = downRhymeBefore.Id,
                //it is not important in test so give id for possible future changes
                UserKey = userId
            };

            repo.FinishRhyme(argToSave);
            Rhyme downRhymeAfter = context.Rhymes.Find(downRhymeBefore.Id);
            Assert.AreEqual(saveDate, downRhymeAfter.FinishedDate);
        }
    }
}
