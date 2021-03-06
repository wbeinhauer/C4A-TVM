﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using Microsoft.PointOfService.Legacy;
using Microsoft.PointOfService.Management;
using Microsoft.PointOfService;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Threading;

namespace C4A_demo_2
{
    public partial class _Default : System.Web.UI.Page
    {
        
        public delegate void scanning_detected(string s);
        public event scanning_detected Raise_scanning_detected;

        private string getjQueryCode(string jsCodetoRun)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine(jsCodetoRun);
            sb.AppendLine(" });");

            return sb.ToString();
        }
        
        
        
        
        private void runjQueryCode(string jsCodetoRun)
        {

            ScriptManager requestSM = ScriptManager.GetCurrent(this);
            if (requestSM != null && requestSM.IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this,
                                                        typeof(Page),
                                                        Guid.NewGuid().ToString(),
                                                        getjQueryCode(jsCodetoRun),
                                                        true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page),
                                                       Guid.NewGuid().ToString(),
                                                       getjQueryCode(jsCodetoRun),
                                                       true);
            }
        }



        protected void btnAsynchPostback_Click(object sender, EventArgs e)
        {
            // runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_crazy','slow');$('.button_crazy').switchClass('button_crazy','button_contrast','fast');");
            runjQueryCode("$('.startseite_C4A').switchClass('startseite_C4A','startseite_C4A_contrast','slow');");
            runjQueryCode("$('.time').switchClass('time','time_contrast','slow');");
            HyperLink4.CssClass = "button_contrast";
            runjQueryCode("$('.zeiticon').switchClass('zeiticon','zeiticon_contrast','slow');");
            runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
            runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
            runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
            
        }

        public void HyperLinkB_Click(object sender, EventArgs e)
        {
            // runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_crazy','slow');$('.button_crazy').switchClass('button_crazy','button_contrast','fast');");
            runjQueryCode("$('.startseite_C4A').switchClass('startseite_C4A','startseite_C4A_contrast','slow');");
            runjQueryCode("$('.zeiticon').switchClass('zeiticon','zeiticon_contrast','slow');");
            runjQueryCode("$('.time').switchClass('time','time_contrast','slow');");
            runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
            runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
            runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
            runjQueryCode("$('.area_header').switchClass('area_header','area_header_contrast','slow');");
        }


        protected void HyperLinkC_Click(object sender, EventArgs e)
        {

            
            
            

            // runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_crazy','slow');$('.button_crazy').switchClass('button_crazy','button_contrast','fast');");
            // runjQueryCode("$('.startseitemvv').switchClass('startseitemvv','startseitemvv_contrast','slow');");
            // runjQueryCode("$('.area_header').switchClass('area_header','area_header_contrast','slow');");
            // runjQueryCode("$('.zeiticon').switchClass('zeiticon','zeiticon_contrast','slow');");
            // runjQueryCode("$('.time').switchClass('time','time_contrast','slow');");
            // runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
            // runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
            // runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
            // runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
            // runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");


            
        }

        protected void startButton_Click(object sender, EventArgs e)
        {

            
                Thread scanning = new Thread(startScanner);
                
              scanning.Start();
                
           



        }

        private void on_scanning_detected(string BarcodeData)
        {

            //this.Invoke((scanning_detected)delegate
            //{
           //     startButton.Visible = false;
           // });
         
            
           /* string Data = BarcodeData;
            if (Data.Contains("la"))
            {
                runjQueryCode("$('.startseite_C4A').switchClass('startseite_C4A','startseite_C4A_contrast','slow');");
                runjQueryCode("$('.zeiticon').switchClass('zeiticon','zeiticon_contrast','slow');");
                runjQueryCode("$('.time').switchClass('time','time_contrast','slow');");
                runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
                runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
                runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
                runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
                runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
                runjQueryCode("$('.area_header').switchClass('area_header','area_header_contrast','slow');");

            }


            if (Data.Contains("lo"))
            {
                runjQueryCode("$('.startseite_C4A_contrast').switchClass('startseite_C4A_contrast','startseite_C4A','slow');");
                runjQueryCode("$('.zeiticon_contrast').switchClass('zeiticon_contrast','zeiticon','slow');");
                runjQueryCode("$('.time_contrast').switchClass('time_contrast','time','slow');");
                runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_normal','slow');");
                runjQueryCode("$('.button_eng_contrast_eservice').switchClass('button_eng_contrast_eservice','button_eng_normal_eservice','slow');");
                runjQueryCode("$('.button_eng_contrast').switchClass('button_eng_contrast','button_eng_normal','slow');");
                runjQueryCode("$('.button_hoch_contrast').switchClass('button_hoch_contrast','button_hoch_normal','slow');");
                runjQueryCode("$('.button_hoch_contrast_touristen').switchClass('button_hoch_contrast_touristen','button_hoch_normal_touristen','slow');");
                runjQueryCode("$('.area_header_contrast').switchClass('area_header_contrast','area_header','slow');");

            }
            */
        }
        
        void startScanner()
        {
            
            IDScanner scanner = new IDScanner();
                
                //bool scanEnable = true;
                //int counter = 3;
                
               // while (counter >= 0)
                //{
                string Data = scanner.scan();
                //counter = counter - 1;
                
               // if (Raise_scanning_detected != null)
                    //Raise_scanning_detected(Data);
                
           
        }

        public void ConnectToData()
        {
            System.Data.Odbc.OdbcConnection conn =
                new System.Data.Odbc.OdbcConnection();
            // TODO: Modify the connection string and include any
            // additional required properties for your database.
            conn.ConnectionString = "Dsn=C4A;dbq=C:/USERS/WBEINHAU/DROPBOX/AUTOMATEN/C4A/C4A_DEMO_2/C4A_DEMO_2/APP_DATA/C4A_Ressources_3.accdb;driverid=25;fil=MS Access;";
            try
            {
                conn.Open();

                HyperLink4.Text = conn.GetSchema().ToString();

                // Process data here.
            }
            catch (Exception ex)
            {
                HyperLink4.Text = "Schnubbeldibums";
            }
            finally
            {
                conn.Close();
            }
        }


       public void main()
        {
           
                IDScanner scanner = new IDScanner();
                
                //bool scanEnable = true;
                int counter = 3;
                
                while (counter >= 0)
                {
                string Data = scanner.scan();
                counter = counter - 1;

                if (Data.Contains("la"))
                {
                    runjQueryCode("$('.startseite_C4A').switchClass('startseite_C4A','startseite_C4A_contrast','slow');");
                    runjQueryCode("$('.zeiticon').switchClass('zeiticon','zeiticon_contrast','slow');");
                    runjQueryCode("$('.time').switchClass('time','time_contrast','slow');");
                    runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
                    runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
                    runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
                    runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
                    runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
                    runjQueryCode("$('.area_header').switchClass('area_header','area_header_contrast','slow');");

                }


                if (Data.Contains("lo"))
                {
                    runjQueryCode("$('.startseite_C4A_contrast').switchClass('startseite_C4A_contrast','startseite_C4A','slow');");
                    runjQueryCode("$('.zeiticon_contrast').switchClass('zeiticon_contrast','zeiticon','slow');");
                    runjQueryCode("$('.time_contrast').switchClass('time_contrast','time','slow');");
                    runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_normal','slow');");
                    runjQueryCode("$('.button_eng_contrast_eservice').switchClass('button_eng_contrast_eservice','button_eng_normal_eservice','slow');");
                    runjQueryCode("$('.button_eng_contrast').switchClass('button_eng_contrast','button_eng_normal','slow');");
                    runjQueryCode("$('.button_hoch_contrast').switchClass('button_hoch_contrast','button_hoch_normal','slow');");
                    runjQueryCode("$('.button_hoch_contrast_touristen').switchClass('button_hoch_contrast_touristen','button_hoch_normal_touristen','slow');");
                    runjQueryCode("$('.area_header_contrast').switchClass('area_header_contrast','area_header','slow');");

                }

                System.Threading.Thread.Sleep(1000);
                }   
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Raise_scanning_detected += new scanning_detected(on_scanning_detected);
        }

        // extralanger timeout
        // automatische Sprachadaption
        // Acessibility Standards
        // Klassenzimmer bei Stromausfall?
        // Nach Kauf: Hinweise, wie man zum Gleis kommt, wo Stufen sind, was Reisende mit schwerem Gepäck anstellen können usw.
        // Voice Reader: Ausweichen / ergänzen mit Voice Ausgabe
        // 
        // 1. CSS Schwarz / weiss , von vdv abweichen, c4a Logo rein
        // 2. Schriftgrösse
        // 3. Sprachauswahl
        // 4. Spracheausgabe
        // 5. Hinweise hinten dran
        // apply_style(NORMAL | CONTRAST | 70IES)
        // add_assistance(WHEELCHAIR | BLIND | SLOW)
        // set_language(GERMAN | ENGLISH | DUTCH)
        // set_area(MUNICH | JAKARTA | MADRID)

    }
}
