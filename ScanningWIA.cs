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
        private DeviceInfo deviceInfo;

        public ScanningWIA()
        {
            deviceManager = new DeviceManager();
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
            
            /*
            if (deviceManager.DeviceInfos.Count > 0)
            {
                ListBoxScanner.SelectedIndex = 0;

            }
            */
            return scanner;
        }

        void scanImage(String fileName)
        {

        }
    }
}
