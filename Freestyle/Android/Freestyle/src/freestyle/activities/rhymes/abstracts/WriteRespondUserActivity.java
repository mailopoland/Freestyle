package freestyle.activities.rhymes.abstracts;


import android.app.FragmentTransaction;

import com.mailoskyteam.freestyle.R;

import freestyle.data.dto.fromdb.rhymes.extend.WriteRespondUser;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.fragments.ReplyCommunicateFragment;
import freestyle.fragments.UserRhymeEditFragment;
import freestyle.helpers.json.JsonConsumer;
//activity for user (no author)
public abstract class WriteRespondUserActivity extends IncompletedRhymeActivity{
	
	private UserBaseData rhymeAuthor;
	
	@Override
    protected void firstCreate() {
		String downObjPack = getDataFromBundle();
		WriteRespondUser downObj = new JsonConsumer().getWriteRespondUser(downObjPack);
		if(downObj != null){
    		getAndShowObj(downObj);
		}
		else{
			emptyCategory();
		}
	}
	
	@Override
	protected UserBaseData getAuthor() {
		return rhymeAuthor;
	}  
	
    protected void getAndShowObj(WriteRespondUser downObj){
    	//rhymeAuthor have to be set first (important order)
    	rhymeAuthor = downObj.author;
    	super.getAndShowObj(downObj);
    	showInActivity(downObj);
    }

    protected void showInActivity(WriteRespondUser downObj){
    	if(downObj.isResponded)
    	{
    		ReplyCommunicateFragment communicateFr = new ReplyCommunicateFragment();
    		communicateFr.setArguments(R.string.reply_waiting);
			FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
    		fragmentTransaction.add(sentencesContainer, communicateFr);
    		fragmentTransaction.commit();
    	}
    	else
    	{
    		showUserRhymeEdit(downObj);
    	}
    }
    
    private void showUserRhymeEdit(WriteRespondUser downObj)
    {
    	String userRhymeEditFrTag = "userRhymeEditFrTag";
    	UserRhymeEditFragment userRhymeEditFr = new UserRhymeEditFragment();
    	userRhymeEditFr.setArguments(downObj.lastSentId, downObj.rhymeId, sentencesContainer);
    	fragmentTransaction = fragmentManager.beginTransaction();
    	fragmentTransaction.add(sentencesContainer, userRhymeEditFr, userRhymeEditFrTag);
    	fragmentTransaction.commit();
    }

	

}
