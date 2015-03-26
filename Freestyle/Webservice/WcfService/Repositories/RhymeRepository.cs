namespace WcfService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.FromDb.Rhymes;
    using WcfService.Models.DTO.FromDb.Rhymes.Extend;
    using WcfService.Models.DTO.ToDb;
    using WcfService.Repositories.Helpers;

    //LEGENDS (for straing comments like this example below):
    //UNTESTED (or NOT COMPLETED TESTED) -> is connected with unit tests, behaviors test are always made

    public class RhymeRepository : BaseRepository
    {
        public RhymeRepository(String userKey, ApplicationDbContext context)
            : base(userKey, context)
        { }

        public RhymeRepository(String userKey)
            : base(userKey)
        { }

        private IQueryable<Rhyme> queryNewResp
        {
            get
            {
                // Showed == false ==> r.SuggestedReplies.Any()
                return context.Rhymes
                    .Where(r => r.AuthorId == userId && r.Showed == false && r.FinishedDate == null);
            }
        }

        private IQueryable<Sentence> queryNewAccept
        {
            get
            {
                return context.Sentences
                    .Where(s => s.AuthorReplyId == userId && s.Showed == false);
            }
        }

        //return date of create rhyme
        public NewRhymeReturn CreateNewRhyme(NewRhyme obj)
        {

            NewRhymeReturn result = new NewRhymeReturn();
            DateTime now = CountNow();

            Rhyme newRhymeObj = new Rhyme()
            {
                AuthorId = userId,
                CreatedDate = now,
                ModifiedDate = now,
                Title = obj.Title,
                VotesAmount = 0,
                VotesValue = 0,
                Showed = true,
                Sentences = new List<Sentence>(){
                    new Sentence()
                    {
                        CreatedDate = now,
                        SentenceText = obj.Text,
                    }
                },
            };
            context.Rhymes.Add(newRhymeObj);
            context.SaveChanges();

            //1 is actual ammount of sentences 
            result.ToEnd = GetToEnd(1);
            result.RhymeId = newRhymeObj.Id;

            return result;
        }

        public ChooseWriteSentence GetRhymeAuthorIncomplSortId(int prevId, bool isNext)
        {
            ChooseWriteSentence result = null;

            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //newest
                (r => r.AuthorId == userId && r.FinishedDate == null),
                //next
                (r => r.AuthorId == userId && r.Id >= prevId && r.FinishedDate == null),
                //prev
                (r => r.AuthorId == userId && r.Id <= prevId && r.FinishedDate == null));

            result = getter.DownloadChooseWriteSentence(new ChooseWriteSentence());

            return result;
        }

        //UNTESTED
        //here is problem when many rhymes dont have any suggested replies it gets crazy
        public ChooseWriteSentence GetRhymeAuthorIncomplSortSugRepId(int prevId, int prevSugId, bool isNext)
        {
             ChooseWriteSentence result = null;
             //(r.FinishedDate > prevFinishDate || (r.FinishedDate == prevFinishDate && r.Id >= prevId)))
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //newest
                (r => r.AuthorId == userId && r.FinishedDate == null),
                //next
                (r => r.AuthorId == userId && 
                    (r.SuggestedReplies.Select(sr => sr.Id).DefaultIfEmpty(0).Max() > prevSugId ||
                        (r.SuggestedReplies.Select(sr => sr.Id).DefaultIfEmpty(0).Max() == prevSugId && r.Id >= prevId))
                    && r.FinishedDate == null),
                //prev
                (r => r.AuthorId == userId && 
                    (r.SuggestedReplies.Select(sr => sr.Id).DefaultIfEmpty(0).Max() < prevSugId ||
                        (r.SuggestedReplies.Select(sr => sr.Id).DefaultIfEmpty(0).Max() == prevSugId && r.Id <= prevId))
                    && r.FinishedDate == null));

            result = getter.DownloadChooseWriteSentence<int>(new ChooseWriteSentence(), (r => r.SuggestedReplies.Select(sr => sr.Id).DefaultIfEmpty(0).Max() ));
            return result;
        }

        public WriteRespondUser GetRhymeNoAuthorIncomplSortId(int prevId, bool isNext)
        {
            WriteRespondUser result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate == null
                        && r.AuthorId != userId
                        && r.SuggestedReplies.All(sr => sr.AuthorId != userId),
                //next
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && r.Id >= prevId
                            && r.SuggestedReplies.All(sr => sr.AuthorId != userId),
                //prev
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && r.Id <= prevId
                            && r.SuggestedReplies.All(sr => sr.AuthorId != userId));

            result = getter.DownloadWriteRespondUser(new WriteRespondUser());

            return result;
        }

        //UNTESTED
        public WriteRespondUser GetRhymeNoAuthorIncomplSortModDate(int prevId, DateTime prevModDate, bool isNext)
        {
            WriteRespondUser result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate == null
                        && r.AuthorId != userId
                        && r.SuggestedReplies.All(sr => sr.AuthorId != userId),
                //next
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && (r.ModifiedDate > prevModDate || (r.ModifiedDate == prevModDate && r.Id >= prevId))
                            && r.SuggestedReplies.All(sr => sr.AuthorId != userId),
                //prev
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && (r.ModifiedDate < prevModDate || (r.ModifiedDate == prevModDate && r.Id <= prevId))
                            && r.SuggestedReplies.All(sr => sr.AuthorId != userId));

            result = getter.DownloadWriteRespondUser<DateTime>(new WriteRespondUser(), r => r.ModifiedDate);

            return result;

        }

        public WriteRespondUser GetRhymeNoAuthorFavIncomplSortId(int prevId, bool isNext)
        {
            WriteRespondUser result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate == null
                        && r.AuthorId != userId
                        && r.FavoritedUsers.Any(fu => fu.UserId == userId),
                //next
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && r.Id >= prevId
                            && r.FavoritedUsers.Any(fu => fu.UserId == userId),
                //prev
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && r.Id <= prevId
                            && r.FavoritedUsers.Any(fu => fu.UserId == userId));


            result = getter.DownloadWriteRespondUser(new WriteRespondUser());

            return result;
        }

        public WriteRespondUser GetRhymeNoAuthorOwnResIncomplSortId(int prevId, bool isNext)
        {
            WriteRespondUser result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate == null
                        && r.AuthorId != userId
                        && r.Sentences.Any(s => s.AuthorReplyId == userId),
                //next
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && r.Id >= prevId
                            && r.Sentences.Any(s => s.AuthorReplyId == userId),
                //prev
                r => r.FinishedDate == null
                            && r.AuthorId != userId
                            && r.Id <= prevId
                            && r.Sentences.Any(s => s.AuthorReplyId == userId));


            result = getter.DownloadWriteRespondUser(new WriteRespondUser());

            return result;
        }

        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorFavComplSortId(int prevId, bool isNext)
        {
            CompletedWithAuthorViewRhyme result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate != null
                        && r.AuthorId != userId
                        && r.FavoritedUsers.Any(fu => fu.UserId == userId),
                //next
                r => r.FinishedDate != null
                            && r.AuthorId != userId
                            && r.Id >= prevId
                            && r.FavoritedUsers.Any(fu => fu.UserId == userId),
                //prev
                r => r.FinishedDate != null
                            && r.AuthorId != userId
                            && r.Id <= prevId
                            && r.FavoritedUsers.Any(fu => fu.UserId == userId));


            result = getter.DownloadCompletedWithAuthorViewRhyme(new CompletedWithAuthorViewRhyme());

            return result;
        }

        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorOwnResComplSortId(int prevId, bool isNext)
        {
            CompletedWithAuthorViewRhyme result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate != null
                        && r.AuthorId != userId
                        && r.Sentences.Any(s => s.AuthorReplyId == userId),
                //next
                r => r.FinishedDate != null
                            && r.AuthorId != userId
                            && r.Id >= prevId
                            && r.Sentences.Any(s => s.AuthorReplyId == userId),
                //prev
                r => r.FinishedDate != null
                            && r.AuthorId != userId
                            && r.Id <= prevId
                            && r.Sentences.Any(s => s.AuthorReplyId == userId));


            result = getter.DownloadCompletedWithAuthorViewRhyme(new CompletedWithAuthorViewRhyme());

            return result;
        }

        public CompletedViewRhyme GetRhymeAuthorComplSortFinishDate(int prevId, DateTime? prevFinishDate, bool isNext)
        {
            CompletedViewRhyme result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate != null
                        && r.AuthorId == userId,
                //next
                  r => ((r.FinishedDate != null)
                            && (r.FinishedDate > prevFinishDate || (r.FinishedDate == prevFinishDate && r.Id >= prevId)))
                            && r.AuthorId == userId,
                //prev
                r => ((r.FinishedDate != null)
                            && (r.FinishedDate < prevFinishDate || (r.FinishedDate == prevFinishDate && r.Id <= prevId)))
                            && r.AuthorId == userId);

            result = getter.DownloadCompletedViewRhyme<DateTime?>(new CompletedViewRhyme(), (r => r.FinishedDate));

            return result;
        }
        //UNTESTED
        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorComplSortFinishDate(int prevId, DateTime? prevFinishDate, bool isNext)
        {
            CompletedWithAuthorViewRhyme result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate != null,
                //next
               r => ((r.FinishedDate != null)
                   && (r.FinishedDate > prevFinishDate || (r.FinishedDate == prevFinishDate && r.Id >= prevId))),
                //prev
               r => ((r.FinishedDate != null)
                   && (r.FinishedDate < prevFinishDate || (r.FinishedDate == prevFinishDate && r.Id <= prevId)))
                );

            result = getter.DownloadCompletedWithAuthorViewRhyme<DateTime?>(new CompletedWithAuthorViewRhyme(), (r => r.FinishedDate));

            return result;
        }

        //UNTESTED
        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorComplSortVoteValue(int prevId, int prevPoints, bool isNext)
        {
            CompletedWithAuthorViewRhyme result = null;
            RhymeDownloader getter = new RhymeDownloader(userId, prevId, isNext, context,
                //the newest
                r => r.FinishedDate != null,
                //next
               r => ((r.FinishedDate != null)
                   && (r.VotesValue > prevPoints || (r.VotesValue == prevPoints && r.Id >= prevId))),
                //prev
               r => ((r.FinishedDate != null)
                   && (r.VotesValue < prevPoints || (r.VotesValue == prevPoints && r.Id <= prevId)))
                );

            result = getter.DownloadCompletedWithAuthorViewRhyme<int>(new CompletedWithAuthorViewRhyme(), (r => r.VotesValue));

            return result;
        }

        //UNTESTED
        public ChooseWriteSentence GetRhymeAuthorIncomplUnshown()
        {
            ChooseWriteSentence result = null;
            var rhyme = queryNewResp
                .OrderByDescending(r => r.SuggestedReplies.Max(sr => sr.Id))
                .FirstOrDefault();

            // all rhymes was showed
            if (rhyme == null)
            {
                rhyme = context.Rhymes
                    .Where(r => r.AuthorId == userId && r.FinishedDate == null && r.SuggestedReplies.Any())
                    .OrderByDescending(r => r.SuggestedReplies.Max(sr => sr.Id))
                    .FirstOrDefault();
            }

            if (rhyme != null)
            {
                result = new RhymeParser(userId, rhyme, context).GetChooseWriteSentence<int>(new ChooseWriteSentence(), (r => r.SuggestedReplies.Select(sr => sr.Id).DefaultIfEmpty(0).Max()));
            }
            //when actual all user rhymes no have any respond (possible)
            else
                result = null;

            return result;
        }

        //UNTESTED
        public NoAuthorRhyme GetRhymeNoAuthorUnshown()
        {
            NoAuthorRhyme result = new NoAuthorRhyme();

            var sent = queryNewAccept
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();
            // all rhymes was showed
            if (sent == null)
            {
                sent = context.Sentences
                    .Where(s => s.AuthorReplyId == userId)
                    .OrderByDescending(s => s.Id)
                    .FirstOrDefault();
            }

            if (sent != null)
            {
                RhymeParser parser = new RhymeParser(userId, sent.Rhyme, context);
                if (sent.Rhyme.FinishedDate != null)
                {
                    result.Completed = parser.GetCompletedWithAuthorViewRhyme<DateTime?>(new CompletedWithAuthorViewRhyme(), (r => r.FinishedDate));
                }
                else
                {
                    result.Incompleted = parser.GetWriteRespondUser<DateTime>(new WriteRespondUser(), r => r.ModifiedDate);
                }
            }
            else
            {
                //never should exe this section (because notification shouldn't show if any user resp is accepted)
                Elmah.ErrorSignal.FromCurrentContext().Raise
                    (new NotImplementedException("In RhymeRepository during GetRhymeNoAuthorUnshown() ws go here, place where never should be able to go"));
                result.Completed = null;
                result.Incompleted = null;
            }

            return result;
        }

        //UNTESTED
        public Noti Noti()
        {
            Noti result = new Noti();
            ApplicationUser user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user != null)
            {
                if (!user.NoAcceptNoti)
                    result.IsNewAccept = isNewAccept();
                else
                    result.IsNewAccept = null;
                if (!user.NoRespNoti)
                    result.IsNewResp = isNewResp();
                else
                    result.IsNewAccept = null;
            }
            return result;
        }

        //UNTESTED
        public bool AddReply(ReplyToSave reply)
        {
            bool result = false;

            Rhyme rhyme = context.Rhymes.Where(r => r.Id == reply.RhymeId).FirstOrDefault();
            if(rhyme != null 
                //user can't reply on his own rhyme
                && rhyme.AuthorId != userId
                //user can't reply on the same sentence more than two times
                && !rhyme.SuggestedReplies.Where(sr => sr.AuthorId == userId).Any()
                //user should reply on last sentence
                && rhyme.Sentences.OrderByDescending(s => s.Id).First().Id == reply.LastSentId)
            {
                rhyme.SuggestedReplies.Add(
                    new SuggestedReply
                    {
                        CreatedDate = CountNow(),
                        AuthorId = userId,
                        ReplyText = reply.Text,
                    }
                );
                rhyme.Showed = false;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        //UNTESTED
        public bool FinishRhyme(RhymeToSave arg)
        {
            bool result = false;
            Rhyme toFinishRhyme = context.Rhymes
                .Where(r => r.Id == arg.RhymeId &&
                    r.FinishedDate == null &&
                    r.AuthorId == userId &&
                    2 * r.Sentences.Count >= Global.MinMsgAmount).FirstOrDefault();
            if (toFinishRhyme != null)
            {
                result = finishIt(toFinishRhyme, arg);
            }
            return result;
        }

        //UNTESTED
        public bool AddSentence(RhymeWithSenToSave arg)
        {
            bool result = false;
            Rhyme toSaveRhyme = context.Rhymes
                .Where(r => r.Id == arg.RhymeId &&
                    r.FinishedDate == null &&
                    r.AuthorId == userId).FirstOrDefault();
            if (toSaveRhyme != null)
            {
                if (toSaveRhyme.Sentences.Count >= Global.MaxMsgAmount)
                    result = finishIt(toSaveRhyme, arg);
                else
                {
                    SuggestedReply sugRep = toSaveRhyme.SuggestedReplies.Where(sr => sr.Id == arg.ReplyId).FirstOrDefault();
                    if (sugRep != null)
                    {
                        Sentence lastSen = toSaveRhyme.Sentences.OrderBy(s => s.CreatedDate).Last();
                        lastSen.ReplyText = sugRep.ReplyText;
                        lastSen.AuthorReplyId = sugRep.AuthorId;
                        toSaveRhyme.ModifiedDate = CountNow();
                        toSaveRhyme.Sentences.Add(new Sentence()
                        {
                            CreatedDate = toSaveRhyme.ModifiedDate,
                            SentenceText = arg.Sentence
                        });
                        toSaveRhyme.Showed = true;
                        context.SuggestedReplies.RemoveRange(toSaveRhyme.SuggestedReplies);
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            return result;
        }

        //UNTESTED
        public bool AddVote(int objRhymeId, bool objValue)
        {
            bool result = false;
            Rhyme rhyme = null;
            rhyme = context.Rhymes.Where(r => r.Id == objRhymeId && r.Votes.All(v => v.UserId != userId)).FirstOrDefault();
            if (rhyme != null)
            {
                int value;
                if (objValue)
                    value = 1;
                else
                    value = -1;

                rhyme.Votes.Add(new Vote
                {
                    UserId = userId,
                    Value = objValue
                });

                rhyme.VotesValue += value;
                rhyme.VotesAmount++;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        //in SaveReply remeber to save if sentences.Count() == toEnd

        //when metod retunr x > 0: x is number of minimum sentences needed to possibility to end rhyme from now
        //when method return x < 0: (-1)*x is number of maximum sentences allowed to add more for now (minimum is achieved)
        //always x != 0 because only client can have even amount of sentences to end (on ws always should be odd)
        //sentencesAmmount is summation of author sentences and users replies
        //static for allow to use without create unneeded context
        public static String GetToEnd(int sentencesAmmount)
        {
            int result = -1;

            int min = Global.MinMsgAmount;
            int max = Global.MaxMsgAmount;

            if (sentencesAmmount < min)
                result = min - sentencesAmmount;
            else if (sentencesAmmount < max)
                result = sentencesAmmount - max;
            //situation when max was reduced and current rhyme have too much sentences 
            else
                result = -1;

            return result.ToString();
        }

        //UNTESTED
        public bool MakeFavorite(int rhymeId)
        {
            bool result = false;
            //if user is not author of rhyme
            if (context.Rhymes.Any(r => r.Id == rhymeId && r.AuthorId != userId))
            {
                context.FavoritedRhymes.Add(new FavoritedRhyme()
                {
                    RhymeId = rhymeId,
                    UserId = userId
                });
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        //UNTESTED
        public bool MakeNotFavorite(int rhymeId)
        {
            bool result = false;
            var toDel = context.FavoritedRhymes.Where(fr => fr.RhymeId == rhymeId && fr.UserId == userId).FirstOrDefault();
            if (toDel != null)
            {
                context.FavoritedRhymes.Remove(toDel);
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        private bool isNewResp()
        {
            return queryNewResp.Any();
        }

        private bool isNewAccept()
        {
            return queryNewAccept.Any();
        }

        private bool finishIt(Rhyme toFinishRhyme, RhymeToSave arg)
        {
            bool result = false;
            SuggestedReply sugRep = toFinishRhyme.SuggestedReplies.Where(sr => sr.Id == arg.ReplyId).FirstOrDefault();
            if (sugRep != null)
            {
                Sentence lastSen = toFinishRhyme.Sentences.OrderBy(s => s.CreatedDate).Last();
                lastSen.ReplyText = sugRep.ReplyText;
                lastSen.AuthorReplyId = sugRep.AuthorId;
                toFinishRhyme.FinishedDate = CountNow();
                toFinishRhyme.ModifiedDate = (DateTime)toFinishRhyme.FinishedDate;
                toFinishRhyme.Showed = true;
                context.SuggestedReplies.RemoveRange(toFinishRhyme.SuggestedReplies);
                context.SaveChanges();
                result = true;
            }
            return result;
        }



    }
}