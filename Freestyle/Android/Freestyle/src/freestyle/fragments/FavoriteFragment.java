package freestyle.fragments;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.mailoskyteam.freestyle.R;

import freestyle.data.app.BundleEnum;
import freestyle.helpers.outin.uploaders.UploaderToast;

public class FavoriteFragment extends BaseFragment{

	private int isFavorited;
	private int rhymeId;
	
	private TextView favIcon;
	
	private final int isFavoriteIconId = R.drawable.fav;
	private final int isNotFavoriteIconId = R.drawable.no_fav;
	//bundle keys:
	private final String isFavoriteKeyB = BundleEnum.IS_FAVORITE.toString();
	private final String rhymeIdKeyB = BundleEnum.RHYME_ID.toString();
	
	//method for set arguments (before the Fragment is attached to the Activity)
	public void setArguments(int isFavorited, int rhymeId){
		Bundle frInput = new Bundle(2);
		frInput.putInt(isFavoriteKeyB, isFavorited);
		frInput.putInt(rhymeIdKeyB, rhymeId);
		this.setArguments(frInput);
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if(frArgs != null){
			this.isFavorited = frArgs.getInt(isFavoriteKeyB);
			this.rhymeId = frArgs.getInt(rhymeIdKeyB);
		}
	}
	
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
        Bundle savedInstanceState) {
    	
        View view = inflater.inflate(R.layout.fragment_favorite, container, false);
        favIcon = (TextView) view.findViewById(R.id.favorite_icon);
        
        if(favIcon != null && isFavorited > 0){
        	if(isFavorited != 1)
            	favIcon.setBackgroundResource(isFavoriteIconId);
            else
            	favIcon.setBackgroundResource(isNotFavoriteIconId);
        	
        	favIcon.setOnClickListener(new View.OnClickListener() {
				
        		private MakeNoFavorite noFav = null;
        		private MakeFavorite fav = null;
   
        		
				@Override
				public void onClick(View v) {
					Context curContext = FavoriteFragment.this.getActivity();
					//remove fav
					if(isFavorited != 1){
						isFavorited = 1;
						favIcon.setBackgroundResource(isNotFavoriteIconId);
						noFav = new MakeNoFavorite(curContext, R.string.fav_removing, 
								R.string.fav_removing_suc, R.string.fav_removing_fail);
						noFav.execute();
					}
					//add fav
					else{
						isFavorited = 2;
						favIcon.setBackgroundResource(isFavoriteIconId);
						fav = new MakeFavorite(curContext, R.string.fav_adding,
								R.string.fav_adding_suc, R.string.fav_adding_fail);
						fav.execute();
					}
				}
			});
        }
        return view;
    }
   
    private class MakeFavorite extends UploaderToast  
    {

		public MakeFavorite(Context curContext, int startMsgId,
				int endMsgSucId, int endMsgFailId) {
			super(curContext, startMsgId, endMsgSucId, endMsgFailId);
		}

		@Override
		protected Boolean uploadMethod(String userKey) throws Exception {
			return getRepoHandler().makeFavorite(userKey, rhymeId);
		}

	}
    
    private class MakeNoFavorite extends UploaderToast 
    {

		public MakeNoFavorite(Context curContext, int startMsgId,
				int endMsgSucId, int endMsgFailId) {
			super(curContext, startMsgId, endMsgSucId, endMsgFailId);
		}
		
		@Override
		protected Boolean uploadMethod(String userKey) throws Exception {
			return getRepoHandler().makeNotFavorite(userKey, rhymeId);
		}
    }
    
    
}