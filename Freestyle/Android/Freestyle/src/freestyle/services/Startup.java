package freestyle.services;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import freestyle.SharedPref;

public class Startup extends BroadcastReceiver {
	@Override
    public void onReceive(Context context, Intent intent) {
        boolean noRespNoti = SharedPref.getMainBool(SharedPref.Keys.NO_RESP_NOTI, context);
        boolean noAcceptNoti = SharedPref.getMainBool(SharedPref.Keys.NO_ACCEPT_NOTI, context);
        if(!(noRespNoti && noAcceptNoti))
        	new Schedule(context).run();
    }
}
