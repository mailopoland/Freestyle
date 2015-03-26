package freestyle.fragments.instructions;

import com.mailoskyteam.freestyle.R;

import android.app.DialogFragment;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

//should start from this instruction, it will open next
public abstract class BaseInstructionFragment extends DialogFragment{
	
	private TextView smallText;
	private TextView bigText;
	private ImageView image;
	private Button confirmBtn;
	
	protected abstract int txtSmallId();
	protected abstract int txtBigId();
	protected abstract int txtBtnId();
	protected abstract int imageId();
	protected abstract void onBtnClick();
	public abstract String getDefaultTag();
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        this.setCancelable(false);
	        setStyle(DialogFragment.STYLE_NO_TITLE, android.R.style.Theme_Black);
	}
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
           Bundle savedInstanceState) {

       final View view = inflater.inflate(R.layout.fragment_dialog_instruction, container, false);
       smallText = (TextView) view.findViewById(R.id.instruction_small_text);
       smallText.setText(txtSmallId());
       bigText = (TextView) view.findViewById(R.id.instruction_big_text);
       bigText.setText(txtBigId());
       image = (ImageView) view.findViewById(R.id.instruction_image);
       image.setImageDrawable(getActivity().getResources().getDrawable(imageId()));
       confirmBtn = (Button) view.findViewById(R.id.button_i_got_it);
       confirmBtn.setText(txtBtnId());
       confirmBtn.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				BaseInstructionFragment.this.onBtnClick();
				dismiss();
			}
       });
       getDialog().getWindow().setBackgroundDrawable(new ColorDrawable(0));
       return view;
    }
	
	@Override
	public void onPause() {
          super.onPause();
          //fix bug of create more than one fragment (during recreation)
          dismiss();
     }
}
