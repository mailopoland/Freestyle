package freestyle.helpers.outin.downloaders;

import android.content.Context;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.LogAdp;
import freestyle.data.app.BundleEnum;
import freestyle.helpers.outin.AsyncProgDialogBool;

public abstract class Downloader extends AsyncProgDialogBool{  
	
	public static String downObjKey = BundleEnum.RHYME_OBJ.toString();
	private String downloaded;
	
	public Downloader(Context curContext){
		super(curContext);
	}

	protected abstract String downloadRhyme() throws Exception;
	
	@Override
	protected Boolean doInBackground(Void... params) {
		boolean errorDownload = true; 
   		
    	try {
			downloaded = downloadRhyme();
			errorDownload = false;
    	} catch (Exception e) {
			LogAdp.e(Downloader.class, "doInBackground", e.toString(), e);
			errorDownload = true;
		}

        return errorDownload;  
	}
    
	@Override
	protected int getMsgIdForDialog(){
		return R.string.download_and_load;
	}
    
    
    protected String getDownloaded(){
    	return downloaded;
    }
}  
