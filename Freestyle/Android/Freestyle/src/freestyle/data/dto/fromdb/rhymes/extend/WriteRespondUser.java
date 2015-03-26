package freestyle.data.dto.fromdb.rhymes.extend;

import freestyle.data.dto.fromdb.rhymes.bases.IncompletedViewRhyme;
import freestyle.data.dto.fromdb.users.UserBaseData;

public class WriteRespondUser extends IncompletedViewRhyme{
	public UserBaseData author;
	public int lastSentId;
	public boolean isResponded;
}
