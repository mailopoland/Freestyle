package freestyle.adapters;

import android.app.ProgressDialog;
import android.content.Context;

import com.mailoskyteam.freestyle.R;

public class CustomProgressDialog {
	private final Context curContext;
	private ProgressDialog progressDialog;
	
	public CustomProgressDialog(Context curContext){
		this.curContext = curContext;
	}
	
	public void showProgressDialog(int msgId){
		//to prevent show more than one progress dialog
    	closeProgressDialog();
		progressDialog = new ProgressDialog(curContext, ProgressDialog.THEME_HOLO_DARK);
		progressDialog.setProgressStyle(ProgressDialog.THEME_HOLO_DARK);
		progressDialog.setTitle(R.string.please_waiting);
		progressDialog.setMessage(curContext.getText(msgId));
		progressDialog.setCanceledOnTouchOutside(false);
		//back button not hide progress dialog
		progressDialog.setCancelable(false);
		progressDialog.show();
	}
	
	public void closeProgressDialog(){
		if(progressDialog != null && progressDialog.isShowing())
			progressDialog.dismiss();
	}
}
