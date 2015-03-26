//Base purpose for this class is parse db object data to dto object data
//but it can during it make simple changes in dto object (for correct display)
//and / or put some information to db (for example mark flag)

namespace WcfService.Repositories.Helpers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.FromDb.Rhymes.Bases;
    using WcfService.Models.DTO.FromDb.Rhymes.Extend;
    using WcfService.Models.DTO.FromDb.Rhymes.Helpers;
    using WcfService.Models.DTO.FromDb.Users;

    public class RhymeParser
    {
        private string userId;
        private Rhyme downRhyme;
        private ApplicationDbContext context;

        public RhymeParser(string userId, Rhyme downRhyme, ApplicationDbContext context)
        {
            this.userId = userId;
            this.downRhyme = downRhyme;
            this.context = context;
        }

        //these getters parse data from dowloaded rhyme to selected object
        #region Body of main getters

        public CompletedWithAuthorViewRhyme GetCompletedWithAuthorViewRhyme<T>(CompletedWithAuthorViewRhyme result, Expression<Func<Rhyme, T>> exprWithField)
        {
            result = GetCompletedWithAuthorViewRhyme(result);
            result.CurValue = exprWithField.Compile()(downRhyme).ToString();
            return result;
        }

        public CompletedWithAuthorViewRhyme GetCompletedWithAuthorViewRhyme(CompletedWithAuthorViewRhyme result)
        {
            result = (CompletedWithAuthorViewRhyme)GetCompletedViewRhyme(result);
            result.Author = new UserBaseData()
            {
                Email = downRhyme.Author.Email,
                Login = downRhyme.Author.ApplicationLogin
            };

            markAsShowedByNoAuthor();

            return result;
        }

        public CompletedViewRhyme GetCompletedViewRhyme<T>(CompletedViewRhyme result, Expression<Func<Rhyme, T>> exprWithField)
        {
            result = GetCompletedViewRhyme(result);
            result.CurValue = exprWithField.Compile()(downRhyme).ToString();
            return result;
        }

        public CompletedViewRhyme GetCompletedViewRhyme(CompletedViewRhyme result)
        {
            result = (CompletedViewRhyme)getBaseRhyme(result);
            result.Points = downRhyme.VotesValue;
            result.CanVote = !downRhyme.Votes.Where(v => v.UserId == userId).Any();
            return result;
        }

        public ChooseWriteSentence GetChooseWriteSentence(ChooseWriteSentence result)
        {
            result = (ChooseWriteSentence)getIncompletedViewRhyme(result);

            result.SuggestedReplies = downRhyme.SuggestedReplies
                .OrderByDescending(sr => sr.Id).Select(sr => new ReplyToAccepted
                {
                    ReplyId = sr.Id,
                    ReplyTxt = sr.ReplyText,
                    User = new UserBaseData
                    {
                        Login = sr.Author.ApplicationLogin,
                        Email = sr.Author.Email
                    }
                }).ToList();

            if (!result.SuggestedReplies.Any())
                result.SuggestedReplies = null;

            markAsShowedByAuthor();

            return result;
        }
        
        public ChooseWriteSentence GetChooseWriteSentence<T>(ChooseWriteSentence result, Expression<Func<Rhyme, T>> exprWithField)
        {
            result = GetChooseWriteSentence(result);
            result.CurValue = exprWithField.Compile()(downRhyme).ToString();
            return result;
        }

        public WriteRespondUser GetWriteRespondUser(WriteRespondUser result)
        {
            result = (WriteRespondUser)getIncompletedViewRhyme(result);

            if (downRhyme.SuggestedReplies.Where(sr => sr.AuthorId == userId).Any())
                result.IsResponded = true;

            //if at the same time author finish rhyme
            //this change send for user (reader) old version
            result.SentencesToShow.Last().ReplyTxt = null;
            result.SentencesToShow.Last().User = null;

            result.LastSentId = downRhyme.Sentences.Last().Id;
            result.Author = new UserBaseData
            {
                Email = downRhyme.Author.Email,
                Login = downRhyme.Author.ApplicationLogin
            };

            markAsShowedByNoAuthor();

            return result;
        }

        public WriteRespondUser GetWriteRespondUser<T>(WriteRespondUser result, Expression<Func<Rhyme, T>> exprWithField)
        {
            result = GetWriteRespondUser(result);
            result.CurValue = exprWithField.Compile()(downRhyme).ToString();
            return result;
        }

        #endregion

        #region Rhyme bases getters

        private BaseViewRhyme getBaseRhyme(BaseViewRhyme result)
        {
            if (downRhyme == null)
                throw new Exception("RhymeGetter's Initator didn't run or it's false result was ignored");

            result.Title = downRhyme.Title;
            result.RhymeId = downRhyme.Id;
            //download to prevent change (these collection can be use in other methods)
            downRhyme.Sentences = downRhyme.Sentences.OrderBy(s => s.Id).ToList();
            result.SentencesToShow = downRhyme.Sentences
                .Select(sen => new SentenceToShow
                {
                    AuthorTxt = sen.SentenceText,
                    ReplyTxt = sen.ReplyText,
                    User = new UserBaseData(sen.AuthorReply)
                }).ToList();
            if (downRhyme.AuthorId == userId)
                result.IsFavorited = 0;
            // if is not fav rhyme for this user
            else if (!downRhyme.FavoritedUsers.Any(fu => fu.UserId == userId))
                result.IsFavorited = 1;
            else
                result.IsFavorited = 2;
            return result;
        }

        private IncompletedViewRhyme getIncompletedViewRhyme(IncompletedViewRhyme result)
        {
            result = (IncompletedViewRhyme)getBaseRhyme(result);
            //always -1 because it is fobbriden to save uncompleted rhyme without started sentence
            result.ToEnd = RhymeRepository.GetToEnd(result.SentencesToShow.Count * 2 - 1);
            return result;
        }

        #endregion

        #region flag showed markers

        private void markAsShowedByAuthor()
        {
            if (downRhyme != null)
            {
                downRhyme.Showed = true;
                context.SaveChanges();
            }
        }

        private void markAsShowedByNoAuthor()
        {
            if (downRhyme != null)
            {
                downRhyme.Sentences.ToList().Where(s => s.AuthorReplyId == userId).ToList().ForEach(s => s.Showed = true);
                context.SaveChanges();
            }
        }

        #endregion
    }
}