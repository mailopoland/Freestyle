package freestyle.activities.rhymes.abstracts;

import freestyle.data.dto.fromdb.rhymes.bases.IncompletedViewRhyme;
import freestyle.fragments.ToEndFragment;

public abstract class IncompletedRhymeActivity extends BaseViewRhymeActivity {
	
	protected final String toEndFrTag = "toEndFragmentTag";
	protected ToEndFragment toEndFr;
	
	protected void getAndShowObj(IncompletedViewRhyme obj){
		int toEnd = obj.toEnd;	
		showInActivity(toEnd);
		super.getAndShowObj(obj);
		
	}

	private void showInActivity(int toEnd){
		showMinMaxToEnd(toEnd);
	}
	
	private void showMinMaxToEnd(int toEnd){
		toEndFr = new ToEndFragment();
		toEndFr.setArguments(toEnd);
    	fragmentTransaction = fragmentManager.beginTransaction();
    	fragmentTransaction.add(additionalInfoContainer, toEndFr, toEndFrTag);
    	fragmentTransaction.commit();
	}
} 
