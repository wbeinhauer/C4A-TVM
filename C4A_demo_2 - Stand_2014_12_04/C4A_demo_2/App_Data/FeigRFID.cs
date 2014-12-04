using System;
using Microsoft.PointOfService;
using HoeftAndWessel.ServiceObjects.FeigRFIDSO;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;
using System.Text;
using System.Xml;



public class FeigRFID
{
    private PosExplorer explorer;
    private RFIDScanner rfidscanner;
    private string rfidData;

    public FeigRFID()
    {
        PosExplorer explorer = new PosExplorer();
        var deviceCollection = explorer.GetDevices("RFIDScanner");
        RFIDScanner rfidscanner = null;
        foreach (DeviceInfo deviceInfo in deviceCollection)
        {
            if (deviceInfo.ServiceObjectName != "FeigRFID")
                continue;
            rfidscanner = (RFIDScanner)explorer.CreateInstance(deviceInfo);
            break;
        }




    }

    public string scanRFID()
    {

        rfidscanner.Open();
        rfidscanner.Claim(1000);
        rfidscanner.DeviceEnabled = true;
        rfidscanner.DataEvent += new DataEventHandler(rfid_DataEvent);
        rfidscanner.DataEventEnabled = true;

        while (rfidscanner.DeviceEnabled)
        {
            System.Threading.Thread.Sleep(1);
        }

        rfidscanner.Release();
        rfidscanner.Close();
        return rfidData;
    }


    public void rfid_DataEvent(object sender, DataEventArgs e)
    {

    }
}