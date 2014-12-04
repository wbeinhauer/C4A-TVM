using HoeftAndWessel;
using Microsoft.PointOfService.BaseServiceObjects;
using Microsoft.PointOfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HoeftAndWessel.UposDevicesLib;
using HoeftAndWessel.UposDevicesLib.ViewModels.LogVMs;


using System.Configuration;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;



public class FeigRFID: RFIDScannerBase
 
{
    private PosExplorer explorer;
    private FeigRFID rfid;
    private string Data;

	public FeigRFID()
	{
        PosExplorer explorer = new PosExplorer();
        var deviceCollection = explorer.GetDevices("RFIDScanner");
        FeigRFID rfid = null;
        foreach (DeviceInfo deviceInfo in deviceCollection)
        {
            if (deviceInfo.ServiceObjectName != "FeigRFID")
                continue;
            rfid = (FeigRFID)explorer.CreateInstance(deviceInfo);
            break;
        }
        
        

        
	}
    public override DirectIOData DirectIO(int command, int data, object obj)
    {
        DirectIOData a= new DirectIOData() ;
        return a;
    }
    public override void LockTag(byte[] tagId, int timeout, byte[] password)
    {
        
    }
    public override void WriteTagId(byte[] sourceTagId, byte[] destinationTagId, int timeout, byte[] password)
    {
        
    }
    public override void DisableTag(byte[] tagId, int timeout, byte[] password)
    {
        
    }
    public override void WriteTagDataImpl(byte[] tagId, byte[] userData, int start, int timeout, byte[] password, DateTime startTime)
    {
        
    }

    public string scanNFC()
    {
        
        rfid.Open();
        rfid.Claim(1000);
        rfid.DeviceEnabled = true;
        rfid.DataEvent += new DataEventHandler(nfc_DataEvent);
        rfid.DataEventEnabled = true;
        
        while (rfid.DeviceEnabled)
        {
            System.Threading.Thread.Sleep(1);
        }

        rfid.Release();
        rfid.Close();
        return Data;
    }


    public void nfc_DataEvent(object sender, DataEventArgs e)
    {

    }
}