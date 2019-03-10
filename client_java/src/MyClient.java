import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

///This is a example REST Api client using Apache technology
public class MyClient {

    private String USER_AGENT = "";
    private String target_url = "";

    public void SetUserAgent(String agent)
    {
        this.USER_AGENT = agent;
        System.out.printf("The Client User Agent is %s now\n", agent);
    }
    public void SetTaretUrl(String target_url)
    {
        this.target_url = target_url;
        System.out.printf("The target Url is %s now\n", target_url);
    }

    public void getRequest(String url) throws Exception
    {
        this.target_url = url;
        {
            getRequest();
        }

    }

    public void getRequest() throws Exception
    {


        if (this.target_url == "")
        {
            this.target_url = "http://www.google.com/search?q=developer";
        }

        HttpClient client = new DefaultHttpClient();
        HttpGet request = new HttpGet(this.target_url);

        //now let's add a request header
        request.addHeader("User-Agent", USER_AGENT);

        //get the response code to check out does it works well

        HttpResponse response = client.execute(request);

        System.out.printf("Sending Get request to the server to %s\n", target_url);
        System.out.printf("The response code is + %d\n", response.getStatusLine().getStatusCode());

        //Now let's read the result be send from the server(should I use async later?)
        BufferedReader in = new BufferedReader(
                //get the input stream from the connection and save the stream into a buffer
                new InputStreamReader(response.getEntity().getContent())
        );
        String inputLine = "";
        StringBuffer result = new StringBuffer();

        while ((inputLine = in.readLine()) != null)
        {
            result.append(inputLine);
        }
        in.close();

        System.out.println("Now the result of GET is" + response.toString());

    }

    private void postRequest(String target_url) throws Exception
    {
        this.target_url = target_url;
        postRequest();
    }


    private void postRequest() throws Exception
    {
        if (this.target_url == "")
        {
            this.target_url = "https://selfsolve.apple.com/wcResults.do";
        }

        URL obj = new URL(target_url);
        HttpURLConnection connect = (HttpURLConnection) obj.openConnection();

        //add a request header


    }


}
