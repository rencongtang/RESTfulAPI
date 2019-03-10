import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;


public class Main {
    public static void main(String[] args) throws Exception
    {
        MyClient myClient = new MyClient();
        myClient.GetRequest("http://www.google.com/search?q=mkyong");
    }


}
