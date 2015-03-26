package freestyle.activities.rhymes.spec;

import freestyle.activities.rhymes.abstracts.ChooseWriteSentenceActivity;
import freestyle.repositories.RepoHandler;

public final class ChooseWriteSenStdActivity extends ChooseWriteSentenceActivity{
	@Override
	protected String downloadRhymeMethod(boolean isNext, RepoHandler repoHandler) throws Exception{
		return repoHandler.getRhymeAuthorIncomplSortSugRepId(curUserId(), rhymeId(), rhymeCurValue(), isNext);
	}
}
