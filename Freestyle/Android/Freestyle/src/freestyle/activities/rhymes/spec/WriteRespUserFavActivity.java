package freestyle.activities.rhymes.spec;

import freestyle.activities.rhymes.abstracts.WriteRespondUserActivity;
import freestyle.repositories.RepoHandler;

public final class WriteRespUserFavActivity extends WriteRespondUserActivity{

	@Override
	protected String downloadRhymeMethod(boolean isNext, RepoHandler repoHandler) throws Exception {
		return repoHandler.getRhymeNoAuthorFavIncomplSortId(curUserId(), rhymeId(), isNext);
	}

}
