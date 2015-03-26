using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using WcfService;
using WcfService.DAL.EntityConnections;
using WcfService.Models.Domain;

namespace WcfServiceTests
{
    [TestClass]
    public class Currency
    {
        private string contextName
        {
            get
            {
                return "RhymeWcfContextTest";
            }
        }


        [TestMethod]
        public void SelectAndUpdateCurrencyTest()
        {
            var barrier = new Barrier(2);

            Task t1 = new Task(() => selectAndUpdate(ref barrier, 1));
            Task t2 = new Task(() => selectAndUpdate(ref barrier, 2));
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
        }

        [TestMethod]
        public void AddTwoSugResp()
        {
            var barrier = new Barrier(2);

            Task t1 = new Task(() => addSugRep(ref barrier, 0));
            Task t2 = new Task(() => addSugRep(ref barrier, 1));
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
            using (var ctx = new ApplicationDbContext(contextName))
            {
                Rhyme rhyme = ctx.Rhymes.OrderByDescending(r => r.Id).First();
            }
        }

        [TestMethod]
        public void AddSugRespChangeCondition()
        {
            var barrier = new Barrier(2);

            Task t1 = new Task(() => addSugRep(ref barrier, 0));
            Task t2 = new Task(() => addNewSentence(ref barrier, 1));
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
            using (var ctx = new ApplicationDbContext(contextName))
            {
                Rhyme rhyme = ctx.Rhymes.OrderByDescending(r => r.Id).First();
            }
        }

        private void addNewSentence(ref Barrier barrier, int it)
        {
            try
            {

                using (var context = new ApplicationDbContext(contextName))
                {
                    Rhyme toCreate = context.Rhymes.Where(r => r.Id == 2).First();
                    using (var scope = createTransactionScope())
                    //using(var scope = new TransactionScope())
                    {
                        Rhyme oneSentence = context.Rhymes.Where(r => r.Title == "Currency").First();
                        barrier.SignalAndWait();
                        if (oneSentence.Sentences.OrderByDescending(s => s.Id).First().SentenceText == "CurrencySen")
                        {
                            barrier.SignalAndWait();
                            ApplicationUser user = context.Users.OrderBy(u => u.Id).ToList()[it];
                            oneSentence.ModifiedDate = DateTime.Now;
                            oneSentence.Sentences.Add(new Sentence()
                            {
                               SentenceText = "nowe zdanie" + it.ToString(),
                               CreatedDate = DateTime.Now
                            });
                            context.SaveChanges();                    
                            scope.Complete();
                            barrier.SignalAndWait();
 //                           barrier.SignalAndWait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var test = ex;
            }
        }

        private void addSugRep(ref Barrier barrier, int it)
        {
            {
                try
                {

                    using (var context = new ApplicationDbContext(contextName))
                    {
                        Rhyme toCreate = context.Rhymes.Where(r => r.Id == 2).First();
                        using (var scope = createTransactionScope())
                        //using(var scope = new TransactionScope())
                        {
                            Rhyme oneSentence = context.Rhymes.Where(r => r.Title == "Currency").First();
                            barrier.SignalAndWait();
                            if (oneSentence.Sentences.OrderByDescending(s => s.Id).First().SentenceText == "CurrencySen")
                            {
                                barrier.SignalAndWait();
                                ApplicationUser user = context.Users.OrderBy(u => u.Id).ToList()[it];
                                oneSentence.ModifiedDate = DateTime.Now;
                                oneSentence.Sentences.OrderByDescending(s=>s.Id).First().SuggestedReplies.Add(new SuggestedReply()
                                {
                                    Author = user,
                                    ReplyText = "Siema eniu" + it.ToString(),
                                    CreatedDate = DateTime.Now
                                });
                                barrier.SignalAndWait();
                                context.SaveChanges();
                                
                                scope.Complete();
                                
 //                               barrier.SignalAndWait();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var test = ex;
                }
            }
        }

        private void selectAndUpdate(ref Barrier barrier, int it)
        {
            {
                try
                {

                    using (var context = new ApplicationDbContext(contextName))
                    {
                        //Rhyme toCreate = context.Rhymes.Where(r => r.Id == 2).First();
                        //using (var scope = new TransactionScope())
                        //{

                        
                        Rhyme rhyme = context.Rhymes.Where(r => r.Id == 1).First();
                        barrier.SignalAndWait();
                        rhyme.Title = "2dupa" + it.ToString();
                        barrier.SignalAndWait();
                        context.SaveChanges();
                        barrier.SignalAndWait();
                       // scope.Complete();
                        barrier.SignalAndWait();
                        rhyme = context.Rhymes.Where(r => r.Id == 1).First();
                        String title = rhyme.Title;
                       // }
                    }
                }
                catch (Exception ex)
                {
                    var test = ex;
                }
            }
        }

        private TransactionScope createTransactionScope()
         { 
            var transactionOptions = new TransactionOptions 
            { 
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = new TimeSpan(0,0,1,0,0) //assume 1 min is the timeout time
            }; 
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions); 
        } 
    }
}
