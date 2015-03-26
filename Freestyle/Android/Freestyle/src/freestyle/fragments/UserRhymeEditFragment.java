package freestyle.fragments;

import android.app.Activity;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;

import com.mailoskyteam.freestyle.R;

import freestyle.data.app.BundleEnum;
import freestyle.data.dto.todb.ReplyToSave;
import freestyle.helpers.Validator;
import freestyle.helpers.outin.uploaders.UploaderToast;


public class UserRhymeEditFragment extends BaseFragment {
	private EditText sentenceEditText;
	
	//data from input bundle
	private int lastSentId;
	private int rhymeId;
	private int sentencesContainer;
	
	//bundle keys
	private final String lastSentIdKeyB = BundleEnum.LAST_SENT_ID.toString();
	private final String rhymeIdKeyB = BundleEnum.RHYME_ID.toString();
	private final String sentencesContainerKeyB = BundleEnum.SENTENCES_CONTAINER.toString();
	
	public void setArguments(int lastSentId, int rhymeId, int sentencesContainer) {
		Bundle frInput = new Bundle(3);
    	frInput.putInt(BundleEnum.LAST_SENT_ID.toString(), lastSentId);
    	frInput.putInt(BundleEnum.RHYME_ID.toString(), rhymeId);
    	frInput.putInt(BundleEnum.SENTENCES_CONTAINER.toString(), sentencesContainer);
		this.setArguments(frInput);
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(frArgs != null){
        	this.lastSentId = frArgs.getInt(lastSentIdKeyB);
        	this.rhymeId = frArgs.getInt(rhymeIdKeyB);
        	this.sentencesContainer = frArgs.getInt(sentencesContainerKeyB);
        }
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
    	
        View view = inflater.inflate(R.layout.fragment_user_rhyme_edit, container, false);       
        Button acceptBtn = (Button) view.findViewById(R.id.accept_button);
        this.sentenceEditText = (EditText) view.findViewById(R.id.edit_user_sentence_text);
        
        if(acceptBtn != null)
        	acceptBtn.setOnClickListener( genListener() );
        
        return view;
    }
    
    public View.OnClickListener genListener(){
		View.OnClickListener result = null;
		
		if(sentenceEditText != null)
			result = new View.OnClickListener() {
				private String sentence;
				@Override
				public void onClick(View v) {
		    		boolean isVaild = true;
		    		Activity curActivity = UserRhymeEditFragment.this.getActivity();
		        	sentence = sentenceEditText.getText().toString();
		        	Validator validator = new Validator(curActivity);
		        	if(!validator.isValidShortText(sentence)){
		        		sentenceEditText.setError(curActivity.getString(R.string.min_lenght) + validator.isValidShortTextMinAmount());
		        		isVaild = false;
		        	}
		        	if(isVaild){
		        		correctDataClickAction(curActivity);
		        	}
		    	}
				private void correctDataClickAction(Activity curActivity) {
					final FragmentManager fragmentManager = curActivity.getFragmentManager();
					ReplyCommunicateFragment communicateFr = new ReplyCommunicateFragment();
					communicateFr.setArguments(R.string.reply_waiting);
					FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
					fragmentTransaction.remove(UserRhymeEditFragment.this);
					fragmentTransaction.add(sentencesContainer, communicateFr);
					fragmentTransaction.commit();
					
					//fix bug - hide virtual keypad after add respond (delete edit field)
					InputMethodManager imm = (InputMethodManager) curActivity.getSystemService(Activity.INPUT_METHOD_SERVICE);
					imm.hideSoftInputFromWindow(curActivity.getCurrentFocus().getWindowToken() ,  0);
					
					new UploadData(curActivity, R.string.new_resp,
							R.string.new_resp_suc, R.string.new_resp_fail, 
							sentence).execute();
				}
			};
		return result;
	}
	
	private class UploadData extends UploaderToast  
    {  
		private String sentence;
		
    	public UploadData(Context curContext, int startMsgId, int endMsgSucId, int endMsgFailId, String sentence){
    		super(curContext, startMsgId, endMsgSucId, endMsgFailId);
    		this.sentence = sentence;
    	}

		@Override
		protected Boolean uploadMethod(String userKey) throws Exception {
			ReplyToSave reply = new ReplyToSave();
    		reply.rhymeId = rhymeId;
    		reply.text = sentence;
    		reply.userKey = userKey;
    		reply.lastSentId = lastSentId;
    		return getRepoHandler().addReply(reply);
		}  
  
    }  
    
}
