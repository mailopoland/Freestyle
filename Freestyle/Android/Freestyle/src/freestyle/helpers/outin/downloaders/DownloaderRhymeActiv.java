package freestyle.helpers.outin.downloaders;

import android.content.Intent;
import android.widget.Toast;

import com.mailoskyteam.freestyle.R;

import freestyle.activities.rhymes.abstracts.BaseViewRhymeActivity;
import freestyle.helpers.json.JsonConsumer;

//Downloader for activities (auto use it youngest class to get download method and check prev rhyme != downloaded rhyme)
public abstract class DownloaderRhymeActiv extends Downloader{  
	
	private final BaseViewRhymeActivity youngestActiv;
	private final boolean isRightDir;
	
	public DownloaderRhymeActiv(BaseViewRhymeActivity youngestActiv, boolean isRightDir){
		super(youngestActiv);
		this.youngestActiv = youngestActiv;
		this.isRightDir = isRightDir;
		//to not execute many times slide action
		youngestActiv.setHorizontalSlideOff();
	}
	
    @Override  
    protected void doIfSuccessed()  
    {  
    	int newId = new JsonConsumer().getRhymeId(getDownloaded());
    	//if not the same rhyme
    	if(youngestActiv.rhymeId() != newId 
    			//and not enter if downloaded null and is not request for first open category
    			&& !(youngestActiv.rhymeId() != 0 && newId == -1 )){
    		Intent intent = new Intent(curContext, youngestActiv.getClass());
			intent.putExtra(downObjKey, getDownloaded());
			curContext.startActivity(intent);
			if(isRightDir){
				youngestActiv.overridePendingTransition(R.anim.slide_in_left, R.anim.slide_out_right);
			}
			else{
				youngestActiv.overridePendingTransition(R.anim.slide_in_right, R.anim.slide_out_left);
			}
			youngestActiv.finish();
		}
		else{
			int txtId;
			if(isRightDir)
				txtId = R.string.no_prev;
			else
				txtId = R.string.no_next;
			Toast.makeText(curContext, txtId, Toast.LENGTH_SHORT).show();
			//nothing change turn on sliding left/right 
			youngestActiv.setHorizontalSlideOn();
		}
    }  
    
    
}  
