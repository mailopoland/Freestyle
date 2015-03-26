package freestyle.helpers.outin.uploaders.settings;

import android.content.Context;
import freestyle.helpers.outin.uploaders.UploaderExt;

public abstract class UploadSettings extends UploaderExt<Void, Void, Integer>{
	public UploadSettings(Context curContext){
		super(curContext);
	}
	
	@Override 
	protected void doPostExecute(Integer msgId){
		if(msgId != 0)
			//this dialog restart activity (to reload correct values in view)
			dialog().customErrorSettings(msgId);
	}
}
