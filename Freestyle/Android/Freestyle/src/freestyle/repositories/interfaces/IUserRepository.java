package freestyle.repositories.interfaces;



public interface IUserRepository {
	public String findUser(String userLogIn) throws Exception;
	public String createUser(String userLogIn) throws Exception;
	public String changeUserLogin(String setLogin) throws Exception;
	public String changeShowEmail(String userKey, boolean show) throws Exception;
	public String changeNotiResp(String userKey, boolean noNoti) throws Exception;
	public String changeNotiAccept(String userKey, boolean noNoti) throws Exception;
	public String changeNotiFreq(String userKey, int value) throws Exception;
	/* unused change for allow to only show or not 
	 * public String changeUserEmail(String setEmail) throws Exception;
	 */
}
