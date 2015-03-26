package freestyle.helpers.outin.uploaders;

import android.content.Context;
import android.widget.Toast;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;
import freestyle.adapters.LogAdp;
import freestyle.helpers.outin.AsyncGen;

public abstract class UploaderToast extends AsyncGen<Void, Void, Boolean> {

	private final int startMsgId; 
	private final int endMsgSucId;
	private final int endMsgFailId;
	
	public UploaderToast(Context curContext, int startMsgId, int endMsgSucId, int endMsgFailId) {
		super(curContext);
		this.startMsgId = startMsgId;
		this.endMsgSucId = endMsgSucId;
		this.endMsgFailId = endMsgFailId;
	}

	protected abstract Boolean uploadMethod(String userKey) throws Exception ;
	
	@Override  
    protected void onPreExecute()  
    {  
    	Toast.makeText(curContext, startMsg(startMsgId), Toast.LENGTH_SHORT).show();
    }  
	
	@Override  
    protected void replaceOnPostExecute(Boolean result)  
    {  
		if(result)
			Toast.makeText(curContext, endMsgSucId, Toast.LENGTH_SHORT).show();
		else
			Toast.makeText(curContext, failMsg(endMsgFailId), Toast.LENGTH_LONG).show();
    }
	
	
	@Override
	protected Boolean doInBackground(Void... params) {
		boolean result = false;
		String userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
		try {
			result = uploadMethod(userKey);
		} catch (Exception e) {
			result = false;
			LogAdp.e(getClass(), "doInBackground", "during upload", e);
		}
		return result;
	}

	private String startMsg(int msgId){
		return curContext.getString(msgId) + curContext.getString(R.string.three_dots);
	}
	
	private String failMsg(int msgId){
		StringBuilder builder = new StringBuilder(curContext.getString(msgId));
		builder.append("\n");
		builder.append(curContext.getString(R.string.try_again));
		return builder.toString();
	}
}
