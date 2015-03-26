package freestyle.data.dto.todb;

public class RhymeWithSenToSave extends RhymeToSave {
	public RhymeWithSenToSave(RhymeToSave rhymeToSave){
		this.replyId = rhymeToSave.replyId;
		this.rhymeId = rhymeToSave.rhymeId;
		this.userKey = rhymeToSave.userKey;
	}
	public RhymeWithSenToSave(){}
	
	public String sentence;
}
