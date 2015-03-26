package freestyle.activities;

import android.os.Bundle;
import freestyle.adapters.ActionBarActivityAdapter;
import freestyle.fragments.SettingsFragment;
//import freestyle.fragments.SettingsFragment;

public class SettingActivity extends ActionBarActivityAdapter {
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
     	getFragmentManager().beginTransaction().replace(android.R.id.content,
                new SettingsFragment()).commit();
    }

}
