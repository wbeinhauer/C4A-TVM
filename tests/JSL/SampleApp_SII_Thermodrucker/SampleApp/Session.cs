using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PointOfService;

namespace SampleApp
{
    public sealed class Session
    {
        private static readonly Session instance = new Session();
        private Session() {}

        public static Session Instance
        {
            get
            {
                return instance;
            }
        }
        public string Ticket { set; get; }
        public int Price { set; get; }
        public int DepositAmount { set; get; }

        PosExplorer explorer = new PosExplorer();
        private CashChanger cashChanger;
        public CashChanger CashChanger 
        {
            get {
                if (cashChanger == null)
                {
                    var deviceCollection = explorer.GetDevices("CashChanger");
                    foreach (DeviceInfo deviceInfo in deviceCollection)
                    {
                        if (deviceInfo.ServiceObjectName != "Hoeft & Wessel Cash Unit")
                            continue;
                        cashChanger = (CashChanger)explorer.CreateInstance(deviceInfo);
                        break;
                    }
                    if (cashChanger == null)
                        return null;
                    cashChanger.Open();
                    cashChanger.Claim(3000);
                    cashChanger.DeviceEnabled = true;
                }
                return cashChanger;
            }
            set
            {
                cashChanger = value;
            }

        }

        private PosPrinter posPrinter;
        public PosPrinter PosPrinter
        {
            get
            {
                if (posPrinter == null)
                {
                    var deviceCollection = explorer.GetDevices("PosPrinter");
                    foreach (DeviceInfo deviceInfo in deviceCollection)
                    {
                        Console.WriteLine("PritNTEr"+deviceInfo.ServiceObjectName);
                        
                    }

                                    }
                return posPrinter;
            }
            set
            {
                posPrinter = value;
            }
        }
    }
}
