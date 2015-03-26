package freestyle.fragments;

import org.json.JSONException;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.LogAdp;
import freestyle.data.app.BundleEnum;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.helpers.DialogBuilder;
import freestyle.helpers.json.JsonConsumer;
import freestyle.helpers.json.JsonCreator;
import freestyle.helpers.outin.uploaders.UploaderToast;


public class RhymeEnderFragment extends BaseFragment {

	private String communicateFrTag;
	private String authorEditorFrTag;
	private RhymeToSave rhymeToSave;
	private String toEndFrTag;
	private int txtId;

	//bundle keys:
	private final String communicateFrTagKeyB = BundleEnum.COMMUNICATE_FR_TAG.toString();
	private final String authorEditorFrTagKeyB = BundleEnum.AUTHOR_EDITOR_FR_TAG.toString();
	private final String toEndFrTagKeyB = BundleEnum.TO_END_FR_TAG.toString();
	private final String txtIdKeyB = BundleEnum.TXT_ID.toString();
	private final String rhymeToSaveJsonKeyB = BundleEnum.RHYME_TO_SAVE_JSON.toString();
	
	public void setArguments(
			final String replyCommunicateViewFragmentTag,
			final RhymeToSave rhymeToSave, final String authorEditorFrTag,
			final String rhymeEnderFrTag, final int finishTxt) {
		Bundle frInput = new Bundle(5);
		frInput.putString(communicateFrTagKeyB, replyCommunicateViewFragmentTag);
		frInput.putString(authorEditorFrTagKeyB, authorEditorFrTag);
		frInput.putString(toEndFrTagKeyB, rhymeEnderFrTag);
		frInput.putInt(txtIdKeyB, finishTxt);
		String rhymeToSaveJsonStr;
		try {
			rhymeToSaveJsonStr = new JsonCreator().createRhymeToSave(rhymeToSave);
		} catch (JSONException e) {
			rhymeToSaveJsonStr = null;
			LogAdp.e(this.getClass(),"onClick", "during create json string", e);
		}
		frInput.putString(rhymeToSaveJsonKeyB, rhymeToSaveJsonStr);
		this.setArguments(frInput);
	}
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(frArgs != null){
        	communicateFrTag = frArgs.getString(communicateFrTagKeyB);
        	authorEditorFrTag = frArgs.getString(authorEditorFrTagKeyB);
        	toEndFrTag = frArgs.getString(toEndFrTagKeyB);
        	txtId = frArgs.getInt(txtIdKeyB);
        	
        	String rhymeToSaveJson = frArgs.getString(rhymeToSaveJsonKeyB);
        	rhymeToSave = new JsonConsumer().getRhymeToSave(rhymeToSaveJson);
        }
        
	}
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        View result = inflater.inflate(R.layout.fragment_sentence_communicate_view, container, false);
        TextView makeEnd = (TextView) result.findViewById(R.id.view_sentence_communicate_text);
        makeEnd.setText(txtId);
        makeEnd.setOnClickListener(clickGen());
        RelativeLayout layout = (RelativeLayout) result.findViewById(R.id.fragment_sentence_comunicate_view_layout);
        layout.requestFocus();
        return result;
    }
    
    private OnClickListener clickGen(){
    	return new OnClickListener() {
			@Override
			public void onClick(View v) {
					new DialogBuilder(getActivity()).confirmDefault(R.string.are_you_sure_finish, yesListener(), noListener());
			}
		};
    }
    
    //if user not confirm end
  	private DialogInterface.OnClickListener noListener(){
  		return new DialogInterface.OnClickListener(){
  			public void onClick(DialogInterface dialog, int which) {
	              dialog.dismiss();
	        }
  		};
  	}
  	//if user confirm end
    private DialogInterface.OnClickListener yesListener(){
			return new DialogInterface.OnClickListener(){
				public void onClick(DialogInterface dialog, int which) {
					//fix bug - hide virtual keypad if it is opened
		    		InputMethodManager imm = (InputMethodManager) getActivity().getSystemService(Activity.INPUT_METHOD_SERVICE);
		    		imm.hideSoftInputFromWindow(getActivity().getCurrentFocus().getWindowToken() ,  0);
		    		
					new UploadData(getActivity(), R.string.finish_rhyme,
							R.string.finish_rhyme_suc, R.string.finish_rhyme_fail).execute();
					final FragmentManager fragmentManager = getActivity().getFragmentManager();
					final FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
					final Fragment communicateFr = fragmentManager.findFragmentByTag(communicateFrTag);
					final Fragment authorEditorFr = fragmentManager.findFragmentByTag(authorEditorFrTag);
					final Fragment toEndFr = fragmentManager.findFragmentByTag(toEndFrTag);
					fragmentTransaction.remove(RhymeEnderFragment.this);
					if(communicateFr != null)
						fragmentTransaction.remove(communicateFr);
					if(authorEditorFr != null)
						fragmentTransaction.remove(authorEditorFr);
					if(toEndFr != null)
						fragmentTransaction.remove(toEndFr);
					fragmentTransaction.commit();
		            dialog.dismiss();
		        }
			};
	}
    
    private class UploadData extends UploaderToast{

		public UploadData(Context curContext, int startMsgId, int endMsgSucId,
				int endMsgFailId) {
			super(curContext, startMsgId, endMsgSucId, endMsgFailId);
		}

		@Override
		protected Boolean uploadMethod(String userKey) throws Exception {
			return getRepoHandler().finishRhyme(rhymeToSave);
		}


	}
    
    
}
