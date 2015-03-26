package freestyle.activities;

import java.util.ArrayList;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.GridView;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.CustomMenuViewAdapter;
import freestyle.helpers.outin.downloaders.DownloaderStandard;
import freestyle.repositories.RepoDefaultReq;


public class WithAuthorResRhymesMenuActivity extends AppMenuActivity {
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_menu);
        
        ArrayList<String> gridArray = new ArrayList<String>(); 
        gridArray.add( getString(R.string.incomplete));
        gridArray.add( getString(R.string.complete));
        GridView grid = (GridView) findViewById(R.id.grid);
        grid.setAdapter(new CustomMenuViewAdapter(this, R.layout.adapter_row_menu, gridArray));
        grid.setOnItemClickListener(new OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View v, int position,
                    long id) {
            	Context curContext = WithAuthorResRhymesMenuActivity.this;
            	DownloaderStandard downloader = null;
            	
            	switch(position){
            		//with my res incompl
	            	case 0:
	            		downloader = new RepoDefaultReq(curContext).writeRespUserMyRhymes();
	            		break;
	            	//with my res compl
	            	case 1:
	            		downloader = new RepoDefaultReq(curContext).complRhymeNoAuthorMyRhymes();
	            		break;
            	}	
            	if(downloader != null)
            		downloader.execute();
            }
        });
    }
}
