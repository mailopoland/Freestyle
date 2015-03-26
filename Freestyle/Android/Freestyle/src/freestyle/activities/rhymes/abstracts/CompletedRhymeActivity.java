package freestyle.activities.rhymes.abstracts;

import freestyle.data.dto.fromdb.rhymes.extend.CompletedViewRhyme;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.fragments.VoteFragment;
import freestyle.helpers.json.JsonConsumer;

public abstract class CompletedRhymeActivity extends BaseViewRhymeActivity {
	
	private int points;
	private boolean canVote;
	private int rhymeId;
	
    protected final String voteFrTag = "voteFrTag";
	
	@Override
	protected void firstCreate() {
		String downObjPack = getDataFromBundle();
		CompletedViewRhyme downObj = new JsonConsumer().getCompletedViewRhyme(downObjPack);
		if(downObj != null)
			getAndShowObj(downObj);
		else
			emptyCategory();
	}
	
	@Override
	protected UserBaseData getAuthor() {
		return curUser();
	}

	protected void  getAndShowObj(CompletedViewRhyme obj){
		points = obj.points;	
		canVote = obj.canVote;
		//it is used to show votes before other fragments (in base activity rhymeId hasn't yet set)
		rhymeId = obj.rhymeId;
		showVotes();
		super.getAndShowObj(obj);
	}
	
	private void showVotes(){
		VoteFragment fragment = new VoteFragment();
		fragment.setArguments(canVote,points,rhymeId);
		fragmentTransaction = fragmentManager.beginTransaction();
    	fragmentTransaction.add(additionalInfoContainer, fragment, voteFrTag);
    	fragmentTransaction.commit();
	}

	
} 
