package freestyle.adapters;
 
import android.annotation.TargetApi;
import android.app.ActionBar;
import android.content.Context;
import android.graphics.Paint;
import android.graphics.Typeface;
import android.os.Build;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.text.Spannable;
import android.text.SpannableString;
import android.text.TextPaint;
import android.text.style.MetricAffectingSpan;
import android.util.LruCache;
import android.widget.TextView;
import com.mailoskyteam.freestyle.R;

public abstract class ActionBarActivityAdapter extends ActionBarActivity {

	TextView prefEditText;
	
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        String myTitle = getResources().getString(R.string.app_name);
        SpannableString s = new SpannableString(myTitle);
        ActionBar actionBar = getActionBar();
        //set action bar title font if can
        if(android.os.Build.VERSION.SDK_INT > 11){
	        s.setSpan(new TypefaceSpan(this, "quigleywiggly.regular.ttf"), 0, s.length(),
	                Spannable.SPAN_EXCLUSIVE_EXCLUSIVE);
        }
        if(actionBar != null)
        	actionBar.setTitle(s);
	}
	
	@TargetApi(Build.VERSION_CODES.HONEYCOMB_MR1) 
	private class TypefaceSpan extends MetricAffectingSpan {

	    private Typeface mTypeface;

	    /**
	     * Load the Typeface and apply to a Spannable.
	     */
	    public TypefaceSpan(Context context, String typefaceName) {
	    	final LruCache<String, Typeface> sTypefaceCache =
		            new LruCache<String, Typeface>(12);
	        mTypeface = sTypefaceCache.get(typefaceName);

	        if (mTypeface == null) {
	            mTypeface = Typeface.createFromAsset(context.getApplicationContext()
	                    .getAssets(), String.format("fonts/%s", typefaceName));

	            // Cache the loaded Typeface
	            sTypefaceCache.put(typefaceName, mTypeface);
	        }
	    }

	    @Override
	    public void updateMeasureState(TextPaint p) {
	        p.setTypeface(mTypeface);

	        // Note: This flag is required for proper typeface rendering
	        p.setFlags(p.getFlags() | Paint.SUBPIXEL_TEXT_FLAG);
	    }

	    @Override
	    public void updateDrawState(TextPaint tp) {
	        tp.setTypeface(mTypeface);

	        // Note: This flag is required for proper typeface rendering
	        tp.setFlags(tp.getFlags() | Paint.SUBPIXEL_TEXT_FLAG);
	    }
	}
    

}
