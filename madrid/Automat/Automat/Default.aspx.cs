using System;
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

namespace C4A_demo_2
{
    public partial class _Default : System.Web.UI.Page
    {

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

            HyperLink4.CssClass = "button_contrast";
            runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
            runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
            runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
            
        }

        protected void HyperLinkB_Click(object sender, EventArgs e)
        {
            // runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_crazy','slow');$('.button_crazy').switchClass('button_crazy','button_contrast','fast');");
            runjQueryCode("$('.startseite_C4A').switchClass('startseite_C4A','startseite_C4A_contrast','slow');");

            
            runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
            runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
            runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
            runjQueryCode("$('.area_header').switchClass('area_header','area_header_contrast','slow');");
            IDScanner scanner = new IDScanner();
            String s = scanner.scan();
            HyperLinkB.Text = s;
        }


        protected void HyperLinkC_Click(object sender, EventArgs e)
        {
            // runjQueryCode("$('.button_contrast').switchClass('button_contrast','button_crazy','slow');$('.button_crazy').switchClass('button_crazy','button_contrast','fast');");
            runjQueryCode("$('.startseitemvv').switchClass('startseitemvv','startseitemvv_contrast','slow');");
            runjQueryCode("$('.area_header').switchClass('area_header','area_header_contrast','slow');");
           
            runjQueryCode("$('.button_normal').switchClass('button_normal','button_contrast','slow');");
            runjQueryCode("$('.button_eng_normal_eservice').switchClass('button_eng_normal_eservice','button_eng_contrast_eservice','slow');");
            runjQueryCode("$('.button_eng_normal').switchClass('button_eng_normal','button_eng_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal').switchClass('button_hoch_normal','button_hoch_contrast','slow');");
            runjQueryCode("$('.button_hoch_normal_touristen').switchClass('button_hoch_normal_touristen','button_hoch_contrast_touristen','slow');");
            
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





        
        protected void Page_Load(object sender, EventArgs e)
        {
           
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
