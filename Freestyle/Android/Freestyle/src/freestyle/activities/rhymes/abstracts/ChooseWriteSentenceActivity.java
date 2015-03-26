package freestyle.activities.rhymes.abstracts;

import com.mailoskyteam.freestyle.R;

import freestyle.data.dto.fromdb.rhymes.extend.ChooseWriteSentence;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.fragments.ReplyChooseFragment;
import freestyle.fragments.ReplyCommunicateFragment;
import freestyle.helpers.json.JsonConsumer;
//activity for author (no person who respond in rhyme)
public abstract class ChooseWriteSentenceActivity  extends IncompletedRhymeActivity{ 
	
	protected final String replyChooseFrTag = "replyChooseFrTag";

	
	@Override
    protected void firstCreate(){
    	String downObjPack = getDataFromBundle();
		ChooseWriteSentence downObj = new JsonConsumer().getChooseWriteSentence(downObjPack);
		if(downObj != null){
			getAndShowObj(downObj);
	    }
	    else
	    	emptyCategory();
    }
	
	@Override
	protected UserBaseData getAuthor() {
		return curUser();
	}

    protected void getAndShowObj(ChooseWriteSentence downObj){
    	super.getAndShowObj(downObj);
    	showInActivity(downObj);
    }
    
    private void showInActivity(ChooseWriteSentence downObj){  	
	    if( downObj.suggestedReplies == null){
	    	showNoResponds();
	    }
	    //display fragment with list of replies to choose
	    else{
	    	ReplyChooseFragment replyChooseFr = new ReplyChooseFragment();
	    	replyChooseFr.setArguments(downObj.suggestedReplies, rhymeId(), sentencesContainer, toEndFrTag);
	    	fragmentTransaction = fragmentManager.beginTransaction();
	    	fragmentTransaction.add(sentencesContainer, replyChooseFr, replyChooseFrTag);
	    	fragmentTransaction.commit();
	    }
    }

	

    private void showNoResponds(){
    	ReplyCommunicateFragment fragment = new ReplyCommunicateFragment();
    	fragment.setArguments(R.string.no_replies);
    	fragmentTransaction = fragmentManager.beginTransaction();
    	fragmentTransaction.add(sentencesContainer, fragment);
    	fragmentTransaction.commit();
    }
    
    

}


