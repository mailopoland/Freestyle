package freestyle.helpers.outin.uploaders;
//req to server can return everything
import android.content.Context;
import freestyle.helpers.outin.AsyncProgDialogGen;

public abstract class UploaderExt<ARGS, PROG, OUT> extends AsyncProgDialogGen<ARGS, PROG, OUT>{

	@Override
	protected int getMsgIdForDialog(){
		return UploaderGlobal.msgId;
	}
	
	public UploaderExt(Context curContext){
		super(curContext);
	}

}
