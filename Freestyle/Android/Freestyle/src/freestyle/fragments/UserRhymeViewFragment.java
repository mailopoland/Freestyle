package freestyle.fragments;

import org.json.JSONException;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.LogAdp;
import freestyle.data.app.BundleEnum;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.helpers.EmailSetter;
import freestyle.helpers.json.JsonConsumer;
import freestyle.helpers.json.JsonCreator;


public class UserRhymeViewFragment extends BaseFragment {
			
	private String text;
	private UserBaseData author;
	
	//bundle keys:
	private final String textKeyB = BundleEnum.TXT.toString();
	private final String authorJsonKeyB = BundleEnum.AUTHOR_JSON.toString();
	
	public void setArguments(final String text, final UserBaseData author){
		Bundle frInput = new Bundle(2);
		frInput.putString(textKeyB, text);
		String authorJsonStr;
		try {
			authorJsonStr = new JsonCreator().createUserBaseData(author);
		} catch (JSONException e) {
			authorJsonStr = null;
			LogAdp.e(this.getClass(), "setArguments(final String text, final UserBaseData author)", 
					"error during create author jsObj", e);
		}
		frInput.putString(authorJsonKeyB, authorJsonStr);
		this.setArguments(frInput);
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if(frArgs != null){
			this.text = this.getActivity().getString(R.string.three_dots) + frArgs.getString(textKeyB);
			this.author = new JsonConsumer().getUserBaseData(frArgs.getString(authorJsonKeyB));
		}
		if(author == null)
		{
			this.author = new UserBaseData();
			this.author.login = "";
		}
	}
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        View result = inflater.inflate(R.layout.fragment_user_rhyme_view, container, false);
        
        TextView textView = (TextView) result.findViewById(R.id.view_user_sentence_text);
    	TextView loginView = (TextView) result.findViewById(R.id.view_user_author_login);
    	TextView emailView = (TextView) result.findViewById(R.id.view_user_author_email);
        if(textView != null)
        	textView.setText(text);
        if(loginView != null)
        	loginView.setText(author.login);
        new EmailSetter(this.getActivity()).setEmail(emailView, author);
    	
        return result; 
    }

}
