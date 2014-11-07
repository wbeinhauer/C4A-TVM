<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="C4A_demo_2._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    

<link rel="stylesheet" type="text/css" media="all" href="style.css" />
<link rel="stylesheet" type="text/css" media="all" href="C4A_styles.css"/>

<script type="text/javascript" src="../js/jquery-1.10.2.js"></script>
<script type="text/javascript" src="../js/jquery-ui.js"></script>



</head>
<body>
    <form id="form1" runat="server">


<div id="display" class="startseite_C4A center">

    <div id="headline">
    	<div id="headcontainer">
			<div id="zeiticon" class="floatleft"><img src="../img/clock.png" width="22" height="22" alt="clock"/></div>
            <div id="timeelement" class="time">09:12:12</div>
            <div id="tageelement" class="time bold">Mo</div>
            <div id="dateelement" class="time">16.7.2012</div>
    	</div>
    </div>

    <div id="container">
        <div id="links">
        <div id="textfeld"><div style="area_header">Alle Fahrkarten</div>MVG Bereich</div>
        <div style="height:105px">
               <asp:HyperLink runat="server" id="HyperLink6" NavigateUrl="#" class="button_normal"><div id="icon"><img src="../img/lupe.png" width="42" height="50" alt="lupe"/></div><div style="padding-left: 30px">Zielhaltestelle</div></asp:HyperLink>

        </div>
        <asp:Label ID="Ueberschrift" runat="server" ><div style="area_header">Häufige Ziele</div></asp:Label>
		<asp:HyperLink runat="server" id="muenchenbutton" NavigateUrl="#" class="button_normal">München Flughafen</asp:HyperLink>
		<asp:HyperLink runat="server" id="olympiabutton" NavigateUrl="#" class="button_normal">Olympiastadion</asp:HyperLink>
        <asp:HyperLink runat="server" id="ostbahnhofbutton" NavigateUrl="#" class="button_normal">Ostbahnhof</asp:HyperLink>
        
 
        
        </div>
        <div id="leerspalte"></div>
        <div id="mitte">

        <div id="textfeld_mitte"><div style="area_header">Fahrkartenkauf</div><br /></div>
        <asp:HyperLink runat="server" id="HyperLink1" NavigateUrl="#" class="button_normal">Einzelfahrkarten</asp:HyperLink>
        
		<asp:HyperLink runat="server" id="HyperLink2" NavigateUrl="#" class="button_normal">Tageskarten</asp:HyperLink>
        <asp:HyperLink runat="server" id="HyperLink3" NavigateUrl="#" class="button_normal">CityTourCard</asp:HyperLink>
        <asp:HyperLink runat="server" id="HyperLink4" NavigateUrl="#" class="button_normal" onclick="ConnectToData">Regionalzug</asp:HyperLink>
        <br /><br />
        <asp:HyperLink runat="server" id="HyperLink5" NavigateUrl="#" class="button_normal">Andere Fahrkarten</asp:HyperLink>
        </div>
 
        <div id="leerspalte"></div>
        
        
        <div id="rechts">
        <div id="textfeld_rechts"><div style="area_header">Service</div><br /></div>
       
        <asp:LinkButton runat="server" id="HyperLinkB" NavigateUrl="#" class="button_eng_normal_eservice" Text="Service"
                    onclick="HyperLinkB_Click"><div style="padding-left: 30px; vertical-align:middle" >e-Service</div></asp:LinkButton> 
                    
        <asp:LinkButton runat="server" id="HyperLinkC" NavigateUrl="#" 
                    class="button_eng_normal_eservice" 
                    onclick="HyperLinkC_Click">Week-End Special</asp:LinkButton>   
        
        <asp:HyperLink runat="server" id="HyperLink7" NavigateUrl="#" OnClientClick="btnAsynchPostback_Click" class="button_hoch_normal_touristen"><div style="position: relative; top: 38px; left: 8px;">Tourist</div></asp:HyperLink>       
        
        
 
        
    
    
             
        <asp:ScriptManager ID="sm" runat="server">
        </asp:ScriptManager>
        
        <asp:UpdatePanel ID="upPnl" runat="server" Visible="false">
        
        <ContentTemplate>
 
        <asp:Button ID="btnAsynchPostback2" runat="server"  
                    CssClass="button_eng_normal" Text="Service"
                    OnClick="btnAsynchPostback_Click"  visible="false" />
        </ContentTemplate>

        
        </asp:UpdatePanel>
    
</div>


    </form>
</body>
</html>
