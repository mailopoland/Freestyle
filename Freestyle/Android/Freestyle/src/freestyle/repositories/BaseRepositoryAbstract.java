package freestyle.repositories;

import freestyle.adapters.BooleanAdapter;
import freestyle.helpers.executors.QueryExecutorGet;
import freestyle.helpers.executors.QueryExecutorPost;

public abstract class BaseRepositoryAbstract {
	
	private final BooleanAdapter isWrongVer;
	
	protected BaseRepositoryAbstract(BooleanAdapter isWrongVer){
		this.isWrongVer = isWrongVer;
	}
	
	protected QueryExecutorPost getPostExe(String methodName){
		return new QueryExecutorPost(methodName, isWrongVer);
	}
	
	protected QueryExecutorGet getGetExe(String methodName){
		return new QueryExecutorGet(methodName, isWrongVer);
	}
}
