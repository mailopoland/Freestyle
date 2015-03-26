package freestyle.repositories;
import freestyle.adapters.BooleanAdapter;
import freestyle.repositories.interfaces.IUserRepository;

public class UserRepository extends BaseRepositoryAbstract implements IUserRepository {
	
	public UserRepository(BooleanAdapter isWrongVer) {
		super(isWrongVer);
	}

	@Override
	public String findUser(String userLogIn) throws Exception{
		String result = null;
		result = getPostExe("FindUser").execute(userLogIn);
		return result;
	}
	
	@Override
	public String createUser(String userLogIn) throws Exception{
		String result = null;
		result = getPostExe("CreateUser").execute(userLogIn);
		return result;
	}

	@Override
	public String changeUserLogin(String setLogin) throws Exception {
		String result = null;
		result = getPostExe("ChangeUserLogin").execute(setLogin);
		return result;
	}

	@Override
	public String changeShowEmail(String userKey, boolean show) throws Exception{
		String result = null;
		result = getGetExe("ChangeShowEmail").execute(userKey, String.valueOf(show));
		return result;
	}
	
	@Override
	public String changeNotiResp(String userKey, boolean noNoti)
			throws Exception {
		String result = getGetExe("ChangeNotiResp").execute(userKey, String.valueOf(noNoti));
		return result;
	}

	@Override
	public String changeNotiAccept(String userKey, boolean noNoti)
			throws Exception {
		String result = getGetExe("ChangeNotiAccept").execute(userKey, String.valueOf(noNoti));
		return result;
	}

	@Override
	public String changeNotiFreq(String userKey, int value) throws Exception {
		String result = getGetExe("ChangeNotiFreq").execute(userKey, String.valueOf(value));
		return result;
	}
	
    /* unused change for allow to only show or not
	@Override
	public String changeUserEmail(String setEmail) throws Exception {
		String result = null;
		result = getPostExe("ChangeUserEmail").execute(setEmail);
		return result;
	}
	*/
}
