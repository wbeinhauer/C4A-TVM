using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;

// GET     h ttp://127.0.0.1:8081/vladimir/settings/{"OS":{"id":"linux"},"solutions":[{"id":"org.gnome.desktop.a11y.magnifier"}]}
//BACK     {"org.gnome.desktop.a11y.magnifier":{"mag-factor":2,"mouse-tracking":"centered","screen-position":"full-screen"}} 

// GET     h ttp://127.0.0.1:8081/sammy/settings/{"OS":{"id":"web"},"solutions":[{"id":"de.fraunhofer.iao.C4A-TVM"}]}
//BACK     {"de.fraunhofer.iao.C4A-TVM":{"highContrastEnabled": true, "fontSize": "big"}} 


namespace ConsoleGET
{
    class Program
    {
        static void Main(string[] args)
            
        {

            
            TVM tvm = new TVM{
                fontsize = "big",
                highContrastEnabled = "true"
            };
                
            TVMSettings t2 = new TVMSettings(tvm);

            string x = JsonConvert.SerializeObject(t2);
            
            Console.WriteLine(x + "  ConsoleGET.Program");

            x = "{\"de.fraunhofer.iao.C4A-TVM\":{\"highContrastEnabled\": false, \"fontSize\": \"middle\"}}";

            Console.WriteLine(x + "  middle");

            TVMSettings t1 = JsonConvert.DeserializeObject<TVMSettings>(x);
            String f1 = t1.TVM.fontsize;
            String c1 = t1.TVM.highContrastEnabled;

            if (f1 != null)
            {
                Console.WriteLine("f1 = " + f1);
            }
            else
            {
                Console.WriteLine("f1 = null");
            }


            if (c1 != null)
            {
                Console.WriteLine("c1 = " + c1);
            }
            else
            {
                Console.WriteLine("c1 = null");
            }


            Printer printer = new Printer();
            string s = "";
            if (printer != null) { s = printer.ToString(); } //ist trotzdem nicht da auch wenn nicht null.
            Console.WriteLine("s =" + s);
            Console.ReadLine();
            //Bitmap ticket = printer.generateTicket("Typ","Start","Ziel","5,50");
            //printer.printTicket(ticket);
            IDScanner scanner = new IDScanner();
            Console.WriteLine("scanner.scan =" + scanner.scan());
            Console.ReadLine();
            

            
        }

        public void readJson()
        {
            string url_to_flowmanager = "http://flowmanager.gpii.net/";
            string usertoken = "sammy";
            string command = "/settings"; //Flow Manager takes login, logout, save, update an settings requests
            string device_info = "/%7B%22OS%22:%7B%22id%22:%22linux%22%7D,%22solutions%22:%5B%7B%22id%22:%22org.gnome.desktop.a11y.magnifier%22%7D%5D%7D";
            string requested_url = "";
            //"{\"OS\":{\"id\":\"linux\"},\"solutions\":[{\"id\":\"org.gnome.desktop.a11y.magnifier\"}]}"; //for testing only later: see below
            //string device_info = "{\"OS\":{\"id\":\"window\"},\"solutions\":[{\"id\":\"org.H+W+FraunhoferIAO.C4A.tvm\"}]}"; //what the solution id might look like
            string json = "{\"org.gnome.desktop.a11y.magnifier\":{\"mag-factor\":2,\"mouse-tracking\":\"centered\",\"screen-position\":\"full-screen\"}}";

            //string json1 = "{\"org.gnome.desktop.a11y.magnifier\":{\"mag-factor\":2,\"mouse-tracking\":\"centered\",\"screen-position\":\"full-screen\"}}";
            string json2 = "{\"de.fraunhofer.iao.C4A-TVM\":{\"fontsize\":\"big\",\"contrast\":\"yellow-black\",\"screen-position\":\"full-screen\"}}";
            //string json = json1;


            // BUILD STRUCTURE FOR TVM SETTINGS: de.fraunhofer.iao.C4A-TVM - "", fontsize , contrast,...


            //JsonConvert.DeserializeObject<List<CustomerJson>>(json);

            Console.WriteLine("Hello Cloud! Who's there?");
            //usertoken = Console.ReadLine();
            Console.WriteLine("Hello " + usertoken + "! ");
            Console.ReadLine();
            //try{
            //  requested_url = Path.Combine(url_to_flowmanager, usertoken, command) + device_info;
            //}catch (ArgumentException argEx){
            //  Console.WriteLine("Exception message: " + argEx.Message);
            //}             
            requested_url = url_to_flowmanager + usertoken + command + device_info;
            Console.WriteLine("requested url = " + requested_url);
            Console.ReadLine();

            //string response = makeRequest(requested_url);
            string response = "{\"org.gnome.desktop.a11y.magnifier\":{\"mag-factor\":2,\"mouse-tracking\":\"centered\",\"screen-position\":\"fullscreen\"}}";
            GnomeMagnifierSettings serialisedSettings = new GnomeMagnifierSettings();
            serialisedSettings.JSONobject = "org.gnome.desktop.a11y.magnifier";
            serialisedSettings.magFactor = "2";
            serialisedSettings.mouseTracking = "centered";
            serialisedSettings.screenPosition = "fullscreen";
            String s = JsonConvert.SerializeObject(serialisedSettings);
            Console.WriteLine(serialisedSettings + s);
            Console.ReadLine();


            GnomeMagnifierSettings parsedSettings = JsonConvert.DeserializeObject<GnomeMagnifierSettings>(response);
            Console.WriteLine(response);
            Console.WriteLine(" magFactor " + parsedSettings.magFactor + " mouseTracking " + parsedSettings.mouseTracking + " screenPosition " + parsedSettings.screenPosition);
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

    public class Solution
    {

        public string name { get; set; } //2
        public string id { get; set; } // id?
        public Context[] contexts { get; set; }  //centered
        public string screenPosition { get; set; } //fullscreen

    }

    public class Context
    {

        public  OS os { get; set; } //win32 web linux
    }

    public class OS
    {

        public string id { get; set; } //win32 web linux
    }

    

    public class TVMSettings
    {
        
        public TVMSettings(TVM tvm){
            this.TVM = tvm;
        }

        [JsonProperty("de.fraunhofer.iao.C4A-TVM")]
        public TVM TVM {get; set;}
    }


    public class TVM
    {
        //[JsonProperty("name")]
        public string highContrastEnabled {get; set;}
        //[JsonProperty("name")]
        public string fontsize {get; set;}


    }

    public class GnomeMagnifierSettings
    {

        public string JSONobject { get; set; } // id?
        public string magFactor { get; set; } //2
        public string mouseTracking { get; set; }  //centered
        public string screenPosition { get; set; } //fullscreen

    }
 

}
