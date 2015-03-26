package freestyle.services;


import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.os.IBinder;
import android.support.v4.app.TaskStackBuilder;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;
import freestyle.activities.rhymes.NotiRhymeActivity;
import freestyle.adapters.LogAdp;
import freestyle.data.app.NotiEnum;
import freestyle.data.dto.fromdb.rhymes.Noti;
import freestyle.helpers.outin.AsyncGen;
import freestyle.repositories.RepoHandler;

public class NotiService extends Service{

	//for backstack in intent (after click on noti)
	final private Context curContext;
	final public static String notiIdBundleTag = "notiId"; 
	public NotiService(){
		this.curContext = this;
	}
	
	//check settings and run selected checker
	public void start(){
		boolean isMasterSyncEnabled = ContentResolver.getMasterSyncAutomatically();
		if(isMasterSyncEnabled)
		{
			boolean noRespNoti = SharedPref.getMainBool(SharedPref.Keys.NO_RESP_NOTI, curContext);
			boolean noAcceptNoti = SharedPref.getMainBool(SharedPref.Keys.NO_ACCEPT_NOTI, curContext);
			
			if( !noRespNoti || !noAcceptNoti ){
				Noti noti = null;
				try {
					noti = new DownloadNoti(curContext).execute().get();
				} 
				catch (Exception e) {
					LogAdp.e(getClass(), "start()", "error during downloading noti", e);
					noti = null;
					return;
				}
				if(noti != null){
					
					if(noti.isNewResp == null){
						//if user turn off respNoti on other device sync this setting
						if(!noRespNoti){
							SharedPref.setMain(SharedPref.Keys.NO_RESP_NOTI, true, curContext);
							noRespNoti = true;
						}
					}
					else if(!noRespNoti && noti.isNewResp){
						startNotify(R.string.resp_noti_title, R.string.resp_noti_description, NotiEnum.RESPOND);
					}
					if(noti.isNewAccept == null){
						//if user turn off respAccept on other device sync this setting
						if(!noAcceptNoti){
							noRespNoti = true;
							SharedPref.setMain(SharedPref.Keys.NO_ACCEPT_NOTI, true, curContext);
						}
					}
					else if(!noAcceptNoti && noti.isNewAccept){
						startNotify(R.string.accept_noti_title, R.string.accept_noti_description, NotiEnum.ACCEPT);
					}
					//if sync settings turn off  getting noti
					if(noRespNoti && noAcceptNoti)
						new Schedule(curContext).cancel();
				}
			}
		}
	}
	


	@Override
	public IBinder onBind(Intent intent) {
		return null;
	}
	
	@Override
    public int onStartCommand(Intent intent, int flags, int startId) {
		this.start();
        return START_NOT_STICKY;
    }
	
	private Notification.Builder createNotify(int titleId, int textId){
		String title = curContext.getString(titleId);
		String text = curContext.getString(textId);
		Bitmap largeIcon = (((BitmapDrawable)curContext.getResources().getDrawable(R.drawable.ic_launcher)).getBitmap());
		return new Notification.Builder(this)
				.setLargeIcon(largeIcon)
				.setSmallIcon(R.drawable.ic_small_noti)
				.setContentTitle(title)
			    .setContentText(text);
	}
	
	private void showNotify(Notification.Builder notiBuilder, NotiEnum notiId){
		NotificationManager notificationManager = (NotificationManager) 
				  getSystemService(NOTIFICATION_SERVICE);
		
		
		@SuppressWarnings("deprecation")
		Notification noti = notiBuilder.getNotification();
		
		notificationManager.notify(notiId.getValue(),noti);
	}
	
	private void startNotify(int titleId, int textId, NotiEnum notiId){
		
		
		Notification.Builder notiBuilder = createNotify(titleId, textId);
		
		//BEGIN add intent on click
		TaskStackBuilder stackBuilder = TaskStackBuilder.create(curContext);
		Intent notiIntent = new Intent(curContext, NotiRhymeActivity.class);
		notiIntent.putExtra(notiIdBundleTag, notiId.getValue());
		stackBuilder.addParentStack(NotiRhymeActivity.class);
		stackBuilder.addNextIntent(notiIntent);
		PendingIntent resultPendingIntent =
		        stackBuilder.getPendingIntent(
		            notiId.getValue(),
		            PendingIntent.FLAG_CANCEL_CURRENT
		        );
		notiBuilder.setContentIntent(resultPendingIntent);
		notiBuilder.setAutoCancel(true);
		//END add intent on click
		
		showNotify(notiBuilder, notiId);

	}
	
	private class DownloadNoti extends AsyncGen<Void, Void, Noti>{
		
		Context curContext;		
		
		public DownloadNoti(Context curContext){
			super(curContext);
			this.curContext = curContext;
		}
		
		@Override
		protected Noti doInBackground(Void... arg0) {
			Noti result = null;
			String userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
			RepoHandler service = getRepoHandler();
			try{
				result = service.noti(userKey);
			}
			catch(Exception ex){
				result = null;
			}
			//if user client is not actual turn off notification
			if(getIsWrongVerValue()){
				result = null;
			}
				
			return result;
		}
		
		@Override  
	    protected void wrongVersionAction()  
	    {  
			new Schedule(curContext).cancel();
			showNeedUpdateNoti();
	    }
		


		@Override
		protected void replaceOnPostExecute(Noti output) {
			//no need to do
		}
		
		private void showNeedUpdateNoti(){
			Notification.Builder notiBuilder = createNotify(R.string.need_new_ver_title, R.string.need_new_ver_text);
			showNotify(notiBuilder, NotiEnum.NEED_NEW_VER);
			//todo do that after click is open play store
		}
	}
	
}