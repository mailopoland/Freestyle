package freestyle.repositories.interfaces;

public interface IRhymeRepository {
	//return id of new rhyme
	public String createNewRhyme(String newRhyme) throws Exception;
	
	public String addReply(String reply) throws Exception ;
	public String finishRhyme(String rhymeToSave) throws Exception;
	public String addSentence(String rhymeWithSenToSave) throws Exception;
	
	public String getRhymeAuthorComplSortFinishDate(String reqRhyme) throws Exception;
	public String getRhymeNoAuthorComplSortFinishDate(String reqRhyme) throws Exception;
	public String getRhymeNoAuthorComplSortVoteValue(String reqRhyme) throws Exception;
	public String getRhymeNoAuthorFavComplSortId(String userId, int rhymeId, boolean isNext) throws Exception;
	public String getRhymeNoAuthorFavIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception;
	public String getRhymeNoAuthorOwnResComplSortId(String userId, int rhymeId, boolean isNext) throws Exception;
	public String getRhymeNoAuthorOwnResIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception;
	public String getRhymeAuthorIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception;
	public String getRhymeAuthorIncomplSortSugRepId(String userId, int rhymeId, String curValue, boolean isNext) throws Exception;
	public String getRhymeNoAuthorIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception;
	public String getRhymeNoAuthorIncomplSortModDate(String reqRhyme) throws Exception;
	public String getRhymeAuthorIncomplUnshown(String userId) throws Exception;
	public String getRhymeNoAuthorUnshown(String userId) throws Exception;
	
	public String noti(String userId) throws Exception;
	public String minMsgAmount() throws Exception;
	public String addVote(String userKey, int rhymeId, String value) throws Exception;
	public String makeFavorite(String userKey, int rhymeId) throws Exception;
	public String makeNotFavorite(String userKey, int rhymeId) throws Exception;
}
