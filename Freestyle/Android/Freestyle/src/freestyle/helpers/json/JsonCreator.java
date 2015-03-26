package freestyle.helpers.json;

import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONStringer;

import freestyle.data.dto.fromdb.rhymes.bases.BaseViewRhyme;
import freestyle.data.dto.fromdb.rhymes.bases.IncompletedViewRhyme;
import freestyle.data.dto.fromdb.rhymes.extend.ChooseWriteSentence;
import freestyle.data.dto.fromdb.rhymes.helpers.ReplyToAccepted;
import freestyle.data.dto.fromdb.rhymes.helpers.SentenceToShow;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.data.dto.todb.NewRhyme;
import freestyle.data.dto.todb.ReplyToSave;
import freestyle.data.dto.todb.ReqRhyme;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.data.dto.todb.RhymeWithSenToSave;
import freestyle.data.dto.todb.SettingsSetString;
import freestyle.data.dto.todb.UserLogIn;

public  class JsonCreator implements IJsonCreatorWsModels {

	@Override
	public String createUserLogIn(UserLogIn obj) throws JSONException {
		String result = null;
		if(obj != null)
			result = createUserLogInHelp(obj).toString();
		return result;
	}
	
	public String createUserBaseData(UserBaseData obj) throws JSONException {
		String result = null;
		if(obj != null)
			result = createUserBaseDataHelp(obj).toString();
		return result;
	}
	
	@Override
	public String createSettingsSetString(SettingsSetString obj) throws JSONException{
		JSONStringer jsObj = null;
		if(obj != null )
			jsObj = new JSONStringer()
			.object()
				.key("UserKey").value(obj.userKey)
				.key("Data").value(obj.data)
			.endObject();
		return jsObj.toString();
	}
	
	@Override
	public String createNewRhyme(NewRhyme obj) throws JSONException{
		JSONStringer jsObj = null;
		if(obj != null )
			jsObj = new JSONStringer() 
			.object() 
		        .key("UserKey").value(obj.userKey) 
		        .key("Title").value(obj.title) 
		        .key("Text").value(obj.text) 
			.endObject();
		return jsObj.toString();
	}
	
	
	
	@Override
	public String createReplyToSave(ReplyToSave obj) throws JSONException{
		JSONStringer jsObj = null;
		if(obj != null )
			jsObj =  new JSONStringer()
			.object()
				.key("Text").value(obj.text)
				.key("UserKey").value(obj.userKey)
				.key("RhymeId").value(obj.rhymeId)
				.key("LastSentId").value(obj.lastSentId)
			.endObject();
		return jsObj.toString();
	}
	
	@Override
	public String createReqRhyme(ReqRhyme obj) throws JSONException{
		JSONStringer jsObj = null;
		if(obj != null )
			jsObj =  new JSONStringer()
			.object()
				.key("UserKey").value(obj.userKey)
				.key("RhymeId").value(obj.rhymeId)
				.key("CurValue").value(obj.curValue)
				.key("IsNext").value(obj.isNext)
			.endObject();
		return jsObj.toString();
	}

	@Override
	public String createRhymeToSave(RhymeToSave obj) throws JSONException{
		JSONStringer jsObj = null;
		if(obj != null )
			jsObj =  new JSONStringer()
			.object()
				.key("UserKey").value(obj.userKey)
				.key("RhymeId").value(obj.rhymeId)
				.key("ReplyId").value(obj.replyId)
			.endObject();
		return jsObj.toString();
	}
	
	@Override
	public String createRhymeWithSenToSave(RhymeWithSenToSave obj) throws JSONException{
		JSONStringer jsObj = null;
		if(obj != null )
			jsObj =  new JSONStringer()
			.object()
				.key("UserKey").value(obj.userKey)
				.key("RhymeId").value(obj.rhymeId)
				.key("ReplyId").value(obj.replyId)
				.key("Sentence").value(obj.sentence)
			.endObject();
		return jsObj.toString();
	}
	
	
	//no for ws objects
	public String createChooseWriteSentence(ChooseWriteSentence obj) throws JSONException {
		String result = null;
		if(obj != null)
			result = createChooseWriteSentenceHelp(obj).toString();
		return result;
	}

	public String createListReplyToAccepted(List<ReplyToAccepted> obj) throws JSONException {
		return createListRepliesToAcceptedHelp(obj).toString();
	}
	
	//Base helpers (not check that obj != null)
	
	private JSONObject createUserLogInHelp(UserLogIn obj) throws JSONException{
		JSONObject result = new JSONObject();
		result.put("Login", obj.login);
		result.put("Pass", obj.pass);
		result.put("ClientVer", obj.clientVer);
		return result;
	}
	
	private JSONObject createChooseWriteSentenceHelp(ChooseWriteSentence obj) throws JSONException{
		JSONObject result = createIncompletedViewRhymeHelp(obj);
		result.put("SuggestedReplies", createListRepliesToAcceptedHelp(obj.suggestedReplies));
		return result;
	}
	
	private JSONObject createIncompletedViewRhymeHelp(IncompletedViewRhyme obj) throws JSONException{
		JSONObject result = createBaseViewRhymeHelp(obj);
		result.put("ToEnd", obj.toEnd);
		return result;
	}
	
	private JSONObject createBaseViewRhymeHelp(BaseViewRhyme obj) throws JSONException{
		JSONObject result = new JSONObject();
		result.put("RhymeId", obj.rhymeId);
		result.put("Title", obj.title);
		result.put("CurValue", obj.curValue);
		result.put("IsFavorited", obj.isFavorited);
		result.put("SentencesToShow", createListSentencesToShowHelp(obj.sentencesToShow));

		return result;
	}
	
	//Spec helpers (they check that arg != null)
	
	private JSONObject createUserBaseDataHelp(UserBaseData user) throws JSONException{
		JSONObject result = new JSONObject();
		if(user != null){
			result.put("Login", user.login);
			result.put("Email", user.email);
		}
		return result;
	}
	
	private JSONArray createListRepliesToAcceptedHelp(List<ReplyToAccepted> obj) throws JSONException{
		JSONArray result = new JSONArray();
		if(obj != null){
			for(ReplyToAccepted el : obj)
				result.put(createReplyToAcceptedHelp(el));
		}
		return result;
	}
	
	private JSONObject createReplyToAcceptedHelp(ReplyToAccepted obj) throws JSONException{
		JSONObject result = new JSONObject();
		if(obj != null){
			result.put("ReplyId", obj.replyId);
			result.put("ReplyTxt", obj.replyTxt);
			result.put("User", createUserBaseDataHelp(obj.user));
		}
		return result;
		
	}
	
	private JSONArray createListSentencesToShowHelp(List<SentenceToShow> sentences) throws JSONException{
		JSONArray result = new JSONArray();
		if(sentences != null)
			for(int i = 0 ; i < sentences.size() ; i++)
				result.put(createSentenceToShowHelp( sentences.get(i)) );
		return result;
	}
	
	private JSONObject createSentenceToShowHelp(SentenceToShow sentence) throws JSONException{
		JSONObject result = new JSONObject();
		if(sentence != null){
			result.put("AuthorTxt", sentence.authorTxt);
			result.put("ReplyTxt", sentence.replyTxt);
			result.put("User", createUserBaseDataHelp(sentence.user) );
		}
		return result;
			
	}

}
