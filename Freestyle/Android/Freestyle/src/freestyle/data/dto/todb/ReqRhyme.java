package freestyle.data.dto.todb;

public class ReqRhyme {
	public ReqRhyme(String userKey, int rhymeId, String curValue, boolean isNext){
		this.userKey = userKey;
		this.rhymeId = rhymeId;
		this.curValue = curValue;
		this.isNext = isNext;
	}
	public String userKey;
	public int rhymeId;
	public String curValue;
	public boolean isNext;
}
