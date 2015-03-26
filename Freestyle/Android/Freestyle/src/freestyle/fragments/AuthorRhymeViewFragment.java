package freestyle.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.data.app.BundleEnum;


public class AuthorRhymeViewFragment extends BaseFragment {	
	private String text;

	//bundle keys
	private final String sentenceKeyB = BundleEnum.SENTENCE.toString();
	
	//method for set arguments (before the Fragment is attached to the Activity)
	public void setArguments(String sentence){
		Bundle frInput = new Bundle(1);
		frInput.putString(sentenceKeyB, sentence);
		this.setArguments(frInput);
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(frArgs != null){
        	this.text = frArgs.getString(sentenceKeyB) + getActivity().getString(R.string.three_dots);
        }
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        // Inflate the layout for this fragment
    	View result = inflater.inflate(R.layout.fragment_author_rhyme_view, container, false);
    	
    	TextView textView = (TextView) result.findViewById(R.id.view_author_sentence_text);
    	textView.setText(text);
    	
        return result; 
    }
}
