using System;
namespace WcfService.Repositories.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using WcfService.DAL.EntityConnections;
    using WcfService.Models.Domain;
    using WcfService.Models.DTO.FromDb.Rhymes.Extend;

    public class RhymeDownloader
    {
        private Rhyme downRhyme;

        private string userId { get; set; }
        private int prevId { get; set; }
        private bool isNext { get; set; }
        private ApplicationDbContext context { get; set; }

        private Expression<Func<Rhyme, bool>> whereNewest { get; set; }
        private Expression<Func<Rhyme, bool>> whereNext { get; set; }
        private Expression<Func<Rhyme, bool>> wherePrev { get; set; }
        //default order (always use if is more than one these is the last one (the least important))
        private Expression<Func<Rhyme, int>> orderBy
        {
            get { return (r => r.Id); }
        }

        private RhymeParser parser
        {
            get { return new RhymeParser(userId, downRhyme, context); }
        }

        public RhymeDownloader(string userId, int prevId, bool isNext, ApplicationDbContext context,
            Expression<Func<Rhyme, bool>> whereNewest,
            Expression<Func<Rhyme, bool>> whereNext,
            Expression<Func<Rhyme, bool>> wherePrev)
        {
            this.userId = userId;
            this.prevId = prevId;
            this.isNext = isNext;
            this.context = context;
            this.whereNewest = whereNewest;
            this.whereNext = whereNext;
            this.wherePrev = wherePrev;
        }

        //these getters download rhyme
        #region Rhyme main getters


        public CompletedWithAuthorViewRhyme DownloadCompletedWithAuthorViewRhyme<T>(CompletedWithAuthorViewRhyme arg, Expression<Func<Rhyme, T>> thenOrderBy)
        {
            CompletedWithAuthorViewRhyme result = null;
            if (init<T>(thenOrderBy))
            {
                result = parser.GetCompletedWithAuthorViewRhyme<T>(arg, thenOrderBy);
            }
            return result;
        }

        public CompletedWithAuthorViewRhyme DownloadCompletedWithAuthorViewRhyme(CompletedWithAuthorViewRhyme arg)
        {
            CompletedWithAuthorViewRhyme result = null;
            if (init())
            {
                result = parser.GetCompletedWithAuthorViewRhyme(arg);
            }
            return result;
        }

        public CompletedViewRhyme DownloadCompletedViewRhyme<T>(CompletedViewRhyme arg, Expression<Func<Rhyme, T>> thenOrderBy)
        {
            CompletedViewRhyme result = null;
            if (init<T>(thenOrderBy))
            {
                result = parser.GetCompletedViewRhyme<T>(arg, thenOrderBy);
            }
            return result;
        }

        public ChooseWriteSentence DownloadChooseWriteSentence(ChooseWriteSentence arg)
        {
            ChooseWriteSentence result = null;
            //download downRhyme
            if (init())
            {
                result = parser.GetChooseWriteSentence(arg);
            }
            return result;
        }

        public ChooseWriteSentence DownloadChooseWriteSentence<T>(ChooseWriteSentence arg, Expression<Func<Rhyme, T>> thenOrderBy)
        {
            ChooseWriteSentence result = null;
            if (init<T>(thenOrderBy))
            {
                result = parser.GetChooseWriteSentence<T>(arg , thenOrderBy);
            }
            return result;
        }

        public WriteRespondUser DownloadWriteRespondUser(WriteRespondUser arg)
        {
            WriteRespondUser result = null;
            if (init())
            {
                result = parser.GetWriteRespondUser(arg);
            }

            return result;
        }

        public WriteRespondUser DownloadWriteRespondUser<T>(WriteRespondUser arg, Expression<Func<Rhyme, T>> thenOrderBy)
        {
            WriteRespondUser result = null;
            if (init<T>(thenOrderBy))
            {
                result = parser.GetWriteRespondUser<T>(arg, thenOrderBy);
            }

            return result;
        }

        #endregion


        

        #region Inits

        //All methods find needed obj (rhyme) and return true, return false if not find
        #region Main Inits

        private bool init()
        {
            bool result = false;
            IQueryable<Rhyme> query = getQueryBase();
            List<Rhyme> beginEls = query.ToList();
            //chooseRhyme put data in downRhyme 
            result = chooseRhyme(beginEls);
            return result;

        }

        //snd condtion
        private bool init<T>(Expression<Func<Rhyme, T>> otherOrderBy)
        {
            bool result = false;
            IQueryable<Rhyme> query = getQueryBase<T>(otherOrderBy);
            List<Rhyme> beginEls = query.ToList();
            //chooseRhyme put data in downRhyme 
            result = chooseRhyme(beginEls);
            return result;
        }

        #endregion

        #region Init helpers

        //return false if no rhyme
        private bool chooseRhyme(List<Rhyme> list)
        {
            bool result = false;

            if (list != null && list.Any())
            {
                result = true;

                downRhyme = list[0];

                if (list.Count > 1)
                {
                    if (downRhyme.Id == prevId)
                        downRhyme = list[1];
                }
            }

            return result;
        }

        private bool chooseRhyme(Rhyme el)
        {
            bool result = false;

            if (el != null)
            {
                result = true;
                downRhyme = el;
            }

            return result;
        }

        private IQueryable<Rhyme> getQueryBase()
        {
            IQueryable<Rhyme> result = null;
            if (prevId < 1)
            {
                result = context.Rhymes
                    .Where(whereNewest)
                    .OrderByDescending(orderBy)
                    .Take(1);
            }
            else
            {
                if (isNext)
                {
                    result = context.Rhymes
                        .Where(whereNext)
                        .OrderBy(orderBy);
                }
                else
                {
                    result = context.Rhymes
                       .Where(wherePrev)
                       .OrderByDescending(orderBy);
                }
                result = result.Take(2);
            }
            return result;
        }

        private IQueryable<Rhyme> getQueryBase<T>(Expression<Func<Rhyme, T>> otherOrderBy)
        {
            IQueryable<Rhyme> result = null;
            if (prevId < 1)
            {
                result = context.Rhymes
                    .Where(whereNewest)
                    .OrderByDescending(otherOrderBy)
                    .ThenByDescending(orderBy)
                    .Take(1);
            }
            else
            {
                if (isNext)
                {
                    result = context.Rhymes
                        .Where(whereNext)
                        .OrderBy(otherOrderBy)
                        .ThenBy(orderBy);
                }
                else
                {
                    result = context.Rhymes
                       .Where(wherePrev)
                       .OrderByDescending(otherOrderBy)
                        //is important to have ordered like in isNext to prevent infinity loop (loading the same obj) in one way
                       .ThenByDescending(orderBy);
                }
                result = result.Take(2);
            }
            return result;
        }

        private IQueryable<Rhyme> getQueryForFirstOrLast()
        {
            IQueryable<Rhyme> result;
            if (isNext)
                result = context.Rhymes
                    .Where(whereNewest)
                    .OrderByDescending(orderBy);
            else
                result = context.Rhymes
                    .Where(whereNewest)
                    .OrderBy(orderBy);
            result = result.Take(1);
            return result;
        }

        //is used when is possible that obj was first/last and it shoud be reload buy it was removed from (not empty) category 
        //it prevent to showing communicate "empty category" in that situation
        private IQueryable<Rhyme> getQueryForFirstOrLast<T>(Expression<Func<Rhyme, T>> otherOrderBy)
        {
            IQueryable<Rhyme> result;
            if (isNext)
                result = context.Rhymes
                    .Where(whereNewest)
                    .OrderByDescending(otherOrderBy)
                    .ThenByDescending(orderBy);
            else
                result = context.Rhymes
                    .Where(whereNewest)
                    .OrderBy(otherOrderBy)
                    .ThenBy(orderBy);
            result = result.Take(1);
            return result;
        }


        #endregion

        #endregion



    }


}