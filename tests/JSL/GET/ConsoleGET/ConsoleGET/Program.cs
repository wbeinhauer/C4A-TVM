using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGET
{
    class Program
    {
        static void Main(string[] args)
            
        {
            
            string url_to_flowmanager = "http://flowmanager.gpii.net/";
            string usertoken = "sammy";
            string command = "/settings"; //Flow Manager takes login, logout, save, update an settings requests
            string device_info = "/%7B%22OS%22:%7B%22id%22:%22linux%22%7D,%22solutions%22:%5B%7B%22id%22:%22org.gnome.desktop.a11y.magnifier%22%7D%5D%7D";
                //"{\"OS\":{\"id\":\"linux\"},\"solutions\":[{\"id\":\"org.gnome.desktop.a11y.magnifier\"}]}"; //for testing only later: see below
            //string device_info = "{\"OS\":{\"id\":\"window\"},\"solutions\":[{\"id\":\"org.H+W+FraunhoferIAO.C4A.tvm\"}]}"; //what the solution id might look like
            string requested_url = "";

            Console.WriteLine("Hallo Cloud! Who's there?");
            //usertoken = Console.ReadLine();
            Console.WriteLine("Hallo "+usertoken+"! ");
            Console.ReadLine();
            //try{
              //  requested_url = Path.Combine(url_to_flowmanager, usertoken, command) + device_info;
            //}catch (ArgumentException argEx){
              //  Console.WriteLine("Exception message: " + argEx.Message);
            //}             
            requested_url = url_to_flowmanager + usertoken + command + device_info;
            Console.WriteLine("requested url = "+requested_url);
            Console.ReadLine();

            string response = makeRequest(requested_url);
                       
            Console.WriteLine(response);
            Console.ReadLine();
            
        }


        public static string makeRequest(String reqUrl)
        {
            String r = "";
            WebRequest request = WebRequest.Create(reqUrl);
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine("HttpWebResponse.StatusDescription: "+((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close(); 
            r = responseFromServer.ToString();
            return r;
        }
    }
}
