package freestyle.activities.rhymes.abstracts;

import java.util.Iterator;
import java.util.List;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.GestureDetector;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ScrollView;
import android.widget.TextView;
import com.mailoskyteam.freestyle.R;
import freestyle.SharedPref;
import freestyle.activities.AppMenuActivity;
import freestyle.adapters.LogAdp;
import freestyle.data.dto.fromdb.rhymes.bases.BaseViewRhyme;
import freestyle.data.dto.fromdb.rhymes.helpers.SentenceToShow;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.data.dto.todb.ReqRhyme;
import freestyle.fragments.AuthorRhymeViewFragment;
import freestyle.fragments.FavoriteFragment;
import freestyle.fragments.SentenceCommunicateFragment;
import freestyle.fragments.UserRhymeViewFragment;
import freestyle.fragments.instructions.FirstInstructionFragment;
import freestyle.helpers.DialogBuilder;
import freestyle.helpers.EmailSetter;
import freestyle.helpers.outin.downloaders.Downloader;
import freestyle.helpers.outin.downloaders.DownloaderRhymeActiv;
import freestyle.repositories.RepoHandler;

public abstract class BaseViewRhymeActivity extends AppMenuActivity  {
	
	protected final FragmentManager fragmentManager = getFragmentManager();
	protected FragmentTransaction fragmentTransaction;

	protected final int sentencesContainer = R.id.sentences_rhymes_container;
	protected final int additionalInfoContainer = R.id.additional_info_container;
	
	//log in user data:
	protected String curUserId(){
		return SharedPref.getMainStr(SharedPref.Keys.USER_KEY, this);
	}
	
	protected UserBaseData curUser(){
		UserBaseData curUser = new UserBaseData();
		curUser.login = SharedPref.getMainStr(SharedPref.Keys.USER_LOGIN, this);
		curUser.email = SharedPref.getMainStr(SharedPref.Keys.USER_EMAIL, this);
		return curUser;
	}

	protected DialogBuilder dialog(){
		return new DialogBuilder(this);
	}
	
	private Bundle comingData;
	
	private int rhymeId;
	private final String rhymeIdTag = "rhymeId"; 
	private String rhymeCurVal;
	private final String rhymeCurValTag = "rhymeCurVal";
	private String rhymeTitle;
	private final String rhymeTitleTag = "rhymeTitle";
	private UserBaseData rhymeAuthor;
	private final String rhymeAuthorLoginTag = "rhymeAuthorLogin";
	private final String rhymeAuthorEmailTag = "rhymeAuthorEmail";
	
	//move detector
	private GestureDetector gestureDetector;
	private ScrollView layout;
	
	protected abstract String downloadRhymeMethod(boolean isNext, RepoHandler serviceRhyme) throws Exception;
	protected abstract UserBaseData getAuthor();
	//last action in onCreate, not use during recreating
	protected abstract void firstCreate();
	
	public int rhymeId(){
		return rhymeId;
	}
	
	public String rhymeCurValue(){
		return rhymeCurVal;
	}
	
	public void setHorizontalSlideOn(){
		if(layout != null){
			layout.setOnTouchListener(new View.OnTouchListener(){
	        	@Override
	            public boolean onTouch(View arg0, MotionEvent arg1) {
	        		arg0.requestFocusFromTouch();
	                //gesture detector to detect swipe.
	                gestureDetector.onTouchEvent(arg1);
	                //performClick() do what click should do normally
	                return arg0.performClick();
	            }
	        });
		}
	}
	
	public void setHorizontalSlideOff(){
		if(layout != null){
			layout.setOnTouchListener(null);
		}
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        firstRun();
        this.gestureDetector = new GestureDetector(this, new GestureListener());
        comingData = getIntent().getExtras();
        setContentView(R.layout.activity_rhyme_base_view);
        layout = (ScrollView) findViewById(R.id.main_container);
        if(layout != null){
	        //fix bug with auto scroll when scroll contain focusable element
	        layout.setDescendantFocusability(ViewGroup.FOCUS_BEFORE_DESCENDANTS);
	        layout.setFocusable(true);
	        layout.setFocusableInTouchMode(true);
	        //end fix + arg0.requestFocusFromTouch() (this meth do switch l/r too) in onTouch below
	        setHorizontalSlideOn();
	        //no recreate
	        if(savedInstanceState == null){
	        	firstCreate();
	        }
        }
   	}
	
	
	@Override
	protected void onSaveInstanceState(Bundle state) {
	    super.onSaveInstanceState(state);    
	    state.putString(rhymeTitleTag, rhymeTitle);
	    state.putInt(rhymeIdTag, rhymeId);
	    state.putString(rhymeCurValTag, rhymeCurVal);
	    if(rhymeAuthor != null){
	    	state.putString(rhymeAuthorLoginTag, rhymeAuthor.login);
	    	state.putString(rhymeAuthorEmailTag, rhymeAuthor.email);
	    }
	    else{
	    	LogAdp.e(getClass(), "onSaveInstanceState(Bundle state)", "no author");
	    }
	    
	}
	
	@Override
	protected void onRestoreInstanceState(Bundle savedInstanceState) {
	    super.onRestoreInstanceState(savedInstanceState);
	    rhymeTitle = savedInstanceState.getString(rhymeTitleTag);
	    rhymeId = savedInstanceState.getInt(rhymeIdTag);
		rhymeCurVal = savedInstanceState.getString(rhymeCurValTag);
		rhymeAuthor = new UserBaseData();
		rhymeAuthor.login = savedInstanceState.getString(rhymeAuthorLoginTag);
		rhymeAuthor.email = savedInstanceState.getString(rhymeAuthorEmailTag);
		
	    showTitle(rhymeTitle);
	    showAuthor(rhymeAuthor);
	}
	
	protected void emptyCategory(){
		SentenceCommunicateFragment communicateFr = new SentenceCommunicateFragment();
		communicateFr.setArguments(R.string.empty_category);
		fragmentTransaction = fragmentManager.beginTransaction();
		fragmentTransaction.add(sentencesContainer, communicateFr);
		fragmentTransaction.commit();
		setHorizontalSlideOff();
	}

	protected void getAndShowObj(BaseViewRhyme obj){
		rhymeId = obj.rhymeId;
		rhymeCurVal = obj.curValue;
		rhymeTitle = obj.title;
		rhymeAuthor = getAuthor();
    	List<SentenceToShow> sentencesToShow = obj.sentencesToShow;
    	int isFavorited = obj.isFavorited;
    	showFavorite(isFavorited, rhymeId);	
    	showTitle(rhymeTitle);
    	showAuthor(rhymeAuthor);
    	showSentences(sentencesToShow);
    	
	}
	
	protected String getDataFromBundle(){
		return comingData.getString(Downloader.downObjKey);
	}
	
	private void showFavorite(int isFavorited, int rhymeKey){
		if(isFavorited > 0){
			FavoriteFragment favFr = new FavoriteFragment();
			favFr.setArguments(isFavorited, rhymeKey);
			fragmentTransaction = fragmentManager.beginTransaction();
			fragmentTransaction.add(additionalInfoContainer, favFr);
			fragmentTransaction.commit();
		}
	}

	private void showTitle(String title){
		TextView titleTextView = (TextView) findViewById(R.id.title_view);
		if(title != null && !title.isEmpty())
			titleTextView.setText(title);
		else{
    		dialog().internalError();
    		LogAdp.e(getClass(), "showTitle", "no title");
		}
	}
	
	private void showAuthor(final UserBaseData rhymeAuthor){
		TextView authorNameView = (TextView) findViewById(R.id.author_name);
		TextView authorEmailView = (TextView) findViewById(R.id.author_email);
		if(rhymeAuthor != null && rhymeAuthor.login != null && !rhymeAuthor.login.isEmpty()){
			authorNameView.setText(rhymeAuthor.login);
			new EmailSetter(this).setEmail(authorEmailView, rhymeAuthor);
		}
		else{
    		dialog().internalError();
    		LogAdp.e(getClass(), "showAuthor", "no author");
		}
	}
	
	private void showSentences(List<SentenceToShow> sentencesToShow){
		if(sentencesToShow != null)
		{
	        fragmentTransaction = fragmentManager.beginTransaction();
	        
	        Iterator<SentenceToShow> iter = sentencesToShow.iterator();
	        SentenceToShow sen;
	        for( int i = 0 ; i < sentencesToShow.size() - 1; i++){
	        	sen = iter.next();
	        	AuthorRhymeViewFragment authorSentenceFr = new AuthorRhymeViewFragment();
	        	authorSentenceFr.setArguments(sen.authorTxt);
	        	fragmentTransaction.add(sentencesContainer, authorSentenceFr);
	        	UserRhymeViewFragment userSentenceFr = new UserRhymeViewFragment();
	        	userSentenceFr.setArguments(sen.replyTxt,sen.user);
	        	fragmentTransaction.add(sentencesContainer, userSentenceFr);
	        	
	        }
	        sen = iter.next();
	        AuthorRhymeViewFragment authorSentenceFr = new AuthorRhymeViewFragment();
        	authorSentenceFr.setArguments(sen.authorTxt);
	        fragmentTransaction.add(sentencesContainer, authorSentenceFr);
	        
	        if(sen.replyTxt != null && !sen.replyTxt.isEmpty()){
	        	UserRhymeViewFragment userSentenceFr = new UserRhymeViewFragment();
	        	userSentenceFr.setArguments(sen.replyTxt,sen.user);
	        	fragmentTransaction.add(sentencesContainer, userSentenceFr);
	        }
	        
	        fragmentTransaction.commit();
		}
	}

	protected ReqRhyme getReqRhyme(boolean isNext){
		return new ReqRhyme(curUserId(), rhymeId(), rhymeCurValue(), isNext);
	}
	
	private DownloaderRhymeActiv getDownloader(final boolean isNext){
		return new DownloaderRhymeActiv(this, isNext) {
			
			@Override
			protected String downloadRhyme() throws Exception {
				String result = downloadRhymeMethod(isNext, getRepoHandler());
				return result;
			}
		};
	}
	
	private boolean getSlide(boolean isNext){
		DownloaderRhymeActiv downloader = getDownloader(isNext);
		if(downloader != null){
			downloader.execute();
		}
		return true;
	}
	//method check that is first run and if it is do prepared actions
	private void firstRun(){
		if(!SharedPref.getMainBool(SharedPref.Keys.NO_SHOW_INSTRUCTION, this)){
			FirstInstructionFragment startInstructions = new FirstInstructionFragment();
			//last showed fragment change value in preference (to not show instructions again)
			startInstructions.show(BaseViewRhymeActivity.this.getFragmentManager(), startInstructions.getDefaultTag());
		}
	}
	
	private final class GestureListener extends SimpleOnGestureListener {

	    private static final int SWIPE_THRESHOLD = 100;
	    private static final int SWIPE_VELOCITY_THRESHOLD = 100;

	    @Override
	    public boolean onDown(MotionEvent e) {
	        return true;
	    }
	    @Override
	    public boolean onSingleTapConfirmed(MotionEvent e) {
	        //onTouch(e);
	        return true;
	    }


	    @Override
	    public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
	        boolean result = false;
	        try {
	            float diffY = e2.getY() - e1.getY();
	            float diffX = e2.getX() - e1.getX();
	            if (Math.abs(diffX) > Math.abs(diffY)) {
	                if (Math.abs(diffX) > SWIPE_THRESHOLD && Math.abs(velocityX) > SWIPE_VELOCITY_THRESHOLD) {
	                    if (diffX > 0) {
	                    	getSlide(true); //from left to right (prev)
	                    } else {
	                    	getSlide(false); //from right to left (next)
	                    }
	                }
	            }
	        } catch (Exception e) {
	            LogAdp.e(getClass(), "onFling", e.toString(), e);
	        }
	        return result;
	    }
	}
}
