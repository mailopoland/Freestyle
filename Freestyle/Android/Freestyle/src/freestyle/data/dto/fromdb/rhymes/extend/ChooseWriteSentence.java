package freestyle.data.dto.fromdb.rhymes.extend;
import java.util.List;

import freestyle.data.dto.fromdb.rhymes.bases.IncompletedViewRhyme;
import freestyle.data.dto.fromdb.rhymes.helpers.ReplyToAccepted;

public class ChooseWriteSentence extends IncompletedViewRhyme{
	public List<ReplyToAccepted> suggestedReplies;
}
