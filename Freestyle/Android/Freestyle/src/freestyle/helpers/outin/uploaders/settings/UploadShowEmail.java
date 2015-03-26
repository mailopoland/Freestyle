package freestyle.helpers.outin.uploaders.settings;

import android.content.Context;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;

public class UploadShowEmail extends UploadSettings  
{
	private final boolean show;
	private final String email;
	
	public UploadShowEmail(Context curContext, boolean show, String email) {
		super(curContext);
		this.show = show;
		this.email = email;
	}
	
	@Override
	protected Integer doInBackground(Void... arg0) {
		boolean receive = false;
		int msgId = R.string.error_settings_change;;

		String userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
		
		try {
			receive = getRepoHandler().changeShowEmail(userKey, show);
		} catch (Exception e) {
			e.printStackTrace();
			receive = false;
		}
		
		if(receive){
			if(show)
				SharedPref.setMain(SharedPref.Keys.USER_EMAIL, email, curContext);
			else
				SharedPref.setMain(SharedPref.Keys.USER_EMAIL, "", curContext);
			msgId = 0;
		}
		
		return msgId;
	}  
	
	
}
