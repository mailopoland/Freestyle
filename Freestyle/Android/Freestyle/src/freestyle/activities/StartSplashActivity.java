package freestyle.activities;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import com.mailoskyteam.freestyle.R;
import freestyle.Global;
import freestyle.SharedPref;
import freestyle.TestingLoginsPreparer;
import freestyle.adapters.LogAdp;
import freestyle.data.app.LoginPass;
import freestyle.data.dto.fromdb.users.LogInBaseData;
import freestyle.data.dto.todb.UserLogIn;
import freestyle.helpers.DialogBuilder;
import freestyle.helpers.UserGoogleInfoFetcher;
import freestyle.helpers.outin.AsyncGen;
import freestyle.repositories.RepoHandler;
import freestyle.services.Schedule;

public class StartSplashActivity extends Activity {

	private UserGoogleInfoFetcher accountMan;
	//use if Global.prepareLoginForTesting = true
	private LoginPass preparedLogin;
    @Override
    protected void onCreate(Bundle savedInstanceState) {

    	super.onCreate(savedInstanceState);
    	setContentView(R.layout.activity_start_splash);
    	String login = getAccountLogin();
    	accountMan = new UserGoogleInfoFetcher(this);
    	if(Global.prepareLoginForTesting){
    		preparedLogin = new TestingLoginsPreparer(this).getPreparedUser();
    		authorize(preparedLogin.login);
    	}
    	else if(accountMan.accountIsOnDevice(login) )
    		authorize(login);
		else{
			//it after load account name create and use LoadViewTask
			new DialogBuilder(this).chooseAccount(this);
		}
    }

    private String getAccountLogin(){
    	String account = SharedPref.getMainStr(SharedPref.Keys.USER_ACCOUNT, this);
    	return account;
    }

    //prevent back during loading
    @Override
    public void onBackPressed() {}

    public void authorize(String login){
    	new LoadViewTask(this, login).execute();
    }

    private class LoadViewTask extends AsyncGen<Void, Void, Void>
    {
    	private boolean noAccount;
    	private boolean loginFail;
    	private final String curVersion;
    	private String userLogin;

        public LoadViewTask(Context curContext, String userLogin) {
			super(curContext);
			curVersion = getAppNameVersion();
			this.userLogin = userLogin;
		}

		@Override
        protected Void doInBackground(Void... params)
        {

        	if(userLogin != null){
        		noAccount = false;
        		String userPassword;

        		//checking for sure that account exsist on device
        		//(prevent cheating)
        		boolean isUserLoginOnDevice;

    	    	if(Global.prepareLoginForTesting){
	    	    	userPassword = preparedLogin.pass;
	    	    	isUserLoginOnDevice = true;
    	    	}
    	    	else{
    	    		userPassword = this.codePass(userLogin);
    	    		isUserLoginOnDevice = accountMan.accountIsOnDevice(userLogin);
    	    	}

    	    	//first check that user exsist, if not try create account
    	    	if(isUserLoginOnDevice && (logIn( userLogin, userPassword ) || createAccount( userLogin, userPassword )))
    	    	{
    	    		loginFail = false;
    	    		Intent intent = new Intent(StartSplashActivity.this, MainMenuActivity.class);
                	startActivity(intent);
                	finish();
    	    	}
    	    	else
        			loginFail = true;
        	}
        	else
        		noAccount = true;
            return null;
        }

        //after executing the code in the thread checking that is no error
        @Override
        protected void replaceOnPostExecute(Void output)
        {
        	if(noAccount){
        		new DialogBuilder(StartSplashActivity.this).noAccount();
        	}
        	else if(loginFail){
        		new DialogBuilder(StartSplashActivity.this).problemWithLogin();
        	}
        }
        //create account and get user's data to device
        protected boolean createAccount(String userName, String password){
        	RepoHandler service = getRepoHandler();
        	UserLogIn input = new UserLogIn(userName, password, curVersion);
        	LogInBaseData user;
    		try {
    			user = service.createUser(input);
    		} catch (Exception e) {
    			LogAdp.e(getClass(), "createAccount", "during create account", e);
    			return false;
    		}
        	return setGlobalUserData(user);
        }

        //log in and update data if change on other device
        protected boolean logIn(String userName, String password){
        	RepoHandler service = getRepoHandler();
        	UserLogIn input = new UserLogIn(userName, password, curVersion);
        	LogInBaseData user;
        	try{
        		user = service.findUser(input);
        	}
        	catch(Exception e){
        		LogAdp.e(getClass(), "logIn", "during log in", e);
        		return false;
        	}
        	return setGlobalUserData(user);
        }

        private boolean setGlobalUserData(LogInBaseData user){
        	boolean result = false;
        	if(user != null && user.id != null && user.id.length() > 0){
        		SharedPref.setMain(SharedPref.Keys.USER_ACCOUNT, userLogin, curContext);
        		SharedPref.setMain(SharedPref.Keys.USER_KEY, user.id + curVersion, curContext);
        		SharedPref.setMain(SharedPref.Keys.USER_LOGIN, user.login, curContext);
        		SharedPref.setMain(SharedPref.Keys.USER_EMAIL, user.email, curContext);
        		SharedPref.setMain(SharedPref.Keys.NO_ACCEPT_NOTI, user.noAcceptNoti, curContext);
        		SharedPref.setMain(SharedPref.Keys.NO_RESP_NOTI, user.noRespNoti, curContext);
        		SharedPref.setMain(SharedPref.Keys.NOTI_FREQ, user.notiFreq, curContext);
        		//run if this app is started first time, or on other device user set on noti
        		//cancel is not necessery because it is cancel automatic (during get noti in service)
        		if(!user.noAcceptNoti || !user.noAcceptNoti)
        			new Schedule(curContext).run();
        		result = true;
        	}
        	return result;
        }

        private String codePass(String input){
            //in production version here is counted one way hash base on input string
            //remove from here for security reason

            return input;
        }

        private String getAppNameVersion(){
        	String version = null;
    		try {
    			PackageInfo pInfo = getPackageManager().getPackageInfo(getPackageName(), 0);
    			version = pInfo.versionName;
    			if(version != null)
    				version = version.replace('.', ',');
    			else
    				throw new NameNotFoundException("versionName is null");
    		} catch (NameNotFoundException e) {
    			LogAdp.wtf(getClass(), "getAppCodeVersion", "during get app code version", e);
    			version = String.valueOf(Global.defWrongVer);
    		}
    		return Global.verTag + version;
        }
    }

}
