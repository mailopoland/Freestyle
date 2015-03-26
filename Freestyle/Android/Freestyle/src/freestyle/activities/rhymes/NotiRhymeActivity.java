//Activity get data from notification, base on it create rhyme obj and run correct activ to show it
package freestyle.activities.rhymes;


import android.os.Bundle;
import freestyle.activities.AppMenuActivity;
import freestyle.data.app.NotiEnum;
import freestyle.helpers.outin.downloaders.DownloaderStandard;
import freestyle.repositories.RepoDefaultReq;
import freestyle.services.NotiService;

//activity for author (no like other activities read ready rhymes - only information about toEnd (num of sentences))
public class NotiRhymeActivity  extends AppMenuActivity {

	private int notiId;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Bundle comingData = getIntent().getExtras();
        notiId = comingData.getInt(NotiService.notiIdBundleTag);
        //check with one notification was clicked
        if( notiId == NotiEnum.ACCEPT.getValue()){
        	acceptDown().execute();
        }
        else{
        	respDown().execute();
        }
    }

    private DownloaderStandard acceptDown(){
    	return new RepoDefaultReq(this).acceptUnshown(this);
    }
    
    private DownloaderStandard respDown(){
    	return new RepoDefaultReq(this).respUnshown(this);
    }  
}
