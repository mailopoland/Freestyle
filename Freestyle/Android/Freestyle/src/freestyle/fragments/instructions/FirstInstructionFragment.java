package freestyle.fragments.instructions;

import com.mailoskyteam.freestyle.R;

//should start from this instruction, it will open next
public class FirstInstructionFragment extends BaseInstructionFragment{

	@Override
	protected int txtSmallId() {
		return R.string.instr_horizontal_swap_small;
	}

	@Override
	protected int txtBigId() {
		return R.string.instr_horizontal_swap_big;
	}

	@Override
	protected int txtBtnId() {
		return R.string.instr_ok_I_got_it;
	}

	@Override
	protected int imageId() {
		return R.drawable.scroll_hotizontal;
	}
	
	@Override
	protected void onBtnClick() {
		BaseInstructionFragment nextFragment = new SecondInstructionFragment();
		nextFragment.show(getActivity().getFragmentManager(), nextFragment.getDefaultTag());
	}

	@Override
	public String getDefaultTag() {
		return "FirstInstructionFragment";
	}


}
