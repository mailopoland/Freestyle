package freestyle.activities;

import android.content.Intent;
import android.view.Menu;
import android.view.MenuItem;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.ActionBarActivityAdapter;

public abstract class AppMenuActivity extends ActionBarActivityAdapter{

	@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu, menu);
        return true;
    }
 
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
    	Intent intent = null;
    	switch (item.getItemId()) {
 
	        case R.id.action_settings:
	        	intent = new Intent(this, SettingActivity.class);
	            break;
	        case R.id.action_help:
	        	intent = new Intent(this, HelpActivity.class);
	        	break;
        }
    	if(intent != null)
    		startActivity(intent);
        return true;
    }
}
