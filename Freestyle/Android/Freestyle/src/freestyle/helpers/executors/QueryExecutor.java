package freestyle.helpers.executors;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpRequestBase;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;
import freestyle.Global;
import freestyle.adapters.BooleanAdapter;
import freestyle.adapters.LogAdp;

public abstract class QueryExecutor{
	
	private final BooleanAdapter isWrongVer;
	
	public QueryExecutor(BooleanAdapter isWrongVer){
		this.isWrongVer = isWrongVer;
	}
	//*** -> hide adress for security reason
	protected final String connectionUrl = 
			"https://***/Service.svc/";
			//"https://192.168.1.110:44302/Service.svc/";
			//"https://192.168.0.3:44302/Service.svc/";
			//"http://localhost:62373/Service.svc/";
			//"https://localhost:44302/Service.svc/";
			//"https://10.0.2.2:44302/Service.svc/";
			//"http://10.0.2.2:62373/Service.svc/";

	
	protected String runExe(HttpRequestBase req) throws Exception{
		String result = null;
		req.setHeader("Accept", "application/json"); 
		req.setHeader("Content-type", "application/json"); 
		//HttpClient httpclient = getForTestNewHttpClient();
		HttpClient httpclient = getDefaultHttpClient();
		HttpResponse response = httpclient.execute(req);
	    StatusLine statusLine = response.getStatusLine();
	    if(statusLine.getStatusCode() == HttpStatus.SC_OK){
	    	if(response != null){
		        ByteArrayOutputStream out = new ByteArrayOutputStream();
		        response.getEntity().writeTo(out);
		        out.close();
		        result = out.toString();
	    	}
	    } 
	    else{
	    	if(statusLine.getStatusCode() == Global.tooOldClientStatCode && response != null){
	    		ByteArrayOutputStream out = new ByteArrayOutputStream();
		        response.getEntity().writeTo(out);
		        out.close();
		        String errorTxt = out.toString();
		        if(errorTxt.equals(Global.tooOldClientTxt)){
		        	isWrongVer.value = true;
		        	result = null;
		        	return null;
		        }
	    	}
			LogAdp.e(getClass(), "execute(String... args)", "Wrong http status code:" + statusLine.getStatusCode());
	        throw new IOException(statusLine.getReasonPhrase());
	    }
	    return result;
	}
	private HttpClient getDefaultHttpClient(){
		HttpParams params = new BasicHttpParams();
        setTimeOut(params);
        return new DefaultHttpClient(params);
	}
	//it is only for local test (not production), it accepts all certificates
//	private HttpClient getForTestNewHttpClient() throws KeyStoreException, KeyManagementException, UnrecoverableKeyException, NoSuchAlgorithmException, CertificateException, IOException {
//            KeyStore trustStore = KeyStore.getInstance(KeyStore.getDefaultType());
//            trustStore.load(null, null);
//
//            SSLSocketFactory sf = new MySSLSocketFactory(trustStore);
//            sf.setHostnameVerifier(SSLSocketFactory.ALLOW_ALL_HOSTNAME_VERIFIER);
//
//            HttpParams params = new BasicHttpParams();
//            setTimeOut(params);
//            HttpProtocolParams.setVersion(params, HttpVersion.HTTP_1_1);
//            HttpProtocolParams.setContentCharset(params, HTTP.UTF_8);
//
//            SchemeRegistry registry = new SchemeRegistry();
//            registry.register(new Scheme("http", PlainSocketFactory.getSocketFactory(), 80));
//            registry.register(new Scheme("https", sf, 443));
//
//            ClientConnectionManager ccm = new ThreadSafeClientConnManager(params, registry);
//
//            return new DefaultHttpClient(ccm, params);    
//    }
//	
	private void setTimeOut(HttpParams httpParameters){
		HttpConnectionParams.setConnectionTimeout(httpParameters, 16000);
		HttpConnectionParams.setSoTimeout(httpParameters, 24000);
	}
}
