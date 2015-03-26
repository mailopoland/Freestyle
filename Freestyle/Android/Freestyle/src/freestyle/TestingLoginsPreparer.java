package freestyle;

import android.content.Context;
import freestyle.data.app.LoginPass;

public class TestingLoginsPreparer {
	
	private Context curContext; 
	
	public TestingLoginsPreparer(Context curContext){
		this.curContext = curContext;
	}
	
	public LoginPass getPreparedUser(){
		LoginPass result = new LoginPass();
		
		int userNumber = SharedPref.getMainInt(SharedPref.Keys.PREPARE_USER_IT, curContext);
		userNumber = (userNumber + 1) % 5;
		SharedPref.setMain(SharedPref.Keys.PREPARE_USER_IT, userNumber, curContext);
		switch (userNumber){
		//*** -> means that data from production version was hided for security reason
			case 0:
				result.login = "mailo.poland@gmail.com";
				result.pass = "***";
				break;
			case 1:
				result.login = "a@a.com";
				result.pass =  "***";
				break;
			case 2:
				result.login = "b@a.com";
				result.pass = "***";
				break;
			case 3:
				result.login = "c@a.com";
				result.pass = "***";
				break;
			default:
				result.login = "d@a.com";
				result.pass = "***";
				break;
		}
		return result;
	}
}
