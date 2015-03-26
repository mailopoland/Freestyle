package freestyle.data.app;

public enum NotiEnum {
	RESPOND(1),
	ACCEPT(2),
	NEED_NEW_VER(3);
	
	private final int value;
	
	private NotiEnum(final int value){
		this.value = value;
	}
	
	public int getValue(){
		return value;
	}
}
