package freestyle.services;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import freestyle.Global;
import freestyle.SharedPref;

public class Schedule {
	final Context curContext;
	final int interval;
	final int requestCode = 1;
	
	AlarmManager manager;
	PendingIntent pendingIntent;

	public Schedule(Context curContext){
		this.curContext = curContext;
		int intervalFromSet = SharedPref.getMainInt(SharedPref.Keys.NOTI_FREQ, curContext);
		if(intervalFromSet < 1){
			intervalFromSet = Global.defaultNotiFreq;
		}
		//interval has amount of minutes
		interval = intervalFromSet * 60 * 1000;
		manager = (AlarmManager) curContext.getSystemService(Context.ALARM_SERVICE);
	}
	
	public void run(){
	    if(!isSetNotiRunner()){
	    	pendingIntent = getNotiRunnerPendingIntent();
	    	manager.setRepeating(AlarmManager.RTC, System.currentTimeMillis() + interval, interval , pendingIntent);
	    }
	}
	
	public void cancel(){
		pendingIntent = getNotiRunnerPendingIntent();
		manager.cancel(pendingIntent);
		pendingIntent.cancel();
	}
	
	private PendingIntent getNotiRunnerPendingIntent(){
		return PendingIntent.getBroadcast(curContext, requestCode, getNotiRunnerIntent(), PendingIntent.FLAG_CANCEL_CURRENT);
	}
	
	private boolean isSetNotiRunner(){
		return PendingIntent.getBroadcast(curContext, requestCode, getNotiRunnerIntent(), PendingIntent.FLAG_NO_CREATE) != null;
	}
	
	private Intent getNotiRunnerIntent(){
		return new Intent(curContext, NotiRunner.class);
	}

}
