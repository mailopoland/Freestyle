using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfService.DAL.EntityConnections;
using System.Threading.Tasks;
using System.Linq;
using WcfService.Models.Domain;
using System.Threading;
using System.Data;

namespace WcfServiceTests
{
    [TestClass]
    public class HowItWorks : BaseTestCreatorAbstract
    {
        [TestInitialize()]
        public new virtual void Init()
        {
            base.Init();
            base.Close();
            context.Dispose();
        }

        [TestCleanup()]
        public new virtual void Close()
        {
           //base.Close();
           //context.Dispose();
        }

        [TestMethod]
        public void Action_Write_Write_Read_UPDLOCK_CurrencyTest()
        {
            int id1 = -1;
            int id2 = -2;
            int id3 = -3;

            var barrierWithRead = new Barrier(3);
            Action jobWrite1 = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        //using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        //{
                            //var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1' ");
                            var el = query.FirstOrDefault();
                            if (el != null)
                            {
                                barrierWithRead.SignalAndWait();
                                id1 = el.Id;
                                el.Title = "Tytuł1_nowy";
                                el.Sentences.First().ReplyText = "Jakas odp";
                                el.Sentences.Add(new Sentence()
                                {
                                    SentenceText = "Drugie zdanie w tescie utworzone1",
                                    CreatedDate = DateTime.Now
                                });
                                barrierWithRead.SignalAndWait();

                                contextLoc.SaveChanges();

                            }
                            else
                            {
                                barrierWithRead.SignalAndWait();
                                barrierWithRead.SignalAndWait();
                            }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };

            Action jobWrite2 = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        //using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        //{
                            //var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1' ");
                            var el = query.FirstOrDefault();
                            if (el != null)
                            {
                                barrierWithRead.SignalAndWait();
                                id2 = el.Id;
                                el.Title = "DWÓJKA";
                                el.Sentences.First().ReplyText = "odp DWÓJKA";
                                el.Sentences.Add(new Sentence()
                                {
                                    SentenceText = "zdanie DWÓJKA",
                                    CreatedDate = DateTime.Now
                                });
                                barrierWithRead.SignalAndWait();
                                contextLoc.SaveChanges();
                            }
                            else
                            {
                                barrierWithRead.SignalAndWait();
                                barrierWithRead.SignalAndWait();
                            }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    // Assert.Fail("Exception!");
                }
            };
            Action jobRead = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {

                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1'");
                            barrierWithRead.SignalAndWait();
                            //var list = query.ToList();
                            int ammount = query.Count();
                            if (ammount != 0)
                            {
                                // var el = list.First();
                                var el = query.First();
                                barrierWithRead.SignalAndWait();
                                id3 = el.Id;
                                Thread.Sleep(1000);
                                var sentList = el.Sentences.ToList();
                                var sentFirst = sentList.First();
                                Assert.IsTrue(sentList.Count < 2);
                                Assert.IsNull(sentFirst.ReplyText);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Assert.Fail("Exception!");
                }
            };


            var task1 = Task.Factory.StartNew(jobWrite1);
            var task2 = Task.Factory.StartNew(jobWrite2);
            var task3 = Task.Factory.StartNew(jobRead);


            Task.WaitAll(task1, task2);
            Rhyme whatYouAre;
            //Assert.AreEqual(id1, id2);
            //Assert.AreEqual(id2, id3);
            using (var contextLoc = new ApplicationDbContext(nameOfContext))
            {
                whatYouAre = contextLoc.Rhymes.Find(id1);
            }




        }



        [TestMethod]
        public void Action_Write_Write_Read_CurrencyTest()
        {
            int id1 = -1;
            int id2 = -2;

            var barrierWithRead = new Barrier(3);
            Action jobWrite1 = () =>
                {
                    try
                    {
                        using (var contextLoc = new ApplicationDbContext(nameOfContext))
                        {
                            using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                            {
                                var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                                //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1_JEDYNY' ");
                                var el = query.FirstOrDefault();
                                if(el != null)
                                {
                                    barrierWithRead.SignalAndWait();
                                    id1 = el.Id;
                                    el.Title = "Tytuł1_nowy";
                                    el.Sentences.First().ReplyText = "Jakas odp";
                                    el.Sentences.Add(new Sentence()
                                    {
                                        SentenceText = "Drugie zdanie w tescie utworzone1",
                                        CreatedDate = DateTime.Now
                                    });
                                    barrierWithRead.SignalAndWait();

                                    contextLoc.SaveChanges();
                            
                                }
                                else
                                {
                                    barrierWithRead.SignalAndWait();
                                    barrierWithRead.SignalAndWait();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
             //           Assert.Fail("Exception!");
                    }
                };

            Action jobWrite2 = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {
                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1_JEDYNY' ");
                            var el = query.FirstOrDefault();
                            if(el != null)
                            { 
                                barrierWithRead.SignalAndWait();
                                id2 = el.Id;
                                el.Title = "DWÓJKA";
                                el.Sentences.First().ReplyText = "odp DWÓJKA";
                                el.Sentences.Add(new Sentence()
                                {
                                    SentenceText = "zdanie DWÓJKA",
                                    CreatedDate = DateTime.Now
                                });
                                barrierWithRead.SignalAndWait();
                                contextLoc.SaveChanges();
                            }
                            else
                            {
                                barrierWithRead.SignalAndWait();
                                barrierWithRead.SignalAndWait();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                   // Assert.Fail("Exception!");
                }
            };
            Action jobRead = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {

                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1'");
                            barrierWithRead.SignalAndWait();
                            //var list = query.ToList();
                            int ammount = query.Count();
                            if (ammount != 0)
                            {
                                // var el = list.First();
                                var el = query.First();
                                barrierWithRead.SignalAndWait();
                                Thread.Sleep(1000);
                                var sentList = el.Sentences.ToList();
                                var sentFirst = sentList.First();
                                Assert.IsTrue(sentList.Count < 2);
                                Assert.IsNull(sentFirst.ReplyText);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                   // Assert.Fail("Exception!");
                }
            };


            var task1 = Task.Factory.StartNew(jobWrite1);
            var task2 = Task.Factory.StartNew(jobWrite2);
            var task3 = Task.Factory.StartNew(jobRead);


            Task.WaitAll(task1, task2);
            Rhyme whatYouAre;
            Assert.AreEqual(id1, id2);
            using (var contextLoc = new ApplicationDbContext(nameOfContext))
            {
                whatYouAre = contextLoc.Rhymes.Find(id1);
            }
            
            

           
        }


        //test run 2 tasks at the same time, one read data with condition
        //second change these data (and condition for first task
        [TestMethod]
        public void Action_Write_Read_CurrencyTest()
        {
            var barrier = new Barrier(2);
            Action jobWrite = () =>
                {
                    try
                    {
                    //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.RepeatableRead }))
                    //    {
                            //in real word all tasks have their own context
                        using (var contextLoc = new ApplicationDbContext(nameOfContext))
                        {
                           // using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                          //  {

                                    //var query = contextLoc.Database.ExecuteSqlCommand("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1'");
                                    //var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1");
                                    //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1'");

                                    
                                
                                
                                //int ammount = 1;
                                var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                                var el = query.First();
                                el.Title = "Tytuł1_nowy";
                                el.Sentences.First().ReplyText = "Jakas odp";
                                el.Sentences.Add(new Sentence()
                                {
                                    SentenceText = "Drugie zdanie w tescie utworzone",
                                    CreatedDate = DateTime.Now
                                });
                                barrier.SignalAndWait();
                                contextLoc.SaveChanges();
                                //if (ammount > 0)
                                //{
                                    ////commandOrder += "_Change-Title_";
                                    
                                    ////el.Title = "Tytuł1_nowy";
                                    ////contextLoc.SaveChanges();
                                    ////notFind = false;
                                    //tran.Commit();
                                //}
                        }
                    //    }
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail("Exception!");
                    }
                };
            Action jobRead = () =>
                {
                    try
                    {
                        using (var contextLoc = new ApplicationDbContext(nameOfContext))
                        {
                            using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                            {
                              
                                    var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                                //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1'");

                                    //var list = query.ToList();
                                int ammount = query.Count();
                                if (ammount != 0)
                                {                                    
                                   // var el = list.First();
                                    var el = query.First();
                                    barrier.SignalAndWait();
                                    Thread.Sleep(1000);
                                    var sentList = el.Sentences.ToList();
                                    var sentFirst = sentList.First();
                                    Assert.IsTrue(sentList.Count < 2);
                                    Assert.IsNull(sentFirst.ReplyText);
                                }
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail("Exception!");
                    }
                };
            var task1 = Task.Factory.StartNew(jobWrite);
            var task2 = Task.Factory.StartNew(jobRead);
            

            Task.WaitAll(task1, task2);

        }

        [TestMethod]
        public void Action_Read_Write_CurrencyTest()
        {
            var barrier = new Barrier(2);
            Action jobWrite = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1' ");
                        var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                        var el = query.First();
                        el.Title = "Tytuł1_nowy";
                        el.Sentences.First().ReplyText = "Jakas odp";
                        
                        el.Sentences.Add(new Sentence()
                        {
                            SentenceText = "Drugie zdanie w tescie utworzone",
                            CreatedDate = DateTime.Now
                        });
                        barrier.SignalAndWait();
                        barrier.SignalAndWait();
                        contextLoc.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };
            Action jobRead = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {

                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            barrier.SignalAndWait();
                            int ammount = query.Count();
                            barrier.SignalAndWait();
                            if (ammount != 0)
                            {
                                Thread.Sleep(1000);
                                var el = query.First();
                                var sentList = el.Sentences.ToList();
                                var sentFirst = sentList.First();
                                Assert.IsTrue(sentList.Count < 2);
                                Assert.IsNull(sentFirst.ReplyText);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };
            var task1 = Task.Factory.StartNew(jobWrite);
            var task2 = Task.Factory.StartNew(jobRead);


            Task.WaitAll(task1, task2);

        }

        [TestMethod]
        public void Action_Read_WriteOther_CurrencyTest()
        {
            var barrier = new Barrier(2);
            Action jobWrite = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł2" && r.Sentences.Count() == 1);
                        var el = query.First();
                        el.Title = "Tytuł2_nowy";
                        el.Sentences.First().ReplyText = "Jakas odp";

                        el.Sentences.Add(new Sentence()
                        {
                            SentenceText = "Drugie zdanie w tescie utworzone",
                            CreatedDate = DateTime.Now
                        });
                        barrier.SignalAndWait();
                        contextLoc.SaveChanges();
                        barrier.SignalAndWait();
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };
            Action jobRead = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {

                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            barrier.SignalAndWait();
                            int ammount = query.Count();
                            barrier.SignalAndWait();
                            if (ammount != 0)
                            {
                                Thread.Sleep(1000);
                                var el = query.First();
                                var sentList = el.Sentences.ToList();
                                var sentFirst = sentList.First();
                                Assert.IsTrue(sentList.Count < 2);
                                Assert.IsNull(sentFirst.ReplyText);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };
            var task1 = Task.Factory.StartNew(jobWrite);
            var task2 = Task.Factory.StartNew(jobRead);


            Task.WaitAll(task1, task2);

        }

        [TestMethod]
        public void Action_Read_Read_CurrencyTest()
        {
            var barrier = new Barrier(2);
            Action jobRead = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {
                            try
                            {
                                var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1");
                                //var query = contextLoc.Rhymes.SqlQuery("SELECT *  FROM Rhymes WITH (UPDLOCK) WHERE Title = N'Tytuł1'");
                                barrier.SignalAndWait();
                                var el = query.First();
                                //test pass if it doesn't make deadlock
                                barrier.SignalAndWait();
                            }
                            catch (Exception ex)
                            {
                                Assert.Fail("Probably deadlock");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };
            var task1 = Task.Factory.StartNew(jobRead);
            var task2 = Task.Factory.StartNew(jobRead);


            Task.WaitAll(task1, task2);


        }

        //show that threads with read can make writers hungry (but in practise it shouldn't make any problem)
        [TestMethod]
        public void Action_Read_Write_Read_CurrencyTest()
        {
            String commandOrder = String.Empty;
            var barrierMain = new Barrier(3);
            var barrierFor2 = new Barrier(2);
            Action jobWrite = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                        barrierMain.SignalAndWait();
                        Thread.Sleep(1000);
                        var el = query.First();
                        barrierFor2.SignalAndWait();
                           
                        el.Title = "Tytuł1_nowy";
                        
                        el.Sentences.First().ReplyText = "Jakas odp";
                        el.Sentences.Add(new Sentence()
                        {
                            SentenceText = "Drugie zdanie w tescie utworzone",
                            CreatedDate = DateTime.Now
                        });
                        contextLoc.SaveChanges();
                        commandOrder += "_Write_";

                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };
            Action jobRead1 = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {
                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            barrierMain.SignalAndWait();
                            if (query.Count() == 1)
                            {
                                var el = query.First();
                                Assert.AreEqual(1, el.Sentences.Count);
                                Assert.IsNull(el.Sentences.First().ReplyText);
                                commandOrder += "_Read_";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };

            Action jobRead3 = () =>
            {
                try
                {
                    using (var contextLoc = new ApplicationDbContext(nameOfContext))
                    {
                        using (var tran = contextLoc.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                        {
                            var query = contextLoc.Rhymes.Where(r => r.Title == "Tytuł1" && r.Sentences.Count() == 1);
                            barrierMain.SignalAndWait();
                            Thread.Sleep(2000);
                            if (query.Count() > 0)
                            {
                                barrierFor2.SignalAndWait();
                                Thread.Sleep(2000);
                                var el = query.First();
                                Assert.AreEqual(1, el.Sentences.Count);
                                Assert.IsNull(el.Sentences.First().ReplyText);
                                commandOrder += "_Read_";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Exception!");
                }
            };

            var task1 = Task.Factory.StartNew(jobRead1);
            var task2 = Task.Factory.StartNew(jobWrite);
            var task3 = Task.Factory.StartNew(jobRead3);


            Task.WaitAll(task1, task2, task3);

            Assert.IsTrue(commandOrder == "_Read__Read__Write_", "Wrong command order");

        }
    }
}
