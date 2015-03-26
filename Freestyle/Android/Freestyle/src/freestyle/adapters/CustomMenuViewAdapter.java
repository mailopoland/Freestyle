package freestyle.adapters;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

public class CustomMenuViewAdapter extends ArrayAdapter<String>  {

		private Context context;
		private int layoutResourceId;
		private ArrayList<String> data;
		
		public CustomMenuViewAdapter(Context context, int layoutResourceId, ArrayList<String> data) {
			super(context, layoutResourceId, data); 
			this.layoutResourceId = layoutResourceId; 
			this.context = context; 
			this.data = data; }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
        	View row = convertView; 
        	RecordHolder holder = null; 
        	if (row == null) { 
        		LayoutInflater inflater = ((Activity) context).getLayoutInflater(); 
        		row = inflater.inflate(layoutResourceId, parent, false); 
        		holder = new RecordHolder(); 
        		holder.txtTitle = (TextView) row.findViewById(R.id.item_text); 
        		Typeface font = Typeface.createFromAsset(getContext().getAssets(), "fonts/expresswayrg.ttf");
        		holder.txtTitle.setTypeface(font);
        		
        		//holder.imageItem = (ImageView) row.findViewById(R.id.item_image); 
        		row.setTag(holder); 
        	} 
        	else { 
        		holder = (RecordHolder) 
        		row.getTag(); 
        	} 
        	String item = data.get(position); 
        	holder.txtTitle.setText(item); 
        	return row;

        }

        static class RecordHolder { 
        	TextView txtTitle; 
        	ImageView imageItem; 
        	}
    }