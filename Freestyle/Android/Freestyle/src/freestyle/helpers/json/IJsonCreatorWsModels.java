package freestyle.helpers.json;

import org.json.JSONException;

import freestyle.data.dto.todb.NewRhyme;
import freestyle.data.dto.todb.ReplyToSave;
import freestyle.data.dto.todb.ReqRhyme;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.data.dto.todb.RhymeWithSenToSave;
import freestyle.data.dto.todb.SettingsSetString;
import freestyle.data.dto.todb.UserLogIn;

public interface IJsonCreatorWsModels {

	public abstract String createUserLogIn(UserLogIn obj) throws JSONException;
	
	public abstract String createSettingsSetString(SettingsSetString obj)
			throws JSONException;

	public abstract String createNewRhyme(NewRhyme obj) throws JSONException;

	public abstract String createReplyToSave(ReplyToSave obj)
			throws JSONException;

	public abstract String createReqRhyme(ReqRhyme obj) throws JSONException;

	public abstract String createRhymeToSave(RhymeToSave obj)
			throws JSONException;

	public abstract String createRhymeWithSenToSave(RhymeWithSenToSave obj)
			throws JSONException;
}