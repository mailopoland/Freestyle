package freestyle.adapters;

import android.util.Log;

public class LogAdp {
	public static void e(Class<?> cl, String tag, String msg){
		Log.e(getTag(cl,tag), msg);
	}
	public static void d(Class<?> cl, String tag, String msg){
		Log.d(getTag(cl,tag), msg);
	}
	public static void v(Class<?> cl, String tag, String msg){
		Log.v(getTag(cl,tag), msg);
	}
	public static void i(Class<?> cl, String tag, String msg){
		Log.i(getTag(cl,tag), msg);
	}
	public static void w(Class<?> cl, String tag, String msg){
		Log.w(getTag(cl,tag), msg);
	}
	public static void wtf(Class<?> cl, String tag, String msg){
		Log.wtf(getTag(cl,tag), msg);
	}
	
	public static void e(Class<?> cl, String tag, String msg, Throwable e){
		Log.e(getTag(cl,tag), msg, e);
	}
	public static void d(Class<?> cl, String tag, String msg, Throwable e){
		Log.d(getTag(cl,tag), msg, e);
	}
	public static void v(Class<?> cl, String tag, String msg, Throwable e){
		Log.v(getTag(cl,tag), msg, e);
	}
	public static void i(Class<?> cl, String tag, String msg, Throwable e){
		Log.i(getTag(cl,tag), msg, e);
	}
	public static void w(Class<?> cl, String tag, String msg, Throwable e){
		Log.w(getTag(cl,tag), msg, e);
	}
	public static void wtf(Class<?> cl, String tag, String msg, Throwable e){
		Log.wtf(getTag(cl,tag), msg, e);
	}
	
	private static String getTag (Class<?> cl, String tag){
		return cl.toString() + ":" + tag;
	}
	
}
