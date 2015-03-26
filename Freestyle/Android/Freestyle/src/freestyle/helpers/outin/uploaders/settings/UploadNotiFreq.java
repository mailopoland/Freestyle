package freestyle.helpers.outin.uploaders.settings;

import android.content.Context;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;
import freestyle.services.Schedule;

public class UploadNotiFreq extends UploadSettings{
	
	private final int newValueInt;
	
	public UploadNotiFreq(final Context curContext, final int newValueInt) {
		super(curContext);
		this.newValueInt = newValueInt;
	}

		@Override
		protected Integer doInBackground(Void... arg0) {
			boolean receive = false;
			int msgId = R.string.error_settings_change;;

			String userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
			
			try {
				receive = getRepoHandler().changeNotiFreq(userKey, newValueInt);
			} catch (Exception e) {
				e.printStackTrace();
				receive = false;
			}
			
			if(receive){
 				SharedPref.setMain(SharedPref.Keys.NOTI_FREQ, newValueInt, curContext);
 				Schedule schedule = new Schedule(curContext);
	 			schedule.cancel();
	 			schedule.run();
				msgId = 0;
			}
			
			return msgId;
		}
}