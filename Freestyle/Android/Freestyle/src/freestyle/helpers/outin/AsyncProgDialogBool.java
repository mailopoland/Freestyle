//use it when you want to display progress dialog 
//and exe task can return true / false ->  success / fail
package freestyle.helpers.outin;

import android.content.Context;

public abstract class AsyncProgDialogBool extends AsyncProgDialogGen<Void, Void, Boolean>{
	
	public AsyncProgDialogBool(Context curContext){
		super(curContext);
	}
	
	protected abstract void doIfSuccessed();
	
	@Override  
    protected void doPostExecute(Boolean downloadError)  
    {  
    	if(downloadError)
    		dialog().problemWithLogin();
    	else
    		doIfSuccessed();
    } 
	
}
