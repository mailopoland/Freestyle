package freestyle.activities;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.GridView;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.CustomMenuViewAdapter;
import freestyle.helpers.outin.downloaders.DownloaderStandard;
import freestyle.repositories.RepoDefaultReq;


public class MainMenuActivity extends AppMenuActivity {
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
    	
    	super.onCreate(savedInstanceState);
   	    setContentView(R.layout.activity_menu);
        ArrayList<String> gridArray = new ArrayList<String>(); 
        gridArray.add( getString(R.string.my_rhymes) );
        gridArray.add( getString(R.string.completed_rhymes) );
        gridArray.add( getString(R.string.top_rhymes) );
        gridArray.add( getString(R.string.favorite_rhymes) );
        gridArray.add( getString(R.string.action_settings) );
        gridArray.add( getString(R.string.action_help) );
        GridView grid = (GridView) findViewById(R.id.grid);
        grid.setAdapter(new CustomMenuViewAdapter(this, R.layout.adapter_row_menu, gridArray));
        grid.setOnItemClickListener(new OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> parent, View v, int position,
                    long id) {
            	Activity curContext = MainMenuActivity.this;
            	Intent intent = null;
            	DownloaderStandard downloader = null;
            	switch(position){
	            	//my rhymes menu
	            	case 0:	
	            		intent = new Intent(curContext, AuthorRhymesMenuActivity.class);
	            		break;
	            	//all completed rhymes
	            	case 1:
	            		downloader = new RepoDefaultReq(curContext).complRhymeNoAuthorStd();
	            		break;
	            	//top rhymes
	            	case 2:
	            		downloader = new RepoDefaultReq(curContext).complRhymeNoAuthorTop();            		
	            		break;
	            	//favorite rhymes
	            	case 3:
	            		intent = new Intent(curContext, FavRhymesMenuActivity.class);
	            		break;
	            	//settings	
	            	case 4:
	            		intent = new Intent(curContext, SettingActivity.class);
	            		break;
	            	//help
	            	case 5:
	            		intent = new Intent(curContext, HelpActivity.class);
	            		break;
            	}
            	if(downloader != null)
            		downloader.execute();
            	else if(intent != null)
            		startActivity(intent);
            	
            }
        });
    }
}
