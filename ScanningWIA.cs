using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace ScanAndMail
{
    internal class ScanningWIA
    {
        private DeviceManager deviceManager;
        private int choosedScanner;

        public ScanningWIA()
        {
            deviceManager = new DeviceManager();

        }

        public void SetChoosedScanner(int choosedScanner)
        {
            if (choosedScanner < 0)
                this.choosedScanner = 1;
            this.choosedScanner = choosedScanner + 1;
            if (choosedScanner > deviceManager.DeviceInfos.Count)
            {
                this.choosedScanner = deviceManager.DeviceInfos.Count;
            }
        }

        public List<String>  ListScanner()
        {
            List<String> scanner = new List<String>();
            
            // Loop through the list of devices
            // ATTENTION: deviceManager.DeviceInfos[i] starts with 1 !!!!
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Skip the device if it's not a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }
                scanner.Add( (string)deviceManager.DeviceInfos[i].Properties["Name"].get_Value());
            }
            
            return scanner;
        }




        void scanImage(String fileName)
        {

        }
    }
}
