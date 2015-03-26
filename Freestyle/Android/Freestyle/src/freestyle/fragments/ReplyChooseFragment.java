package freestyle.fragments;

import java.util.List;
import java.util.ListIterator;

import org.json.JSONArray;

import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.SharedPref;
import freestyle.adapters.LogAdp;
import freestyle.data.app.ActionsAllowed;
import freestyle.data.app.BundleEnum;
import freestyle.data.dto.fromdb.rhymes.helpers.ReplyToAccepted;
import freestyle.data.dto.todb.RhymeToSave;
import freestyle.data.dto.todb.RhymeWithSenToSave;
import freestyle.helpers.EmailSetter;
import freestyle.helpers.json.JsonConsumer;
import freestyle.helpers.json.JsonCreator;


public class ReplyChooseFragment extends BaseFragment{

//	public Button accept;
	public ReplyToAccepted curEl;
	
	private List<ReplyToAccepted> replies;
	private ListIterator<ReplyToAccepted> iter;
	private TextView text;
	private TextView login;
	private TextView email;
	private ImageButton left;
	private ImageButton right;
	private Button accept;
	private Button decline;
	
	//switch between operation prev and next
	private boolean wasNextClick;
	private EmailSetter emailSet;
	private int sentencesContainer;
	private String toEndFrTag;
	private int rhymeId;
	
	//bundle keys
	private final String repliesToAcceptJsonKeyB = BundleEnum.REPLIES_TO_ACCEPT_JSON.toString();
	private final String sentencesContainerKeyB = BundleEnum.SENTENCES_CONTAINER.toString();
	private final String toEndFrTagKeyB = BundleEnum.TO_END_FR_TAG.toString();
	private final String rhymeIdKeyB = BundleEnum.RHYME_ID.toString();
	
	//method for set arguments (before the Fragment is attached to the Activity)
	public void setArguments(final List<ReplyToAccepted> suggestedReplies, 
			final int rhymeId, final int sentencesContainer, final String toEndFrTag) {
		Bundle frInput = new Bundle(4);
		String repliesToAcceptStr;
		try {
			repliesToAcceptStr = new JsonCreator().createListReplyToAccepted(suggestedReplies);
		} catch (Exception e) {
			repliesToAcceptStr = new JSONArray().toString();
			LogAdp.e(this.getClass(), "showInActivity(ChooseWriteSentence downObj)", e.toString(), e);
		}
		frInput.putString(repliesToAcceptJsonKeyB, repliesToAcceptStr );
		frInput.putInt(sentencesContainerKeyB, sentencesContainer);
		frInput.putString(toEndFrTagKeyB, toEndFrTag);
		frInput.putInt(rhymeIdKeyB, rhymeId);
		this.setArguments(frInput);
	}
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.emailSet = new EmailSetter(this.getActivity());
        if(frArgs != null){
        	JsonConsumer jsCons = new JsonConsumer();
        	String repliesJsStr = frArgs.getString(repliesToAcceptJsonKeyB);
        	this.replies = jsCons.getListReplyToAccepted(repliesJsStr);
        	this.iter = replies.listIterator();
        	if(iter.hasNext()){
	        	curEl = iter.next();
	        	wasNextClick = true;
        	}
        	this.sentencesContainer = frArgs.getInt(sentencesContainerKeyB);
        	this.toEndFrTag = frArgs.getString(toEndFrTagKeyB);
        	this.rhymeId = frArgs.getInt(rhymeIdKeyB);
        }
        
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        View result = inflater.inflate(R.layout.fragment_reply_choose_view, container, false);
        text = (TextView) result.findViewById(R.id.view_reply_choose_text);
        login = (TextView) result.findViewById(R.id.view_user_author_login);
        email = (TextView) result.findViewById(R.id.view_user_author_email);
        left = (ImageButton) result.findViewById(R.id.btn_left);
        right = (ImageButton) result.findViewById(R.id.btn_right);
        accept = (Button) result.findViewById(R.id.btn_accept);
        decline = (Button) result.findViewById(R.id.btn_decline);
        setButtons();

        return result;
    }

	private void setButtons() {
		if(text != null && login != null){
        	
        	setTextFields();
        
        	//decline implement in the future
        	if(decline != null)
        		decline.setVisibility(View.GONE);
	        
	        if(accept != null){
	        	accept.setOnClickListener( genListener() );
	        	if(right != null && left != null){
					setRightBtn();
					setLeftBtn();
	        	}
	        	else
	        		//means that sth is wrong!
	        		return; 
	        	
	        	boolean hideNextArrow = true;
	        	if(wasNextClick){
	        		hideNextArrow = !iter.hasNext();
	        	}
	        	else{
	        		//change direction
	        		iter.next();
	        		hideNextArrow = !iter.hasNext();
	        		//change to old direction
	        		iter.previous();
	        	}
	        	
	        	if(hideNextArrow)
	        		right.setVisibility(View.GONE);
	        	
				boolean hidePrevArrow = true;
				if(wasNextClick){
					//change direction
					iter.previous();
					hidePrevArrow = !iter.hasPrevious();
					//change to old direction
					iter.next();
				}
				else
					hidePrevArrow = !iter.hasPrevious();
				if(hidePrevArrow)
	        		left.setVisibility(View.GONE);
	        }
        }
	}

	private void setLeftBtn() {
		left.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if(iter.hasPrevious()){
					curEl = iter.previous();
					if(wasNextClick)
					{
						curEl = iter.previous();
						wasNextClick = false;
					}
					setTextFields();
				}
				if(!iter.hasPrevious())
					left.setVisibility(View.GONE);
				if(!right.isShown())
					right.setVisibility(View.VISIBLE);
					
			}
		});
	}

	private void setRightBtn() {
		right.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if(iter.hasNext()){
					curEl = iter.next();

					if(!wasNextClick){
						curEl = iter.next();
						wasNextClick = true;
					}
					setTextFields();
				}
			    if(!iter.hasNext())
					right.setVisibility(View.GONE);
				if(!left.isShown())
					left.setVisibility(View.VISIBLE);	
			}
		});
	}
    
    private void setTextFields(){
    	text.setText(this.getActivity().getString(R.string.three_dots) + curEl.replyTxt);
        login.setText(curEl.user.login);
        //set visable because emailSet can hide it
        email.setVisibility(View.VISIBLE);
        emailSet.setEmail(email, curEl.user);
    }
    
    private View.OnClickListener genListener()
	{
		return new View.OnClickListener() {
			@Override
			public void onClick(View v) {

				final ReplyChooseFragment curFr = ReplyChooseFragment.this;
    			final FragmentManager fragmentManager = curFr.getActivity().getFragmentManager();
				final ReplyToAccepted replyToAccept = curFr.curEl;	        				        		
        		final String replyCommunicateViewFragmentTag = "sentenceCommunicateViewFragmentTag";
        		final SentenceCommunicateFragment replyCommunicateFr = new SentenceCommunicateFragment();
        		replyCommunicateFr.setArguments(R.string.do_action_to_save);
        		final UserRhymeViewFragment newUserRhymeFr = new UserRhymeViewFragment();
        		newUserRhymeFr.setArguments(replyToAccept.replyTxt, replyToAccept.user);
        		final FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        		fragmentTransaction.remove(curFr);
        		fragmentTransaction.add(sentencesContainer, newUserRhymeFr);
        		fragmentTransaction.add(sentencesContainer, replyCommunicateFr, replyCommunicateViewFragmentTag);
        		showNextActionsFr(curFr, fragmentManager, replyToAccept,
						replyCommunicateViewFragmentTag, fragmentTransaction);
        		
        		fragmentTransaction.commit();
    		}

			private void showNextActionsFr(final ReplyChooseFragment curFr,
					final FragmentManager fragmentManager,
					final ReplyToAccepted replyToAccept,
					final String replyCommunicateViewFragmentTag,
					final FragmentTransaction fragmentTransaction) {
				final ToEndFragment toEndFr = (ToEndFragment)fragmentManager.findFragmentByTag(toEndFrTag);
        		if(toEndFr != null){
        			RhymeToSave rhymeToSave = new RhymeToSave();
        			rhymeToSave.rhymeId = rhymeId;
        			rhymeToSave.replyId = replyToAccept.replyId;
        			rhymeToSave.userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curFr.getActivity());
        			final String authorEditorFrTag = "authorEditorFrTag";
        			final String rhymeEnderFrTag = "rhymeEnderFrTag";
        			//is possible to respond and (or only or without) finish
        			ActionsAllowed actionsAllowed = toEndFr.newSentenceAdd();
        			int finishTxt = R.string.click_to_finish;
        			if(actionsAllowed.respondRhyme){
        				showRespondRhymeFr(curFr,
								replyCommunicateViewFragmentTag,
								fragmentTransaction, toEndFr, rhymeToSave,
								authorEditorFrTag, rhymeEnderFrTag);
        				finishTxt = R.string.or_click_to_finish;
        			}
        			if(actionsAllowed.finishRhyme){
        				showFinishRhymeFr(replyCommunicateViewFragmentTag,
								fragmentTransaction, rhymeToSave,
								authorEditorFrTag, rhymeEnderFrTag, finishTxt);
        			}
        		}
			}

			private void showRespondRhymeFr(final ReplyChooseFragment curFr,
					final String replyCommunicateViewFragmentTag,
					final FragmentTransaction fragmentTransaction,
					final ToEndFragment toEndFr, RhymeToSave rhymeToSave,
					final String authorEditorFrTag, final String rhymeEnderFrTag) {
				RhymeWithSenToSave toUpdateRhyme = new RhymeWithSenToSave(rhymeToSave);
				AuthorRhymeEditFragment rhymeUpdateFr = new AuthorRhymeEditFragment();
				rhymeUpdateFr.setArguments(replyCommunicateViewFragmentTag,
						rhymeEnderFrTag, toUpdateRhyme, sentencesContainer, toEndFrTag);
				fragmentTransaction.add(sentencesContainer, rhymeUpdateFr, authorEditorFrTag);
			}

			

			private void showFinishRhymeFr(
					final String replyCommunicateViewFragmentTag,
					final FragmentTransaction fragmentTransaction,
					RhymeToSave rhymeToSave, final String authorEditorFrTag,
					final String rhymeEnderFrTag, int finishTxt) {
				RhymeEnderFragment rhymeEnderFr = new RhymeEnderFragment();
				rhymeEnderFr.setArguments(replyCommunicateViewFragmentTag,
						rhymeToSave, authorEditorFrTag, rhymeEnderFrTag,
						finishTxt);
				fragmentTransaction.add(sentencesContainer, rhymeEnderFr, rhymeEnderFrTag);
			}

			
	    	};
	}
}
