package freestyle.activities.rhymes;


import java.util.ArrayList;

import org.json.JSONException;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.mailoskyteam.freestyle.R;

import freestyle.Global;
import freestyle.SharedPref;
import freestyle.activities.AppMenuActivity;
import freestyle.activities.rhymes.spec.ChooseWriteSenStdActivity;
import freestyle.adapters.LogAdp;
import freestyle.data.dto.fromdb.rhymes.NewRhymeReturn;
import freestyle.data.dto.fromdb.rhymes.extend.ChooseWriteSentence;
import freestyle.data.dto.fromdb.rhymes.helpers.SentenceToShow;
import freestyle.data.dto.todb.NewRhyme;
import freestyle.helpers.Validator;
import freestyle.helpers.json.JsonCreator;
import freestyle.helpers.outin.downloaders.Downloader;
import freestyle.helpers.outin.uploaders.UploaderSimple;
import freestyle.repositories.RepoHandler;

//activity for author (no like other activities read ready rhymes - only information about toEnd (num of sentences))
public class AddFirstRhymeActivity  extends AppMenuActivity {

	private EditText titleEditText;
	private EditText sentenceEditText;
	private String sentence;
	private String title;
	
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_first_rhyme);       
        titleEditText = (EditText) findViewById(R.id.title_edit);
        sentenceEditText = (EditText) findViewById(R.id.edit_author_sentence_text);
        if(titleEditText != null && sentenceEditText != null){
	        final Button button = (Button) findViewById(R.id.accept_button);
	        final Context curContext = this;
	        if(button != null)
		        button.setOnClickListener(new View.OnClickListener() {
		            public void onClick(View v) {
		            	boolean isVaild = true;
		            	Validator validator = new Validator(curContext);
		            	title = titleEditText.getText().toString();
		            	if(!validator.isValidTitle(title)){
		            		titleEditText.setError(curContext.getString(R.string.min_lenght) + validator.isValidTitleMinAmount());
		            		isVaild = false;
		            	}
		            	sentence = sentenceEditText.getText().toString();
		            	if(!validator.isValidShortText(sentence)){
		            		sentenceEditText.setError(curContext.getString(R.string.min_lenght) + validator.isValidShortTextMinAmount());
		            		isVaild = false;
		            	}
		            	if(isVaild){
		            		new UploadData(curContext).execute();
		            	}
		            }
		        });
        }
    }

    private class UploadData extends UploaderSimple
    {  
    	private NewRhymeReturn newDate;
    	
    	public UploadData(Context curContext){
    		super(curContext);
    	}

   
        protected Boolean doInBackground(Void... params)  
        {   
        	NewRhyme newRhyme = new NewRhyme();
			newRhyme.title = title;
			newRhyme.text = sentence;
			newRhyme.userKey = SharedPref.getMainStr(SharedPref.Keys.USER_KEY, curContext);
        	RepoHandler service = getRepoHandler();
        	newDate = null;
        	try {
				newDate = service.createNewRhyme(newRhyme);
			} catch (Exception e) {
				newDate = null;
				LogAdp.e(AddFirstRhymeActivity.class, "doInBackground", "during upload new rhyme", e);
			}
        	boolean errorDownload = newDate == null;
            return errorDownload;  
        }  


		@Override
		protected void doIfSuccessed() {
			
			ChooseWriteSentence obj = new ChooseWriteSentence();
			obj.rhymeId = newDate.rhymeId;
			obj.curValue = String.valueOf(Global.newestId);
			SentenceToShow sentToShow = new SentenceToShow();
			sentToShow.authorTxt = sentence;
			obj.sentencesToShow = new ArrayList<SentenceToShow>(1);
			obj.sentencesToShow.add(sentToShow);
			obj.isFavorited = 0;
			obj.title = title;
			obj.toEnd = newDate.toEnd;
			JsonCreator jsCre = new JsonCreator();
			String jsObj = null;
			try {
				jsObj = jsCre.createChooseWriteSentence(obj);
			} catch (JSONException e) {
				LogAdp.e(AddFirstRhymeActivity.class, "doIfSuccessed", "during create jsObj", e);
			}
			
			Intent toCreatedRhyme = new Intent(curContext, ChooseWriteSenStdActivity.class);
			toCreatedRhyme.putExtra(Downloader.downObjKey, jsObj);
    		startActivity(toCreatedRhyme);
    		finish();
			
		}
    }  
}
