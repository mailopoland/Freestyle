package freestyle.repositories;
import freestyle.adapters.BooleanAdapter;
import freestyle.data.dto.fromdb.rhymes.NewRhymeReturn;
import freestyle.data.dto.fromdb.rhymes.Noti;
import freestyle.data.dto.fromdb.users.LogInBaseData;
import freestyle.data.dto.todb.NewRhyme;
import freestyle.data.dto.todb.ReplyToSave;
import freestyle.data.dto.todb.ReqRhyme;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.data.dto.todb.RhymeWithSenToSave;
import freestyle.data.dto.todb.SettingsSetString;
import freestyle.data.dto.todb.UserLogIn;
import freestyle.helpers.json.IJsonCreatorWsModels;
import freestyle.helpers.json.JsonConsumer;
import freestyle.helpers.json.JsonCreatorForWs;
import freestyle.repositories.interfaces.IRhymeRepository;
import freestyle.repositories.interfaces.IUserRepository;

public class RepoHandler {
	
	//Properties:
	private final BooleanAdapter isWrongVer;
	private IUserRepository userRepoObj;
	private IRhymeRepository rhymeRepoObj;
	private JsonConsumer fromJson(){
		return new JsonConsumer();
	}
	private IJsonCreatorWsModels toJson(){
		return new JsonCreatorForWs();
	}
	private IUserRepository userRepo(){
		if(userRepoObj == null)
			userRepoObj = new UserRepository(isWrongVer);
		return userRepoObj;
	}
	private IRhymeRepository rhymeRepo(){
		if(rhymeRepoObj == null)
			rhymeRepoObj = new RhymeRepository(isWrongVer);
		return rhymeRepoObj;
	}
	
	//Constructors:
	public RepoHandler(IUserRepository userRepo, IRhymeRepository rhymeRepo, BooleanAdapter isWrongVer){
		this.userRepoObj = userRepo;
		this.rhymeRepoObj = rhymeRepo; 
		this.isWrongVer = isWrongVer;
	}
	
	public RepoHandler(BooleanAdapter isWrongVer){
		this(null, null, isWrongVer);
	}
	
	//Methods:
	
	//POST:
	public LogInBaseData createUser(UserLogIn user) throws Exception {
	//(String userName, String password, String clientVer) {
		String resultString = null;
		LogInBaseData result = null;
		String input = toJson().createUserLogIn(user);
		resultString = userRepo().createUser(input);
		result = fromJson().getLoginBaseData(resultString);

		return result;
	}

	public LogInBaseData findUser(UserLogIn user) throws Exception{
		String resultString = null;
		LogInBaseData result = null;
		String input = toJson().createUserLogIn(user);
		resultString = userRepo().findUser(input);
		if(resultString != null && !resultString.isEmpty())
			result = fromJson().getLoginBaseData(resultString);
		
		return result;
	}
	
	public NewRhymeReturn createNewRhyme(NewRhyme newRhyme) throws Exception{
		String resultString = null;
		NewRhymeReturn result = null;	
		String jsonObj = toJson().createNewRhyme(newRhyme);
		resultString = rhymeRepo().createNewRhyme(jsonObj);
		result = fromJson().getNewRhymeReturn(resultString);
		
		return result;
	}
	
	public boolean addReply(ReplyToSave reply) throws Exception{
		String jsonObj = toJson().createReplyToSave(reply);
		String resultStr = rhymeRepo().addReply(jsonObj);
		boolean result = Boolean.parseBoolean(resultStr);
		return result;
	}
	
	public boolean finishRhyme(RhymeToSave rhyme) throws Exception{
		String jsonObj = toJson().createRhymeToSave(rhyme);
		String resultStr = rhymeRepo().finishRhyme(jsonObj);
		boolean result = Boolean.parseBoolean(resultStr);
		return result;
	}
	
	public boolean addSentence(RhymeWithSenToSave rhyme) throws Exception{
		String jsonObj = toJson().createRhymeWithSenToSave(rhyme);
		String resultStr = rhymeRepo().addSentence(jsonObj);
		boolean result = Boolean.parseBoolean(resultStr);
		return result;
	}
	
	public String getRhymeAuthorComplSortFinishDate(ReqRhyme reqRhyme) throws Exception{
		String jsonObj = toJson().createReqRhyme(reqRhyme);
		String resultString = rhymeRepo().getRhymeAuthorComplSortFinishDate(jsonObj);		
		return resultString;
	}
	
	public String getRhymeNoAuthorComplSortFinishDate(ReqRhyme reqRhyme) throws Exception{
		String jsonObj = toJson().createReqRhyme(reqRhyme);
		String resultString = rhymeRepo().getRhymeNoAuthorComplSortFinishDate(jsonObj);
		return resultString;
	}
	
	public String getRhymeNoAuthorComplSortVoteValue(ReqRhyme reqRhyme)
			throws Exception {
		String jsonObj = toJson().createReqRhyme(reqRhyme);
		String resultString = rhymeRepo().getRhymeNoAuthorComplSortVoteValue(jsonObj);
		return resultString;
	}
	
	//GET:

	public String  getRhymeNoAuthorFavComplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String resultString = rhymeRepo().getRhymeNoAuthorFavComplSortId(userId, rhymeId, isNext);
		return resultString;
	}

	public String getRhymeNoAuthorFavIncomplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String resultString = rhymeRepo().getRhymeNoAuthorFavIncomplSortId(userId, rhymeId, isNext);
		return resultString;
	}
	
	public String  getRhymeNoAuthorOwnResComplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String resultString = rhymeRepo().getRhymeNoAuthorOwnResComplSortId(userId, rhymeId, isNext);
		return resultString;
	}

	public String getRhymeNoAuthorOwnResIncomplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String resultString = rhymeRepo().getRhymeNoAuthorOwnResIncomplSortId(userId, rhymeId, isNext);
		return resultString;
	}
	
	public String getRhymeNoAuthorIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception{
		String resultString = rhymeRepo().getRhymeNoAuthorIncomplSortId(userId, rhymeId, isNext);
		return resultString;
	}
	
	public String getRhymeNoAuthorIncomplSortModDate(ReqRhyme reqRhyme) throws Exception{
		String jsonObj = toJson().createReqRhyme(reqRhyme);
		String resultString = rhymeRepo().getRhymeNoAuthorIncomplSortModDate(jsonObj);
		return resultString;
	}
	
	public String getRhymeAuthorIncomplUnshown(String userId) throws Exception{
		String result = rhymeRepo().getRhymeAuthorIncomplUnshown(userId);
		return result;
	}
	
	public String getRhymeAuthorIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception{
		String resultString = rhymeRepo().getRhymeAuthorIncomplSortId(userId, rhymeId, isNext);		
		return resultString;
	}
	
	public String getRhymeAuthorIncomplSortSugRepId(String userId, int rhymeId, String curValue, boolean isNext) throws Exception{
		String resultString = rhymeRepo().getRhymeAuthorIncomplSortSugRepId(userId, rhymeId, curValue, isNext);
		return resultString;
	}


    public String getRhymeNoAuthorUnshown(String userId) throws Exception{
    	String resultString = rhymeRepo().getRhymeNoAuthorUnshown(userId);
    	return resultString;
    }
	
	public String minMsgAmount() throws Exception{
		String result = null;
		result = rhymeRepo().minMsgAmount();		
		return result;
	}
	
	public int changeUserLogin(SettingsSetString data) throws Exception{
		int result = 0;
		String jsonObj = toJson().createSettingsSetString(data);
		String stringResult = userRepo().changeUserLogin(jsonObj);
		result = Integer.parseInt(stringResult);
		return result;
	}
	
	public boolean changeShowEmail(String userKey, boolean show) throws Exception{
		boolean result = false;
		String stringResult = userRepo().changeShowEmail(userKey, show);
		result = Boolean.parseBoolean(stringResult);
		return result;
	}
	
	public boolean changeNotiResp(String userKey, boolean noNoti) throws Exception{
		boolean result = false;
		String stringResult = userRepo().changeNotiResp(userKey, noNoti);
		result = Boolean.parseBoolean(stringResult);
		return result;
	}
	
	public boolean changeNotiAccept(String userKey, boolean noNoti) throws Exception{
		boolean result = false;
		String stringResult = userRepo().changeNotiAccept(userKey, noNoti);
		result = Boolean.parseBoolean(stringResult);
		return result;
	}
	
	public boolean changeNotiFreq(String userKey, int value) throws Exception{
		boolean result = false;
		String stringResult = userRepo().changeNotiFreq(userKey, value);
		result = Boolean.parseBoolean(stringResult);
		return result;
	}

    public Noti noti(String userId) throws Exception{
    	Noti result = null;
    	String resultString = rhymeRepo().noti(userId);
    	//json consumer get string (json) for one of result's field
    	result = fromJson().getNoti(resultString);
    	return result;
    }
	
	public boolean addVote(String userKey, int rhymeId, boolean value) throws Exception{
		boolean result = false;
		String valueString = String.valueOf(value);
		String resultString = rhymeRepo().addVote(userKey, rhymeId, valueString);
		result = Boolean.parseBoolean(resultString);
		return result;
	}
	
	public boolean makeFavorite(String userKey, int rhymeId) throws Exception{
		boolean result = false;
		String resultString = rhymeRepo().makeFavorite(userKey, rhymeId);
		result = Boolean.parseBoolean(resultString);
		return result;
	}
	
	public boolean makeNotFavorite(String userKey, int rhymeId) throws Exception{
		boolean result = false;
		String resultString = rhymeRepo().makeNotFavorite(userKey, rhymeId);
		result = Boolean.parseBoolean(resultString);
		return result;
	}
	/* unused change for allow to only show or not
	public int changeUserEmail(SettingsSetString data) throws Exception{
		int result = 0;
		String jsonObj = toJson().create(data);
		String stringResult = userRepo().changeUserEmail(jsonObj);
		result = Integer.parseInt(stringResult);
		return result;
	}
	*/
	
}
