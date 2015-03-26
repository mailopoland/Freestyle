package freestyle.helpers;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.Context;

/**
 * This class uses the AccountManager to get info about current user's google account.
 */
public class UserGoogleInfoFetcher {

  final Context context; 
  
  public UserGoogleInfoFetcher(Context context){
	  this.context = context;
  }
	
	//return null if no google accounts
  public CharSequence[] getAccountsNames() {
    
    Account[] accounts = getAccounts();
    
    if(accounts.length > 0){
    	CharSequence result[] = new CharSequence[accounts.length];
    	for(int i = 0; i < accounts.length ; i++)
    		result[i] = accounts[i].name;
    	return result;
    }
    else
    	return null;
  }
  //check that account is still on device
  public boolean accountIsOnDevice(String name){
	  if(name != null && !name.isEmpty()){
		  Account[] accounts = getAccounts();
		  if(accounts.length > 0){
			  for(Account acc : accounts){
				  if(name.equals(acc.name))
					  return true;
			  }
		  }
	  }
	  return false;
  }
  
  private Account[] getAccounts() {
	AccountManager accountManager = AccountManager.get(context); 
    Account[] accounts = accountManager.getAccountsByType("com.google");
    return accounts;
  }
}
