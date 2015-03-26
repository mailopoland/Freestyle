package freestyle.helpers.outin;

import android.content.Context;
import android.os.AsyncTask;
import freestyle.adapters.BooleanAdapter;
import freestyle.helpers.DialogBuilder;
import freestyle.repositories.RepoHandler;


//always use it instead of AsyncTask, it check client correct version 
public  abstract class AsyncGen<ARGS, PROG, OUT> extends AsyncTask<ARGS, PROG, OUT>{
	
	protected final Context curContext; 
	private final BooleanAdapter isWrongVer;
	private final RepoHandler repoHandler;
	
	public AsyncGen(Context curContext) {
		this.curContext = curContext;
		this.isWrongVer = new BooleanAdapter();
		this.repoHandler = new RepoHandler(this.isWrongVer);
	}
	
	//child should use it instead of onPostExecute, it can do nothing
	protected abstract void replaceOnPostExecute(OUT output);
	
	protected DialogBuilder dialog(){
		return new DialogBuilder(curContext);
	}
	
	@Override  
    protected void onPostExecute(OUT output)  
    {  
		if(isWrongVer.value)
			wrongVersionAction();
		else
			replaceOnPostExecute(output);
    }
	
	protected void wrongVersionAction(){
		dialog().wrongClientVer();
	}

	protected RepoHandler getRepoHandler() {
		return repoHandler;
	}
	
	protected boolean getIsWrongVerValue(){
		return this.isWrongVer.value;
	}

}
