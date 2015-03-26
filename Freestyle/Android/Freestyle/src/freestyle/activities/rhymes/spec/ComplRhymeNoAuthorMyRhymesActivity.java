package freestyle.activities.rhymes.spec;

import freestyle.activities.rhymes.abstracts.CompletedRhymeNoAuthorActivity;
import freestyle.repositories.RepoHandler;

public final class ComplRhymeNoAuthorMyRhymesActivity extends CompletedRhymeNoAuthorActivity{

	@Override
	protected String downloadRhymeMethod(boolean isNext, RepoHandler repoHandler) throws Exception {
		return repoHandler.getRhymeNoAuthorOwnResComplSortId(curUserId(), rhymeId(), isNext);
	}

}
