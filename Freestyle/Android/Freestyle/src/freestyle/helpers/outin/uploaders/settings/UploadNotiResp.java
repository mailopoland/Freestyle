package freestyle.helpers.outin.uploaders.settings;

import android.content.Context;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;

public class UploadNotiResp extends UploadSettings  
{
	private boolean noNoti;

	public UploadNotiResp(Context curContext, boolean noNoti) {
		super(curContext);
		this.noNoti = noNoti;
	}
	
	@Override
	protected Integer doInBackground(Void... arg0) {
		boolean receive = false;
		int msgId = R.string.error_settings_change;;

		String userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
		
		try {
			receive = getRepoHandler().changeNotiResp(userKey, noNoti);
		} catch (Exception e) {
			e.printStackTrace();
			receive = false;
		}
		
		if(receive){
				SharedPref.setMain(SharedPref.Keys.NO_RESP_NOTI, noNoti, curContext);
			msgId = 0;
		}
		
		return msgId;
	}  
	
	
}