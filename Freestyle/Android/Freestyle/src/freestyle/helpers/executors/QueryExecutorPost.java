package freestyle.helpers.executors;

import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;

import freestyle.adapters.BooleanAdapter;


public class QueryExecutorPost extends QueryExecutor{
	private String methodName;
	
	public QueryExecutorPost(final String methodName, BooleanAdapter isWrongVer){
		super(isWrongVer);
		this.methodName = methodName;
	}
	
	public String execute(final String jsonString) throws Exception{		
		String result = null;
		if(jsonString != null && !jsonString.isEmpty()){
			final String url = connectionUrl + methodName;
			HttpPost post = new HttpPost(url);
			StringEntity jsonInSe = new StringEntity(jsonString, "UTF-8");
			post.setEntity(jsonInSe);
			result = runExe(post);
		}
		return result;
	}
}
