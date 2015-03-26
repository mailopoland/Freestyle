package freestyle.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.data.app.BundleEnum;


public class SentenceCommunicateFragment extends BaseFragment {
		
	private int msgId;
	
	//bundle keys:
	private final String msgIdKeyB = BundleEnum.MSG_ID.toString();
	
	//method for set arguments (before the Fragment is attached to the Activity)
	public void setArguments(int msgId){
		Bundle frInput = new Bundle(1);
		frInput.putInt(msgIdKeyB, msgId);
		setArguments(frInput);
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(frArgs != null){
        	this.msgId = frArgs.getInt(msgIdKeyB);
        }
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        View result = inflater.inflate(R.layout.fragment_sentence_communicate_view, container, false);
        TextView communicate = (TextView) result.findViewById(R.id.view_sentence_communicate_text);
        communicate.setText(msgId);
        return result;
    }
}
