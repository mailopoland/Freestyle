package freestyle.helpers;

import android.content.Context;
import com.mailoskyteam.freestyle.R;

public class Validator {
	
	private final Context curContext;
	public Validator(Context curContext){
		this.curContext = curContext;
	}
	
	public String isValidTitleMinAmount(){
		int minLen = getTitleMin();
		return getTxtBreak() + minLen;
	}
	
	public boolean isValidTitle(String text){
		int minLen = getTitleMin();
		return text.length() >= minLen;
	}
	
	public String isValidShortTextMinAmount(){
		int minLen = getShortTextMin();
		return getTxtBreak() + minLen;
	}
	public boolean isValidShortText(String text) {
		int minLen = getShortTextMin();
		return text.length() >= minLen;
	}
	
	public String isValidLoginTextMinAmount(){
		int minLen = getLoginTextMin();
		return getTxtBreak() + minLen;
	}
	
	public boolean isValidLogin(String text){
		int minLen = getLoginTextMin();
		return text.length() >= minLen;
	}
	
	public String isValidEmailTextMinAmount(){
		StringBuilder builder = new StringBuilder(" ");
		builder.append(curContext.getResources().getInteger(R.integer.user_email_min_length));
		builder.append("\n");
		builder.append(curContext.getString(R.string.empty_to_remove));
		return  builder.toString();
	}
	
	public boolean isValidEmail(String text){
		String ePattern = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$";
		java.util.regex.Pattern p = java.util.regex.Pattern.compile(ePattern);
        java.util.regex.Matcher m = p.matcher(text);
		return (m.matches() && text.length() >= curContext.getResources().getInteger(R.integer.user_email_min_length)) || text.length() == 0;
	}
	
	private int getShortTextMin(){
		return curContext.getResources().getInteger(R.integer.rhyme_msg_min_length);
	}
	
	private int getTitleMin(){
		return curContext.getResources().getInteger(R.integer.rhyme_title_min_length);
	}
	
	private int getLoginTextMin(){
		return curContext.getResources().getInteger(R.integer.user_login_min_length);
	}
	
	private String getTxtBreak(){
		return " ";
	}
}
