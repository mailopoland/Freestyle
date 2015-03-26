package freestyle.helpers.json;

import java.util.ArrayList;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONObject;

import freestyle.adapters.LogAdp;
import freestyle.data.dto.fromdb.rhymes.NewRhymeReturn;
import freestyle.data.dto.fromdb.rhymes.Noti;
import freestyle.data.dto.fromdb.rhymes.bases.BaseViewRhyme;
import freestyle.data.dto.fromdb.rhymes.bases.IncompletedViewRhyme;
import freestyle.data.dto.fromdb.rhymes.extend.ChooseWriteSentence;
import freestyle.data.dto.fromdb.rhymes.extend.CompletedViewRhyme;
import freestyle.data.dto.fromdb.rhymes.extend.CompletedWithAuthorViewRhyme;
import freestyle.data.dto.fromdb.rhymes.extend.NoAuthorRhyme;
import freestyle.data.dto.fromdb.rhymes.extend.WriteRespondUser;
import freestyle.data.dto.fromdb.rhymes.helpers.ReplyToAccepted;
import freestyle.data.dto.fromdb.rhymes.helpers.SentenceToShow;
import freestyle.data.dto.fromdb.users.LogInBaseData;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.data.dto.todb.RhymeWithSenToSave;

public class JsonConsumer {
	
	//methods witch use helpers and get their exceptions
	public List<ReplyToAccepted> getListReplyToAccepted(String input){
		List<ReplyToAccepted> result;
		try{
			JSONArray specObject = new JSONArray(input);
			int len = specObject.length();
			result = new ArrayList<ReplyToAccepted>(len);
			for(int i = 0; i < len ; ++i)
	        	result.add( getReplyToAcceptedHelper(specObject.getJSONObject(i)) );
		}catch (Exception e) {
			LogAdp.e(this.getClass(),"getRepliesToAccepted(String input)", e.toString(), e);
			result = new ArrayList<ReplyToAccepted>();
		}
		return result;
	}
	
	public UserBaseData getUserBaseData(String input){
		UserBaseData result;
		try{
			JSONObject specObject = new JSONObject(input);
			result = getUserBaseDataHelper(specObject);
		}catch (Exception e) {
			LogAdp.e(this.getClass(),"getUserBaseData(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public LogInBaseData getLoginBaseData(String input){
		LogInBaseData result = new LogInBaseData();
		
		try {
			JSONObject specObject = new JSONObject(input);
		    result.id = specObject.getString("UserKey");
		    result.login = specObject.getString("Login");
		    result.email = specObject.optString("Email");
		    result.noAcceptNoti = specObject.optBoolean("NoAcceptNoti");
		    result.noRespNoti = specObject.optBoolean("NoRespNoti");
		    result.notiFreq = specObject.optInt("NotiFreq");
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getLoginBaseData(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public NewRhymeReturn getNewRhymeReturn(String input){
		
		NewRhymeReturn result = new NewRhymeReturn();
		try {
			JSONObject specObject = new JSONObject(input);
			result.rhymeId = specObject.getInt("RhymeId");
			result.toEnd = specObject.getInt("ToEnd");
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getNewRhymeReturn(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public RhymeToSave getRhymeToSave(String input){
		RhymeToSave result = new RhymeToSave();
		try{
			JSONObject specObject = new JSONObject(input);
			result.replyId = specObject.getInt("ReplyId");
			result.rhymeId = specObject.getInt("RhymeId");
			result.userKey = specObject.getString("UserKey");
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getRhymeToSave(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public RhymeWithSenToSave getRhymeWithSenToSave(String input){
		RhymeToSave helper = getRhymeToSave(input);
		RhymeWithSenToSave result = new RhymeWithSenToSave();
		result.replyId = helper.replyId;
		result.rhymeId = helper.rhymeId;
		result.userKey = helper.userKey;
		try{
			JSONObject specObject = new JSONObject(input);
			result.sentence = specObject.getString("Sentence");
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getRhymeWithSenToSave(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}

	public int getRhymeId(String input){
		int result = -1;
		try{
			JSONObject specObject = new JSONObject(input);
			result = specObject.getInt("RhymeId");
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getRhymeId(String input)", e.toString(), e);
			result = -1;
		}
		return result;
	}
	
	public ChooseWriteSentence getChooseWriteSentence(String input){
		ChooseWriteSentence result = null;
		try{
			JSONObject specObject = new JSONObject(input);
			result = getChooseWriteSentenceHelper(specObject, new ChooseWriteSentence());
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getChooseWriteSentence(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}

	public WriteRespondUser getWriteRespondUser(String input){
		WriteRespondUser result = null;
		try{
			JSONObject specObject = new JSONObject(input);
			result = getWriteRespondUserHelper(specObject, new WriteRespondUser());
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getWriteRespondUser(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}

	public CompletedViewRhyme getCompletedViewRhyme(String input){
		CompletedViewRhyme result = null;
		try{
			JSONObject specObject = new JSONObject(input);
			result = getCompletedViewRhymeHelper(specObject, new CompletedViewRhyme());
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getCompletedViewRhyme(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public CompletedWithAuthorViewRhyme getCompletedWithAuthorViewRhyme(String input){
		CompletedWithAuthorViewRhyme result = null;
		try{
			JSONObject specObject = new JSONObject(input);
			result = getCompletedWithAuthorViewRhymeHelper(specObject, new CompletedWithAuthorViewRhyme());
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getCompletedWithAuthorViewRhyme(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public NoAuthorRhyme getNoAuthorRhyme(String input){
		NoAuthorRhyme result = null;
		try{
			JSONObject specObject = new JSONObject(input);
			result = getNoAuthorRhymeHelper(specObject, new NoAuthorRhyme());
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getNoAuthorRhyme(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	public Noti getNoti(String input){
		Noti result = null;
		try{
			JSONObject specObject = new JSONObject(input);
			result = new Noti();
			if(specObject.isNull("IsNewAccept"))
				result.isNewAccept = null;
			else
				result.isNewAccept = specObject.getBoolean("IsNewAccept");
			if(specObject.isNull("IsNewResp"))
				result.isNewResp = null;
			else
				result.isNewResp = specObject.getBoolean("IsNewResp");
		} catch (Exception e) {
			LogAdp.e(this.getClass(),"getNoti(String input)", e.toString(), e);
			result = null;
		}
		return result;
	}
	
	//private helpers:
	private BaseViewRhyme getBaseRhymeHelper(JSONObject specObject, BaseViewRhyme result) throws Exception{
        result.rhymeId = specObject.getInt("RhymeId");
        result.curValue = specObject.optString("CurValue");
        result.title = specObject.getString("Title");
        result.isFavorited = specObject.getInt("IsFavorited");
        JSONArray senToShowJs = specObject.getJSONArray("SentencesToShow");
        int senToShowJsLenght = senToShowJs.length();
        result.sentencesToShow = new ArrayList<SentenceToShow>(senToShowJsLenght);
        
        int i = 0;
    	for(; i < senToShowJsLenght - 1 ; ++i)
        	result.sentencesToShow.add(getSentenceToShowHelper(senToShowJs.getJSONObject(i)));
    	result.sentencesToShow.add(getLastSentenceToShowHelper(senToShowJs.getJSONObject(i)));
    	
    	return result;
	}
	
	private IncompletedViewRhyme getIncompletedViewRhymeHelper(JSONObject specObject, IncompletedViewRhyme result) throws Exception{
		result = result.getClass().cast( getBaseRhymeHelper(specObject, result) );
		result.toEnd = specObject.getInt("ToEnd");
		return result;
	}
	
	private WriteRespondUser getWriteRespondUserHelper(JSONObject specObject, WriteRespondUser result) throws Exception {
		result = result.getClass().cast(getIncompletedViewRhymeHelper(specObject, result) );
		result.isResponded = specObject.optBoolean("IsResponded");
		result.lastSentId = specObject.getInt("LastSentId");
        JSONObject authorJS = specObject.getJSONObject("Author");
        result.author = getUserBaseDataHelper(authorJS);
		return result;
	}
	
	private ChooseWriteSentence getChooseWriteSentenceHelper(JSONObject specObject, ChooseWriteSentence result) throws Exception{
		result = result.getClass().cast(getIncompletedViewRhymeHelper(specObject, result));
        JSONArray sugRepJs = specObject.optJSONArray("SuggestedReplies");
        if(sugRepJs != null){
        	int sugRepJsLenght = sugRepJs.length();
        	if(sugRepJsLenght > 0){
        	result.suggestedReplies = new ArrayList<ReplyToAccepted>(sugRepJsLenght);
	        	for(int i =0; i < sugRepJsLenght ; ++i)
	            	result.suggestedReplies.add(getReplyToAcceptedHelper(sugRepJs.getJSONObject(i)));
        	}
        }
		return result;
	}

	private CompletedViewRhyme getCompletedViewRhymeHelper(JSONObject specObject, CompletedViewRhyme result) throws Exception{
		result = result.getClass().cast( getBaseRhymeHelper(specObject, result) );
		result.points = specObject.optInt("Points");
		result.canVote = specObject.optBoolean("CanVote");
		return result;
	}
	
	private CompletedWithAuthorViewRhyme getCompletedWithAuthorViewRhymeHelper(JSONObject specObject, CompletedWithAuthorViewRhyme result) throws Exception{
		result = result.getClass().cast( getCompletedViewRhymeHelper(specObject, result));
		result.author = getUserBaseDataHelper(specObject.getJSONObject("Author"));
		return result;
	}
	
	private NoAuthorRhyme getNoAuthorRhymeHelper(JSONObject specObject, NoAuthorRhyme result) throws Exception {
		//method check that arg != null
		result.completed = specObject.optString("Completed");
		//method check that arg != null
		result.incompleted = specObject.optString("Incompleted");
		return result;
	}
	
	private UserBaseData getUserBaseDataHelper(JSONObject userJs) throws Exception{
		UserBaseData result = new UserBaseData();
		result.login = userJs.getString("Login");
		result.email = userJs.optString("Email");
		return result;
	}

	private ReplyToAccepted getReplyToAcceptedHelper(JSONObject input) throws Exception{
		ReplyToAccepted result = new ReplyToAccepted();
		result.replyId = input.getInt("ReplyId");
		result.replyTxt = input.getString("ReplyTxt");
		JSONObject userJs = input.getJSONObject("User");
		result.user = getUserBaseDataHelper(userJs);
		return result;
	}
	
	private SentenceToShow getSentenceToShowHelper(JSONObject input) throws Exception{
		SentenceToShow result = new SentenceToShow();
		result.authorTxt = input.getString("AuthorTxt");
		result.replyTxt = input.getString("ReplyTxt");
		JSONObject userJs = input.getJSONObject("User");
		result.user = getUserBaseDataHelper(userJs);	
		return result;
	}
	//do the same what getSentenceToShow + check before add that last reply != null
	private SentenceToShow getLastSentenceToShowHelper(JSONObject input) throws Exception{
		SentenceToShow result = new SentenceToShow();
		result.authorTxt = input.getString("AuthorTxt");
		result.replyTxt = input.optString("ReplyTxt");
		if(!result.replyTxt.isEmpty()){
			result.replyTxt = result.replyTxt; 
			JSONObject userJs = input.getJSONObject("User");
			result.user = getUserBaseDataHelper(userJs);
		}
		return result;
	}
}