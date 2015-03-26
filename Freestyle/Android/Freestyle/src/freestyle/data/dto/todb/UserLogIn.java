package freestyle.data.dto.todb;

public class UserLogIn {
	
	public String login;
	public String pass;
	public String clientVer;
	
	public UserLogIn(String login, String pass, String clientVer){
		this.login = login;
		this.pass = pass;
		this.clientVer = clientVer;
	}
}
