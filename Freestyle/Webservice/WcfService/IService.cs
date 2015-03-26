namespace WcfService
{
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using WcfService.Models.DTO.FromDb.Rhymes;
    using WcfService.Models.DTO.FromDb.Rhymes.Extend;
    using WcfService.Models.DTO.ToDb;
    using WcfService.Models.DTO.Users;

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LogInBaseData FindUser(UserLogIn arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LogInBaseData CreateUser(UserLogIn arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        NewRhymeReturn CreateNewRhyme(NewRhyme arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool AddReply(ReplyToSave arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool FinishRhyme(RhymeToSave arg);
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool AddSentence(RhymeWithSenToSave arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        WriteRespondUser GetRhymeNoAuthorIncomplSortModDate(ReqRhyme arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CompletedViewRhyme GetRhymeAuthorComplSortFinishDate(ReqRhyme arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CompletedWithAuthorViewRhyme GetRhymeNoAuthorComplSortFinishDate(ReqRhyme arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CompletedWithAuthorViewRhyme GetRhymeNoAuthorComplSortVoteValue(ReqRhyme arg);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int ChangeUserLogin(SettingsSetString arg);

        /*  unused change for allow to only show or hide email
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int ChangeUserEmail(SettingsSetString arg); 
        */

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        int MinMsgAmount();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddVote/{userKey}/{rhymeId}/{value}")]
        bool AddVote(string userKey, string rhymeId, string value);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ChangeShowEmail/{userKey}/{show}")]
        bool ChangeShowEmail(string userKey, string show);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ChangeNotiResp/{userKey}/{noNoti}")]
        bool ChangeNotiResp(string userKey, string noNoti);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ChangeNotiAccept/{userKey}/{noNoti}")]
        bool ChangeNotiAccept(string userKey, string noNoti);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ChangeNotiFreq/{userKey}/{value}")]
        bool ChangeNotiFreq(string userKey, string value);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeNoAuthorFavIncomplSortId/{userKey}/{rhymeId}/{isNext}")]
        WriteRespondUser GetRhymeNoAuthorFavIncomplSortId(string userKey, string rhymeId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeNoAuthorFavComplSortId/{userKey}/{rhymeId}/{isNext}")]
        CompletedWithAuthorViewRhyme GetRhymeNoAuthorFavComplSortId(string userKey, string rhymeId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeNoAuthorOwnResIncomplSortId/{userKey}/{rhymeId}/{isNext}")]
        WriteRespondUser GetRhymeNoAuthorOwnResIncomplSortId(string userKey, string rhymeId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeNoAuthorOwnResComplSortId/{userKey}/{rhymeId}/{isNext}")]
        CompletedWithAuthorViewRhyme GetRhymeNoAuthorOwnResComplSortId(string userKey, string rhymeId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeAuthorIncomplSortId/{userKey}/{rhymeId}/{isNext}")]
        ChooseWriteSentence GetRhymeAuthorIncomplSortId(string userKey, string rhymeId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeAuthorIncomplSortSugRepId/{userKey}/{rhymeId}/{sugRepId}/{isNext}")]
        ChooseWriteSentence GetRhymeAuthorIncomplSortSugRepId(string userKey, string rhymeId = Global.DefaultPrevId, string sugRepId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeNoAuthorIncomplSortId/{userKey}/{rhymeId}/{isNext}")]
        WriteRespondUser GetRhymeNoAuthorIncomplSortId(string userKey, string rhymeId = Global.DefaultPrevId, string isNext = Global.DefaultIsNext);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeAuthorIncomplUnshown/{userKey}")]
        ChooseWriteSentence GetRhymeAuthorIncomplUnshown(string userKey);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRhymeNoAuthorUnshown/{userKey}")]
        NoAuthorRhyme GetRhymeNoAuthorUnshown(string userKey);

        [OperationContract]
        [WebInvoke(Method = "GET",  ResponseFormat = WebMessageFormat.Json, UriTemplate = "Noti/{userKey}")]
        Noti Noti(string userKey);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MakeFavorite/{userKey}/{rhymeId}")]
        bool MakeFavorite(string userKey, string rhymeId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MakeNotFavorite/{userKey}/{rhymeId}")]
        bool MakeNotFavorite(string userKey, string rhymeId);


     }
}
