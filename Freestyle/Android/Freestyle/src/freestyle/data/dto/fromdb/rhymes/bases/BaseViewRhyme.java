package freestyle.data.dto.fromdb.rhymes.bases;

import java.util.List;

import freestyle.data.dto.fromdb.rhymes.helpers.SentenceToShow;

public abstract class BaseViewRhyme{
	
	public int rhymeId;
	
	//universal data (snd condition - order by sth other than id)
	public String curValue;
	
	//return: 0 => is author (author can't fav his rhymes), 
    // 1 => is not fav, 
    // 2 => is fav
	public int isFavorited;
	public String title;
	public List<SentenceToShow> sentencesToShow;
	
}
