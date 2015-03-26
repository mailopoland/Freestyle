package freestyle.helpers.outin.uploaders.settings;

import android.content.Context;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;
import freestyle.data.dto.todb.SettingsSetString;

public class UploadUserName extends UploadSettings  
{
	private String newUserName;

	public UploadUserName(Context curContext, String newUserName) {
		super(curContext);
		this.newUserName = newUserName;
	}
	
	@Override
	protected Integer doInBackground(Void... arg0) {
		int receive = 0;
		int msgId = R.string.error_settings_change;;
		SettingsSetString data = new SettingsSetString();
		data.userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
		data.data = newUserName;
		
		try {
			receive = getRepoHandler().changeUserLogin(data);
		} catch (Exception e) {
			e.printStackTrace();
			receive = 0;
		}
		if(receive == 2){
			msgId = R.string.error_settings_login_exsist;
		}
		else if(receive == 1){
			SharedPref.setMain(SharedPref.Keys.USER_LOGIN, newUserName, curContext);
			msgId = 0;
		}
		return msgId;
	}  
	
	
}