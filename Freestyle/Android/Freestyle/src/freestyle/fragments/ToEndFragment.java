package freestyle.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.adapters.LogAdp;
import freestyle.data.app.ActionsAllowed;
import freestyle.data.app.BundleEnum;
import freestyle.helpers.DialogBuilder;

public class ToEndFragment extends BaseFragment {

	private final String max = "Koniec za max. ";
	private final String min = "Koniec za min. ";
	
	private int amount;
	private boolean isMinToEnd;
	
	//bundle keys:
	private final String amountKeyB = BundleEnum.AMOUNT.toString();
	
	public void setArguments(int toEnd){
		Bundle frInput = new Bundle(1);
		frInput.putInt(amountKeyB, toEnd);
		this.setArguments(frInput);
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if(frArgs != null){
			this.amount = frArgs.getInt(amountKeyB);
			if(this.amount < 0)
				isMinToEnd = false;
			else
				isMinToEnd = true;
		}
		else{
			DialogBuilder dialogMsg = new DialogBuilder(this.getActivity());
			dialogMsg.internalError();
			LogAdp.wtf(getClass(), "ToEndFragment", "no data in input bundle");
		}
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_to_end, container, false);
    }
   
    @Override
	public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState); 
        setAmount();
    }
    
    public ActionsAllowed newSentenceAdd(){
    	
    	if(!isMinToEnd){
    		amount++;
    	}
    	//after add new sentence when amountInt = 0, we don't update maxToEnd (because we have to download this value)
    	//and show it as minToEnd = 0
    	//it will be auto update when user download again this rhyme
    	else if(amount != 0){
    		amount--;
    	}
    	
    	setAmount();
    	
    	ActionsAllowed result = new ActionsAllowed();
    	result.finishRhyme = amount <= 0;
    	//amountInt == 0 in 2 cases, when 1) isMinToEnd is going into isMaxToEnd or if author have to finish
    	result.respondRhyme = (amount != 0 || (amount == 0 && isMinToEnd)) ;
    	return result;
    }
    
    private void setAmount(){
    	String text;
    	if(isMinToEnd){
    		text = min + amount;
    	}
    	else{
    		text = max + (-1) * amount;
    	}
    	
    	TextView textView = (TextView) getView().findViewById(R.id.to_end);
    	textView.setText(text);
    }
    
}
