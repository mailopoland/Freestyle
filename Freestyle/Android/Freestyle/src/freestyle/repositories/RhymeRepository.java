package freestyle.repositories;

import freestyle.adapters.BooleanAdapter;
import freestyle.repositories.interfaces.IRhymeRepository;

public class RhymeRepository extends BaseRepositoryAbstract implements IRhymeRepository{
	
	public RhymeRepository(BooleanAdapter isWrongVer) {
		super(isWrongVer);
	}

	@Override
	public String createNewRhyme(String newRhyme) throws Exception {
		String result = getPostExe("CreateNewRhyme").execute(newRhyme);
		return result;
	}
	
	@Override
	public String addReply(String reply) throws Exception{
		String result = getPostExe("AddReply").execute(reply);
		return result;
	}
	
	@Override
	public String finishRhyme(String rhymeToSave) throws Exception{
		String result =  getPostExe("FinishRhyme").execute(rhymeToSave);
		return result;
	}
	
	@Override
	public String addSentence(String rhymeWithSenToSave) throws Exception {
		String result = getPostExe("AddSentence").execute(rhymeWithSenToSave);
		return result;
	}
	
	@Override
	public String getRhymeAuthorComplSortFinishDate(String reqRhyme) throws Exception{
		String result = getPostExe("GetRhymeAuthorComplSortFinishDate").execute(reqRhyme);
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorComplSortFinishDate(String reqRhyme) throws Exception{
		String result = getPostExe("GetRhymeNoAuthorComplSortFinishDate").execute(reqRhyme);
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorComplSortVoteValue(String reqRhyme)
			throws Exception {
		String result = getPostExe("GetRhymeNoAuthorComplSortVoteValue").execute(reqRhyme);
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorFavComplSortId(String userId, int rhymeId,
			boolean isNext) throws Exception {
		String result = getGetExe("GetRhymeNoAuthorFavComplSortId").execute(userId, String.valueOf(rhymeId), String.valueOf(isNext));
		return result;
	}

	@Override
	public String getRhymeNoAuthorFavIncomplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String result = getGetExe("GetRhymeNoAuthorFavIncomplSortId").execute(userId, String.valueOf(rhymeId), String.valueOf(isNext));
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorOwnResComplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String result = getGetExe("GetRhymeNoAuthorOwnResComplSortId").execute(userId, String.valueOf(rhymeId), String.valueOf(isNext));
		return result;
	}

	@Override
	public String getRhymeNoAuthorOwnResIncomplSortId(String userId,
			int rhymeId, boolean isNext) throws Exception {
		String result = getGetExe("GetRhymeNoAuthorOwnResIncomplSortId").execute(userId, String.valueOf(rhymeId), String.valueOf(isNext));
		return result;
	}
	
	@Override
	public String getRhymeAuthorIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception{
		String result = getGetExe("GetRhymeAuthorIncomplSortId").execute( userId, String.valueOf(rhymeId), String.valueOf(isNext));
		return result;
	}
	
	@Override
	public String getRhymeAuthorIncomplSortSugRepId(String userId, int rhymeId, String curValue,
			boolean isNext) throws Exception {
		String result = getGetExe("GetRhymeAuthorIncomplSortSugRepId").execute( userId, String.valueOf(rhymeId), curValue, String.valueOf(isNext) );
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorIncomplSortId(String userId, int rhymeId, boolean isNext) throws Exception{
		String result = getGetExe("GetRhymeNoAuthorIncomplSortId").execute(userId, String.valueOf(rhymeId), String.valueOf(isNext));
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorIncomplSortModDate(String reqRhyme) throws Exception {
		String result = getPostExe("GetRhymeNoAuthorIncomplSortModDate").execute(reqRhyme);
		return result;
	}
	
	@Override
	public String getRhymeAuthorIncomplUnshown(String userId) throws Exception{
		String result = getGetExe("GetRhymeAuthorIncomplUnshown").execute(userId);
		return result;
	}
	
	@Override
	public String getRhymeNoAuthorUnshown(String userId) throws Exception{
		String result = getGetExe("GetRhymeNoAuthorUnshown").execute(userId);
		return result;
	}
	
	@Override
	public String noti(String userId) throws Exception{
		String result = getGetExe("Noti").execute(userId);
		return result;
	}
	
	@Override
	public String minMsgAmount() throws Exception{
		String result = getGetExe("MinMsgAmount").execute();
		return result;
	}
	
	@Override
	public String addVote(String userId, int rhymeId, String value) throws Exception{
		String result = getGetExe("AddVote").execute(userId, String.valueOf(rhymeId), value);
		return result;
	}

	@Override
	public String makeFavorite(String userId, int rhymeId) throws Exception {
		String result = getGetExe("MakeFavorite").execute(userId, String.valueOf(rhymeId));
		return result;
	}

	@Override
	public String makeNotFavorite(String userId, int rhymeId) throws Exception {
		String result = getGetExe("MakeNotFavorite").execute(userId, String.valueOf(rhymeId));
		return result;
	}
}
