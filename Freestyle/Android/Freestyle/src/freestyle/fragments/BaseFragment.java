package freestyle.fragments;

import android.app.Fragment;
import android.os.Bundle;


public abstract class BaseFragment extends Fragment{
	
	//static for use in settings fragment
	public final static boolean shouldSetRetainInst = true;

	//protected Activity curActivity;
	protected Bundle frArgs;
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        frArgs = getArguments();
        setRetainInstance(shouldSetRetainInst);
	}
}
