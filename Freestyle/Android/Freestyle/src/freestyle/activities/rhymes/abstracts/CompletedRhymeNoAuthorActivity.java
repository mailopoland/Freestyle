package freestyle.activities.rhymes.abstracts;

import freestyle.data.dto.fromdb.rhymes.extend.CompletedWithAuthorViewRhyme;
import freestyle.data.dto.fromdb.users.UserBaseData;
import freestyle.helpers.json.JsonConsumer;

public abstract class CompletedRhymeNoAuthorActivity extends CompletedRhymeActivity {
	
	UserBaseData rhymeAuthor;
	
	@Override
	protected void firstCreate() {
		String downObjPack = getDataFromBundle();
		CompletedWithAuthorViewRhyme downObj = new JsonConsumer().getCompletedWithAuthorViewRhyme(downObjPack);	
		if(downObj != null)
		{
			//important to get author before get and show other things
			rhymeAuthor = downObj.author;
			getAndShowObj(downObj);
		}
		else
			emptyCategory();
	}
	
	@Override
	protected UserBaseData getAuthor() {
		return rhymeAuthor;
	}
    
	
} 
