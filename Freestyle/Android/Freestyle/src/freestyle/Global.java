package freestyle;

import org.apache.http.HttpStatus;

public class Global {
	
	//use account already exsist account (is created with db)
	public static final boolean prepareLoginForTesting 
	//= true;
	= false;
	
	/* Repo default values for the newest obj from it's category */
	/* --------------------------------------------------------- */
    //id for req the newest object
  	public static final int newestId = 0;
  	//in practise only newestId is important, newestX below have to have only X in correct format
  	public static final String newestDate = "9999-12-31 23:59:59";
  	public static final String newestVote = "0";
  	public static final boolean newestIsNext = true;
  	/* --------------------------------------------------------- */
  	/* END Repo default values for the newest obj from it's category */
  	
  	/* Validators lenght default values */
  	/* --------------------------------------------------------- */
  		// Are in res/values/integers.xml because are used by xmls (views) too
  	/* --------------------------------------------------------- */
  	/* END Validators lenght default values */
  	
  	/* Other default values */
  	/* --------------------------------------------------------- */
  	//2h
	public static final int defaultNotiFreq = 120; 
    public static final char verTag = 'v';
    //it is used if sth go wrong during get version
    public static final double defWrongVer = -1.0;
    public static final String tooOldClientTxt = "\"Too old client version\"";
    public static final int tooOldClientStatCode = HttpStatus.SC_PRECONDITION_FAILED;
    /* --------------------------------------------------------- */
    /* END Other default values */
}
