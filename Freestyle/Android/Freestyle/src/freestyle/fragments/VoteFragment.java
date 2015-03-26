package freestyle.fragments;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.data.app.BundleEnum;
import freestyle.helpers.outin.uploaders.UploaderToast;

public class VoteFragment extends BaseFragment {
	//set via onCreate
	private boolean canVote;
	private int points;
	private int rhymeId;
	//set in class
	TextView pointsView;
    TextView voteUpView;
    TextView voteDownView;
	
    //bundle keys:
    private final String canVoteKeyB = BundleEnum.CAN_VOTE.toString();
    private final String pointsKeyB = BundleEnum.POINTS.toString();
    private final String rhymeIdKeyB = BundleEnum.RHYME_ID.toString();
    
    public void setArguments(boolean canVote, int points, int rhymeId) {
		Bundle frInput = new Bundle(3);
		frInput.putBoolean(canVoteKeyB, canVote);
		frInput.putInt(pointsKeyB, points);
		frInput.putInt(rhymeIdKeyB, rhymeId);
		this.setArguments(frInput);
	}
    
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(frArgs != null){
        	canVote = frArgs.getBoolean(canVoteKeyB);
        	points = frArgs.getInt(pointsKeyB);
        	rhymeId = frArgs.getInt(rhymeIdKeyB);
        }
	}
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_vote, container, false);
        pointsView = (TextView) view.findViewById(R.id.points);
        voteUpView = (TextView) view.findViewById(R.id.vote_up);
        voteDownView = (TextView) view.findViewById(R.id.vote_down);
        if(pointsView != null){
        	String pointsTxt = String.valueOf(points);
        	pointsView.setText(pointsTxt);
        }
        if(canVote)
        {
	        if(voteUpView != null){
	        	voteUpView.setOnClickListener(genListener(true));
	        }
	        if(voteDownView != null){
	        	voteDownView.setOnClickListener(genListener(false));
	        }
        }
        else
        	this.hideVoteUpDown();
        	
        return view;
    }

    
    public void hideVoteUpDown(){
    	if(voteUpView != null)
    		voteUpView.setVisibility(View.GONE);
    	if(voteDownView != null)
    		voteDownView.setVisibility(View.GONE);
    }
    
    public void addVote(boolean value){
    	if(pointsView != null){
    		if(value)
    			++points;
    		else
    			--points;
        	String pointsTxt = String.valueOf(points);
        	pointsView.setText(pointsTxt);
        }	
    }
    
	private OnClickListener genListener(final boolean isPlus) {
		return new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				new UploaderToast(VoteFragment.this.getActivity(), R.string.new_vote, R.string.new_vote_suc, R.string.new_vote_fail){
					@Override
					protected Boolean uploadMethod(String userKey)
							throws Exception {
						return getRepoHandler().addVote(userKey, rhymeId, isPlus);
					}
				}.execute();
				addVote(isPlus);
				hideVoteUpDown();
			}
		};
	}
}
