package freestyle.helpers.outin.uploaders;
//req to server return only bool
import android.content.Context;
import freestyle.helpers.outin.AsyncProgDialogBool;

public abstract class UploaderSimple extends AsyncProgDialogBool{

	public UploaderSimple(Context curContext) {
		super(curContext);
	}
	
	@Override
	protected int getMsgIdForDialog(){
		return UploaderGlobal.msgId;
	}
}
