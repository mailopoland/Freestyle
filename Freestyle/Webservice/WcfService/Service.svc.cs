namespace WcfService
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using WcfService.Models.DTO.FromDb.Rhymes;
    using WcfService.Models.DTO.FromDb.Rhymes.Extend;
    using WcfService.Models.DTO.ToDb;
    using WcfService.Models.DTO.Users;
    using WcfService.Models.Exceptions;
    using WcfService.Repositories;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IService
    {
        //userName is android google account name
        //return null if not exsist
        public LogInBaseData FindUser(UserLogIn arg)
        {
            return exe<LogInBaseData> (() =>
                {            
                    using (var repo = new UserRepository(arg.ClientVer))
                    {
                        return repo.FindUser(arg.Login, arg.Pass);
                    }
                });
        }

        //return key after creating
        public LogInBaseData CreateUser(UserLogIn arg)
        {
            return exe<LogInBaseData> (() =>
            {
                using (var repo = new UserRepository(arg.ClientVer))
                {
                    return repo.CreateUser(arg.Login, arg.Pass);
                }
            });
        }

        public int ChangeUserLogin(SettingsSetString arg)
        {
            return exe<int>(() =>
            {
                using (var repo = new UserRepository(arg.UserKey))
                {
                    return repo.ChangeUserLogin(arg.Data);
                }
            });
        }

        //return min msg amount for new rhyme
        public int MinMsgAmount()
        {
            int curAmount = 1;
            return Global.MinMsgAmount - curAmount;
        }

        public NewRhymeReturn CreateNewRhyme(NewRhyme arg)
        {
            return exe<NewRhymeReturn>(() =>
                {
                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.CreateNewRhyme(arg);
                    }
                });
        }

        public bool AddReply(ReplyToSave arg)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.AddReply(arg);
                    }
                });
        }

        public bool FinishRhyme(RhymeToSave arg)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.FinishRhyme(arg);
                    }
                });
        }

        public bool AddSentence(RhymeWithSenToSave arg)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.AddSentence(arg);
                    }
                });
        }

        public bool ChangeShowEmail(string userKey, string show)
        {
            return exe<bool>(() =>
            {
                using (var repo = new UserRepository(userKey))
                {
                    bool showBool = Boolean.Parse(show);
                    return repo.ChangeShowEmail(showBool);
                }
            });
        }

        public bool ChangeNotiResp(string userKey, string noNoti)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new UserRepository(userKey))
                    {
                        bool noNotiBool = Boolean.Parse(noNoti);
                        return repo.ChangeNotiResp(noNotiBool);
                    }
                });
        }

        public bool ChangeNotiAccept(string userKey, string noNoti)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new UserRepository(userKey))
                    {
                        bool noNotiBool = Boolean.Parse(noNoti);
                        return repo.ChangeNotiAccept(noNotiBool);
                    }
                });
        }

        public bool ChangeNotiFreq(string userKey, string value)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new UserRepository(userKey))
                    {
                        int valueInt = int.Parse(value);
                        return repo.ChangeNotiFreq(valueInt);
                    }
                });
        }

        public CompletedViewRhyme GetRhymeAuthorComplSortFinishDate(ReqRhyme arg)
        {
            return exe<CompletedViewRhyme>(() =>
                {
                    int prevIdInt = arg.RhymeId;
                    DateTime finishDate = DateTime.Parse(arg.CurValue);

                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.GetRhymeAuthorComplSortFinishDate(prevIdInt, finishDate, arg.IsNext);
                    }
                });
        }

        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorComplSortFinishDate(ReqRhyme arg)
        {

            return exe<CompletedWithAuthorViewRhyme>(() =>
                {
                    int prevIdInt = arg.RhymeId;
                    DateTime finishDate = DateTime.Parse(arg.CurValue);

                    DateTime? t = finishDate;

                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.GetRhymeNoAuthorComplSortFinishDate(prevIdInt, finishDate, arg.IsNext);
                    }
                });
        }

        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorComplSortVoteValue(ReqRhyme arg)
        {
            return exe<CompletedWithAuthorViewRhyme>(() =>
                {
                    int prevIdInt = arg.RhymeId;
                    int voteVal = int.Parse(arg.CurValue);
                    using (var repo = new RhymeRepository(arg.UserKey))
                    {
                        return repo.GetRhymeNoAuthorComplSortVoteValue(prevIdInt, voteVal, arg.IsNext);
                    }
                });
        }


        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorFavComplSortId(string userKey, string rhymeId, string isNext)
        {
            return exe<CompletedWithAuthorViewRhyme>(() =>
                {
                    bool isNextBool = Boolean.Parse(isNext);
                    int prevIdInt = int.Parse(rhymeId);
                    using (var repo = new RhymeRepository(userKey))
                    {
                        return repo.GetRhymeNoAuthorFavComplSortId(prevIdInt, isNextBool);
                    }
                });
        }


        public CompletedWithAuthorViewRhyme GetRhymeNoAuthorOwnResComplSortId(string userKey, string rhymeId, string isNext)
        {
            return exe<CompletedWithAuthorViewRhyme>( () =>
                {
                    bool isNextBool = Boolean.Parse(isNext);
                    int prevIdInt = int.Parse(rhymeId);
                    using (var repo = new RhymeRepository(userKey))
                    {
                        return repo.GetRhymeNoAuthorOwnResComplSortId(prevIdInt, isNextBool);
                    }
                });
        }

        public WriteRespondUser GetRhymeNoAuthorFavIncomplSortId(string userKey, string rhymeId, string isNext)
        {
            return exe<WriteRespondUser>(() =>
            {
                bool isNextBool = Boolean.Parse(isNext);
                int prevIdInt = int.Parse(rhymeId);
                using (var repo = new RhymeRepository(userKey))
                {
                    return repo.GetRhymeNoAuthorFavIncomplSortId(prevIdInt, isNextBool);
                }
            });
        }


        public WriteRespondUser GetRhymeNoAuthorOwnResIncomplSortId(string userKey, string rhymeId, string isNext)
        {
            return exe<WriteRespondUser>(() =>
            {
                bool isNextBool = Boolean.Parse(isNext);
                int prevIdInt = int.Parse(rhymeId);
                using (var repo = new RhymeRepository(userKey))
                {
                    return repo.GetRhymeNoAuthorOwnResIncomplSortId(prevIdInt, isNextBool);
                }
            });
        }

        public WriteRespondUser GetRhymeNoAuthorIncomplSortId(string userKey, string rhymeId, string isNext)
        {
            return exe<WriteRespondUser>(() =>
            {
                bool isNextBool = Boolean.Parse(isNext);
                int prevIdInt = int.Parse(rhymeId);
                using (var repo = new RhymeRepository(userKey))
                {
                    return repo.GetRhymeNoAuthorIncomplSortId(prevIdInt, isNextBool);
                }
            });
        }

        public WriteRespondUser GetRhymeNoAuthorIncomplSortModDate(ReqRhyme arg)
        {
            return exe<WriteRespondUser>(() =>
            {
                DateTime prevModDate = DateTime.Parse(arg.CurValue);
                using (var repo = new RhymeRepository(arg.UserKey))
                {
                    return repo.GetRhymeNoAuthorIncomplSortModDate(arg.RhymeId, prevModDate, arg.IsNext);
                }
            });
        }

        public ChooseWriteSentence GetRhymeAuthorIncomplSortSugRepId(string userKey, string rhymeId, string sugRepId, string isNext)
        {
            return exe<ChooseWriteSentence>(() =>
            {
                bool isNextBool = Boolean.Parse(isNext);
                int prevIdInt = int.Parse(rhymeId);
                int prevSugIdint = int.Parse(sugRepId);
                using (var repo = new RhymeRepository(userKey))
                {
                    return repo.GetRhymeAuthorIncomplSortSugRepId(prevIdInt, prevSugIdint, isNextBool);
                }
            });
        }

        public ChooseWriteSentence GetRhymeAuthorIncomplSortId(string userKey, string rhymeId, string isNext)
        {
            return exe<ChooseWriteSentence>( () =>
                {
                    bool isNextBool = Boolean.Parse(isNext);
                    int prevIdInt = int.Parse(rhymeId);
                    using (var repo = new RhymeRepository(userKey))
                    {
                        return repo.GetRhymeAuthorIncomplSortId(prevIdInt, isNextBool);
                    }
                });
        }



        public ChooseWriteSentence GetRhymeAuthorIncomplUnshown(string userKey)
        {
            return exe<ChooseWriteSentence>(()=>
                {
                    using (var repo = new RhymeRepository(userKey))
                    {
                        return repo.GetRhymeAuthorIncomplUnshown();
                    }
                });            
        }

        public NoAuthorRhyme GetRhymeNoAuthorUnshown(string userKey)
        {
            return exe<NoAuthorRhyme>(()=>
                {
                    using (var repo = new RhymeRepository(userKey))
                    {
                        return repo.GetRhymeNoAuthorUnshown();
                    }
                });
        }

        public Noti Noti(string userKey)
        {
            return exe<Noti>(()=>
                {
                    using (var repo = new RhymeRepository(userKey))
                    {
                        return repo.Noti();
                    }
                });
        }

        public bool AddVote(string userKey, string rhymeId, string value)
        {
            return exe<bool>(()=>
                {
                    using (var repo = new RhymeRepository(userKey))
                    {
                        bool valueBool = bool.Parse(value);
                        int rhymeIdInt = int.Parse(rhymeId);
                        return repo.AddVote(rhymeIdInt, valueBool);
                    }
                });
        }

        public bool MakeFavorite(string userKey, string rhymeId)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new RhymeRepository(userKey))
                    {
                        int rhymeIdInt = int.Parse(rhymeId);
                        return repo.MakeFavorite(rhymeIdInt);
                    }
                });
        }

        public bool MakeNotFavorite(string userKey, string rhymeId)
        {
            return exe<bool>(() =>
                {
                    using (var repo = new RhymeRepository(userKey))
                    {
                        int rhymeIdInt = int.Parse(rhymeId);
                        return repo.MakeNotFavorite(rhymeIdInt);
                    }
                });
        }

          //executor, handle error in one place
        private T exe<T>(Func<T> func, T errorResult = default(T))
        {
            T result;
            try
            {
                result = func();
            }
            catch (ClientVersionException)
            {
                throw new WebFaultException<string>(Global.TooOldClientTxt, Global.TooOldClientStatCode);
            }
            catch (Exception ex)
            {
                result = errorResult;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return result;
        }

        /* unused change for allow to only show or hide email
        public int ChangeUserEmail(SettingsSetString arg)
        {
            return exe<int>(() =>
                {
                    using (var repo = new UserRepository(arg.UserKey))
                    {
                        return repo.ChangeUserEmail(arg.Data);
                    }
                });
        }
        */
        
    }
}
