package freestyle.helpers.outin.downloaders;

import android.content.Context;
import android.content.Intent;

//Standard downloader (it required manual set downloading method and final activity class)
public abstract class DownloaderStandard extends Downloader{  
	
	private Class<?> newActivity;
	
	public DownloaderStandard(Context curContext, Class<?> newActivity){
		super(curContext);
		this.newActivity = newActivity;
	}

	@Override  
    protected void doIfSuccessed()  
    {  
		doIfSuccessedHelper(getDownloaded(), newActivity);
    }	
	
	protected void doIfSuccessedHelper(String downloaded, Class<?> activity){
		Intent intent = new Intent(curContext, activity);
		intent.putExtra(downObjKey, downloaded); 
		curContext.startActivity(intent);
	}
}  
