package freestyle.activities.rhymes.spec;

import freestyle.activities.rhymes.abstracts.CompletedRhymeNoAuthorActivity;
import freestyle.repositories.RepoHandler;

public final class ComplRhymeNoAuthorTopActivity extends CompletedRhymeNoAuthorActivity{

	@Override
	protected String downloadRhymeMethod(boolean isNext, RepoHandler repoHandler) throws Exception {
		return repoHandler.getRhymeNoAuthorComplSortVoteValue(getReqRhyme(isNext));
	}

}
