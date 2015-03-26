package freestyle.helpers.executors;

import org.apache.http.client.methods.HttpGet;

import freestyle.adapters.BooleanAdapter;


public class QueryExecutorGet extends QueryExecutor{
	
	
	private final String methodName;
	
	public QueryExecutorGet(final String methodName, BooleanAdapter isWrongVer){
		super(isWrongVer);
		this.methodName = methodName;
	}
	
	public String execute(String... args) throws Exception{
		String url = this.createUrl(args);
		HttpGet get = new HttpGet(url);
		String result = runExe(get);
		return result;
	}
	
	private String createUrl(String... args){
		String result = null;
		
		StringBuilder builder = new StringBuilder(connectionUrl);
		builder.append(methodName);
		for( int i = 0; i < args.length; i++){
			builder.append('/');
			builder.append(args[i]);
		}
		result = builder.toString();
		
		return result;
	}
}
