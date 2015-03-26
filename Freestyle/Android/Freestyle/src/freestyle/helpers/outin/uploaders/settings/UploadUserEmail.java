package freestyle.helpers.outin.uploaders.settings;

import android.content.Context;

import com.example.freestyle.R;

import freestyle.SharedPref;
import freestyle.data.dto.todb.SettingsSetString;

public class UploadUserEmail extends UploadSettings 
{
	private String newUserEmail;

	public UploadUserEmail(Context curContext, String newUserEmail) {
		super(curContext);
		this.newUserEmail = newUserEmail;
	}

	@Override
	protected Integer doInBackground(Void... arg0) {
		int receive = 0;
		int msgId = R.string.error_settings_change;
		SettingsSetString data = new SettingsSetString();
		data.userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
		data.data = newUserEmail;
		try {
			receive = getRepoHandler().changeUserEmail(data);
		} catch (Exception e) {
			e.printStackTrace();
			receive = 0;
		}
		if(receive == 2){
			msgId = R.string.error_settings_login_exsist;
		}
		else if(receive == 1){
			msgId = 0;
			SharedPref.setMain(SharedPref.Keys.USER_EMAIL, newUserEmail, curContext);
		}
		return msgId;
	}  
}