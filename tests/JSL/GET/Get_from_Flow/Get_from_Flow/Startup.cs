using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Net;
using System.IO;
using System.Text;

[assembly: OwinStartup(typeof(Get_from_Flow.Startup))]

namespace Get_from_Flow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            string url_to_flowmanager = "flowmanager.gpii.net";
            string usertoken = "sammy";
            string command = "settings"; //Flow Manager takes login, logout, save, update an settings requests
            string device_info = "{\"OS\":{\"id\":\"linux\"},\"solutions\":[{\"id\":\"org.gnome.desktop.a11y.magnifier\"}]}"; //for testing only later: see below
            //string device_info = "{\"OS\":{\"id\":\"window\"},\"solutions\":[{\"id\":\"org.H+W+FraunhoferIAO.C4A.tvm\"}]}"; //what the solution id might look like
            string requested_url = url_to_flowmanager + "\\" + usertoken + "\\" + command + "\\" + device_info;

            Console.WriteLine(requested_url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requested_url);

            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine("Content length is {0}", response.ContentLength);
            Console.WriteLine("Content type is {0}", response.ContentType);

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            Console.WriteLine("Response stream received.");
            Console.WriteLine(readStream.ReadToEnd());
            response.Close();
            readStream.Close();

        }
    }
}
