package freestyle.fragments.instructions;

import com.mailoskyteam.freestyle.R;
import freestyle.SharedPref;

//should start from this instruction, it will open next
public class SecondInstructionFragment extends BaseInstructionFragment{

	@Override
	protected int txtSmallId() {
		return R.string.instr_vertical_swap_small;
	}

	@Override
	protected int txtBigId() {
		return R.string.instr_vertical_swap_big;
	}

	@Override
	protected int txtBtnId() {
		return R.string.instr_ok_I_got_it;
	}

	@Override
	protected int imageId() {
		return R.drawable.scroll_vertical;
	}
	
	@Override
	protected void onBtnClick() {
		//not show again instructions
		SharedPref.setMain(SharedPref.Keys.NO_SHOW_INSTRUCTION, true, getActivity());
	}

	@Override
	public String getDefaultTag() {
		return "SecondInstructionFragment";
	}

}
