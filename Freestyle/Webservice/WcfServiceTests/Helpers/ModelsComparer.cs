
namespace WcfServiceTests.Helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.FromDb.Rhymes.Bases;
    using WcfService.Models.DTO.FromDb.Rhymes.Extend;
    using WcfService.Models.DTO.FromDb.Rhymes.Helpers;
    using WcfService.Models.DTO.FromDb.Users;
    using WcfService.Repositories;

    internal class ModelsComparer
    {
/*        
        #region special fields

        public static void WrongFinishRhyme(Rhyme downRhymeBefore, Rhyme downRhymeAfter)
        {
            Assert.AreEqual(downRhymeBefore.FinishedDate, downRhymeAfter.FinishedDate, "Finished date is wrong");
            Assert.AreEqual(downRhymeBefore.ModifiedDate, downRhymeAfter.ModifiedDate, "ModifiedDate is wrong");
            Assert.AreEqual(downRhymeBefore.SuggestedReplies.Count(), downRhymeAfter.SuggestedReplies.Count(), "Wrong suggested replies amount");
            downRhymeAfter.Sentences = downRhymeAfter.Sentences.OrderBy(s => s.CreatedDate).ToList();
            Assert.AreEqual(downRhymeBefore.Sentences.Count(), downRhymeAfter.Sentences.Count(), "Wrong sentences amount");
            Assert.AreEqual(downRhymeBefore.Sentences.Last().AuthorReplyId, downRhymeAfter.Sentences.Last().AuthorReplyId, "AuthorReplyId is wrong");
            Assert.AreEqual(downRhymeBefore.Sentences.Last().ReplyText, downRhymeAfter.Sentences.Last().ReplyText, "AuthorReplyId is wrong");
        }

        #endregion
*/
        #region With second CurValue


        public static void CompletedViewRhymeFinishDateComp(Rhyme rhyme, CompletedViewRhyme toComp)
        {
            ModelsComparer.CompletedViewRhymeComp(rhyme, toComp);
            Assert.IsNotNull(rhyme.FinishedDate, "Rhyme FinishDate is null!");
            Assert.AreEqual(rhyme.FinishedDate.ToString(), toComp.CurValue, "FinishDate is wrong");
        }
        #endregion

        #region Checkers which check spec value

        private static void curValFinishDate(Rhyme rhyme, BaseViewRhyme toComp)
        {
            
            Assert.AreEqual(((DateTime)rhyme.FinishedDate).ToString(), toComp.RhymeId.ToString(), "Wrong CurValue (FinishDate");
        }

        #endregion
        #region Not only Base

        public static void ChooseWriteSentenceComp(Rhyme rhyme, ChooseWriteSentence toComp)
        {
            ModelsComparer.IncompletedViewRhymeComp(rhyme, toComp);

            Assert.IsTrue(toComp.SentencesToShow.Count > 0);
            rhyme.Sentences = rhyme.Sentences.OrderBy(s => s.CreatedDate).ToList();

            if (toComp.SuggestedReplies == null)
                Assert.AreEqual(rhyme.SuggestedReplies.Count, 0);
            else
            {
                Assert.AreEqual(rhyme.SuggestedReplies.Count, toComp.SuggestedReplies.Count);
                rhyme.SuggestedReplies = rhyme.SuggestedReplies.OrderByDescending(sr => sr.Id).ToList();

                int i = 0;
                foreach (var sr in rhyme.SuggestedReplies)
                {
                    Assert.AreEqual(sr.ReplyText, toComp.SuggestedReplies[i].ReplyTxt);
                    Assert.AreEqual(sr.Id, toComp.SuggestedReplies[i].ReplyId);
                    ModelsComparer.UserBaseComp(sr.Author, toComp.SuggestedReplies[i].User);
                    i++;
                }
            }
        }

        public static void WriteRespondUserComp(Rhyme rhyme, WriteRespondUser toComp)
        {
            ModelsComparer.IncompletedViewRhymeComp(rhyme, toComp);
            //there is sth stupid in my opinion (it checks fobriden thinks)
            //bool isFavorited = rhyme.FavoritedUsers.Where(fr => fr.UserId == rhyme.AuthorId).Count() > 0;
            //Assert.AreEqual(isFavorited, toComp.IsFavorited, "Wrong isFavorited");
            //bool isResponded = rhyme.SuggestedReplies.Where(sr => sr.AuthorId == rhyme.AuthorId).Count() > 0;
            //Assert.AreEqual(isResponded, toComp.IsResponded, "Wrong isResponded");
            ModelsComparer.UserBaseComp(rhyme.Author, toComp.Author);

        }

        public static void CompletedViewRhymeComp(Rhyme rhyme, CompletedViewRhyme toComp)
        {
            ModelsComparer.BaseViewRhymeComp(rhyme, toComp);

            Assert.AreEqual(rhyme.VotesValue, toComp.Points, "Wrong VotesValue(Points)");
        }

        #endregion

        #region Only base

        private static void UserBaseComp(ApplicationUser appUser, UserBaseData baseUser)
        {
            Assert.AreEqual(appUser.Email, baseUser.Email);
            Assert.IsNotNull(appUser.ApplicationLogin);
            Assert.AreEqual(appUser.ApplicationLogin, baseUser.Login);
        }

        //without BaseViewRhyme.CurValue this field have to compare individual
        private static void BaseViewRhymeComp(Rhyme rhyme, BaseViewRhyme toComp)
        {
            Assert.AreEqual(rhyme.Id, toComp.RhymeId, "Wrong CurValue (Id)");
            Assert.AreEqual(rhyme.Title, toComp.Title, "Wrong Title");
            Assert.IsTrue(rhyme.Sentences.Count > 0, "No sentences");
            Assert.AreEqual(rhyme.Sentences.Count, toComp.SentencesToShow.Count, "Wrong sentences ammount");

            Sentence sen;
            SentenceToShow senToComp;
            for (int i = 0; i < rhyme.Sentences.Count - 1; i++)
            {
                sen = rhyme.Sentences.ElementAt(i);
                senToComp = toComp.SentencesToShow[i];
                Assert.AreEqual(sen.SentenceText, senToComp.AuthorTxt, "Wrong AuthorTxt(SentenceText)");
                Assert.AreEqual(sen.ReplyText, senToComp.ReplyTxt, "Wrong ReplyText");
                ModelsComparer.UserBaseComp(sen.AuthorReply, senToComp.User);
            }
            sen = rhyme.Sentences.Last();
            senToComp = toComp.SentencesToShow.Last();
            Assert.AreEqual(sen.SentenceText, senToComp.AuthorTxt, "Wrong AuthorTxt(SentenceText)");
            if (!String.IsNullOrEmpty(sen.ReplyText))
            {
                Assert.AreEqual(sen.ReplyText, senToComp.ReplyTxt, "Wrong ReplyText");
                ModelsComparer.UserBaseComp(sen.AuthorReply, senToComp.User);
            }
            else
            {
                Assert.IsTrue(String.IsNullOrEmpty(senToComp.ReplyTxt), "Last sentence shouldn't have ReplyTxt");
            }
        }

        private static void IncompletedViewRhymeComp(Rhyme rhyme, IncompletedViewRhyme toComp, bool minusIncomplLastSent = true)
        {
            ModelsComparer.BaseViewRhymeComp(rhyme, toComp);
            int ammountOfSen = rhyme.Sentences.Count * 2;
            if (minusIncomplLastSent)
                ammountOfSen--;
            Assert.AreEqual(RhymeRepository.GetToEnd(ammountOfSen), toComp.ToEnd, "Wrong ToEnd field");
            Assert.AreEqual(1, Math.Abs(int.Parse(toComp.ToEnd) % 2), "ToEnd have to be odd number");
        }

        #endregion







    }
}
