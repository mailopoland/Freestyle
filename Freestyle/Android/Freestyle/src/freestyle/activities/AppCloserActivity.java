package freestyle.activities;

import android.app.Activity;
import android.os.Bundle;

//have special setings in manifest file (no stack with activities)
public class AppCloserActivity extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		this.finish();
	}
}