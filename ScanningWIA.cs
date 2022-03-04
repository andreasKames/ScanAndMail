using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using System.Runtime.InteropServices;
using System.Windows;

namespace ScanAndMail
{
    internal class ScanningWIA
    {
        private DeviceManager deviceManager;
        

        public ScanningWIA()
        {
            deviceManager = new DeviceManager();
        }
        
        public List<String>  ListScanner()
        {
            List<String> scannerList = new List<String>();
            
            // Loop through the list of devices
            // ATTENTION: deviceManager.DeviceInfos[i] starts with 1 !!!!
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Skip the device if it's not a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }
                scannerList.Add( (string)deviceManager.DeviceInfos[i].Properties["Name"].get_Value());
            }
            
            return scannerList;
        }

        public ImageFile ScanImage(int scannerNumber)
        {
            CommonDialogClass dlg = new CommonDialogClass();
            try
            {
                var deviceManager = new DeviceManager();
                var scanner = deviceManager.DeviceInfos[scannerNumber + 1].Connect();
                var scannerItem = scanner.Items[1];
                int resolution = 150;
                int width_pixel = 1250;
                int height_pixel = 1700;
                int color_mode = 1;
                ScannerSettings.AdjustScannerSettings(scannerItem, resolution, 0, 0, width_pixel, height_pixel, 0, 0, color_mode);
                object scanResult = dlg.ShowTransfer(scannerItem, WIA.FormatID.wiaFormatJPEG, true);

                if (scanResult != null)
                {
                    ImageFile image = (ImageFile)scanResult;
                    return image;
                }
            }
            catch (COMException e)
            {
                // Convert the error code to UINT
                uint errorCode = (uint)e.ErrorCode;
                String errorMessage;

                // See the error codes
                if (errorCode == 0x80210006)
                {
                    errorMessage = "Der Scanner ist beschaeftigt oder nicht bereit.";
                }
                else if (errorCode == 0x80210064)
                {
                    errorMessage = "Der Scan-Vorgang wurde abgebrochen.";
                  //  Console.WriteLine("The scanning process has been cancelled.");
                }
                else if (errorCode == 0x8021000C)
                {
                    errorMessage = "Falsche Scaneinstellung.";
                    //Console.WriteLine("There is an incorrect setting on the WIA device.");
                }
                else if (errorCode == 0x80210005)
                {
                    errorMessage = "Das Gerät ist offline. Stellen Sie sicher, dass das Gerät an und an den PC angeschlossen ist.";
                    //Console.WriteLine("The device is offline. Make sure the device is powered on and connected to the PC.");
                }
                else if (errorCode == 0x80210001)
                {
                    errorMessage = "Unbekannter Fehler!";
                    //Console.WriteLine("An unknown error has occurred with the WIA device.");
                }
                else
                {
                    errorMessage = "Unbekannter Fehlercode!";
                }
                MessageBox.Show(errorMessage);
            }
            return null;
        }
    }
}
