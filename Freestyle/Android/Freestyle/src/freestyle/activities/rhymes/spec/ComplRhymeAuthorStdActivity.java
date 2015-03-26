package freestyle.activities.rhymes.spec;

import freestyle.activities.rhymes.abstracts.CompletedRhymeActivity;
import freestyle.repositories.RepoHandler;

public final class ComplRhymeAuthorStdActivity extends CompletedRhymeActivity{

	@Override
	protected String downloadRhymeMethod(boolean isNext, RepoHandler repoHandler) throws Exception {
		return repoHandler.getRhymeAuthorComplSortFinishDate(getReqRhyme(isNext));
	}
}
