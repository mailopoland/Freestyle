package freestyle.helpers.outin;

import android.content.Context;
import freestyle.adapters.CustomProgressDialog;

public abstract class AsyncProgDialogGen<ARGS, PROG, OUT> extends AsyncGen<ARGS, PROG, OUT> {
	private final CustomProgressDialog progressDialog;
	
	protected abstract void doPostExecute(OUT output);
	protected abstract int getMsgIdForDialog();
	
	public AsyncProgDialogGen(Context curContext){
		super(curContext);
		this.progressDialog = new CustomProgressDialog(curContext);
	}
	
	@Override  
    protected void onPreExecute()  
    {  
    	showProgressDialogUpload();
    }  
	
	@Override  
    protected void replaceOnPostExecute(OUT output)  
    {  
		doPostExecute(output);
		closeProgressDialog();
    }
	
	protected void showProgressDialogUpload(){
		progressDialog.showProgressDialog(getMsgIdForDialog());
	}
	
	protected void closeProgressDialog(){
		progressDialog.closeProgressDialog();
	}

	
}
