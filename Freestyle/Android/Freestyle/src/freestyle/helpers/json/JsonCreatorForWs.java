package freestyle.helpers.json;

import org.json.JSONException;

import freestyle.data.dto.todb.NewRhyme;
import freestyle.data.dto.todb.ReplyToSave;
import freestyle.data.dto.todb.ReqRhyme;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.data.dto.todb.RhymeWithSenToSave;
import freestyle.data.dto.todb.SettingsSetString;
import freestyle.data.dto.todb.UserLogIn;

//decorate object to recognize it by web service
public class JsonCreatorForWs implements IJsonCreatorWsModels{

	private final String argName = "arg";
	private final JsonCreator baseCreator = new JsonCreator();
	
	@Override
	public String createUserLogIn(UserLogIn obj) throws JSONException {
		return decorate(baseCreator.createUserLogIn(obj));
	}
	
	@Override
	public String createSettingsSetString(SettingsSetString obj)
			throws JSONException {
		return decorate(baseCreator.createSettingsSetString(obj));
	}

	@Override
	public String createNewRhyme(NewRhyme obj) throws JSONException {
		return decorate(baseCreator.createNewRhyme(obj));
	}

	@Override
	public String createReplyToSave(ReplyToSave obj) throws JSONException {
		return decorate(baseCreator.createReplyToSave(obj));
	}

	@Override
	public String createReqRhyme(ReqRhyme obj) throws JSONException {
		return decorate(baseCreator.createReqRhyme(obj));
	}

	@Override
	public String createRhymeToSave(RhymeToSave obj) throws JSONException {
		return decorate(baseCreator.createRhymeToSave(obj));
	}

	@Override
	public String createRhymeWithSenToSave(RhymeWithSenToSave obj)
			throws JSONException {
		return decorate(baseCreator.createRhymeWithSenToSave(obj));
	}

	private String decorate(String input) throws JSONException{
		//+3 are that chars: {:}
		StringBuilder strBuilder = new StringBuilder(argName.length() + input.length() + 3);
		strBuilder.append("{\"");
		strBuilder.append(argName);
		strBuilder.append("\":");
		strBuilder.append(input);
		strBuilder.append("}");
		return strBuilder.toString();
	}
}
