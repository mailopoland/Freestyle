package freestyle.helpers;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.mailoskyteam.freestyle.R;

import freestyle.data.dto.fromdb.users.UserBaseData;

//set email with function to send email via external application
public class EmailSetter {
	
	private final Context curContext;
	
	public EmailSetter(final Context curContext){
		this.curContext = curContext;
	}
	
	public void setEmail(final TextView emailView, final UserBaseData sendTo){
		if(emailView != null){
        	if(sendTo.email != null && !sendTo.email.isEmpty()){
    			String toShow = new StringBuilder(curContext.getResources().getInteger(R.integer.user_email_max_length) + 2)
    								.append("(")
    								.append(sendTo.email)
    								.append(")").toString();
    			emailView.setText(toShow);
    			emailView.setOnClickListener(
    					new View.OnClickListener() {
    						@Override
    			            public void onClick(View viewIn) {
    			                send(sendTo);
    			            }
    					}
    			);
    		}
        	else
        		emailView.setVisibility(View.GONE);
        }
	}
	
	private void send(UserBaseData sendTo){
		DialogBuilder dialog = new DialogBuilder(curContext);
		dialog.confirmCustom(curContext.getString(R.string.send_email), curContext.getString(R.string.do_you_want_send_to) + " " + sendTo.login, getYes(sendTo), getNo());
	}
	
	private DialogInterface.OnClickListener getNo(){
		return new DialogInterface.OnClickListener(){
			public void onClick(DialogInterface dialog, int which) {
	            dialog.dismiss();
	        }
		};
	}
	
	private DialogInterface.OnClickListener getYes(final UserBaseData sendTo){
		return new DialogInterface.OnClickListener(){
			public void onClick(DialogInterface dialog, int which) {
				Intent emailIntent = new Intent(Intent.ACTION_SEND);
				emailIntent.setType("message/rfc822");
				emailIntent.putExtra(Intent.EXTRA_EMAIL  , new String[]{sendTo.email} );
				emailIntent.putExtra(Intent.EXTRA_SUBJECT, curContext.getText(R.string.hello) + " " + sendTo.login);
				try {
				    curContext.startActivity(Intent.createChooser(emailIntent, curContext.getText(R.string.choose_email_client)));
				} catch (android.content.ActivityNotFoundException ex) {
				    Toast.makeText(curContext, R.string.no_email_client, Toast.LENGTH_LONG).show();
				}
				dialog.dismiss();
	        }
		};
	}
}
