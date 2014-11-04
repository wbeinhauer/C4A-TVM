using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.PointOfService;


public class IDNFC
{
    private PosExplorer explorer;
    private RFIDScanner nfc;
    private string Data;

	public IDNFC()
	{
        explorer = new PosExplorer();
        var deviceCollection = explorer.GetDevices("RFIDScanner");
        foreach (DeviceInfo deviceInfo in deviceCollection)
        {
            if (deviceInfo.ServiceObjectName != "FeigRFID")
                continue;
            nfc = (RFIDScanner)explorer.CreateInstance(deviceInfo);
            break;
        }   
	}

    public string scanNFC()
    {
        
        nfc.Open();
        nfc.Claim(1000);
        nfc.DeviceEnabled = true;
        nfc.DataEvent += new DataEventHandler(nfc_DataEvent);
        nfc.DataEventEnabled = true;
        
        while (nfc.DeviceEnabled)
        {
            System.Threading.Thread.Sleep(1);
        }

        nfc.Release();
        nfc.Close();
        return Data;
    }

    public void open()
    {
        nfc.Open();
    }

    public void nfc_DataEvent(object sender, DataEventArgs e)
    {

    }
}