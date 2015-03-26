package freestyle.helpers.outin.downloaders;

import com.mailoskyteam.freestyle.R;

import android.content.Context;
import android.view.Gravity;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

//DownloaderStandard plus display message about sorting after correct downloading
public abstract class DownloaderStandardSortMsg extends DownloaderStandard{

	private int sortMethodId;
	
	public DownloaderStandardSortMsg(Context curContext, Class<?> newActivity, int sortMethodId) {
		super(curContext, newActivity);
		this.sortMethodId = sortMethodId;
	}

	@Override  
    protected void doIfSuccessed()  
    {  
		super.doIfSuccessed();
		displayToast();
    }	
	
	protected void setSortMethodId(int value){
		sortMethodId = value;
	}
	
	protected void displayToast(){
		String sortTxt = curContext.getString(R.string.sort_by) + curContext.getString(sortMethodId);
		Toast toast = Toast.makeText(curContext, sortTxt, Toast.LENGTH_LONG);
		LinearLayout layout = (LinearLayout) toast.getView();
		if (layout != null && layout.getChildCount() > 0) {
		  TextView tv = (TextView) layout.getChildAt(0);
		  tv.setGravity(Gravity.CENTER_VERTICAL | Gravity.CENTER_HORIZONTAL);
		}
		toast.show();
	}
}
