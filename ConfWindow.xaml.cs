using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using NSane;
using WIA;

namespace ScanAndMail
{
    /// <summary>
    /// Interaktionslogik für ConfWindow.xaml
    /// </summary>
    public partial class ConfWindow : Window
    {
        public ConfWindow()
        {
            InitializeComponent();
        }

        private void ConfWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a DeviceManager instance
            var deviceManager = new DeviceManager();

            // Loop through the list of devices
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                // Skip the device if it's not a scanner
                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }
                ListBoxScanner.Items.Add( deviceManager.DeviceInfos[i].Properties["Name"].get_Value() );
                
                // Print something like e.g "WIA Canoscan 4400F"
                //Console.WriteLine(deviceManager.DeviceInfos[i].Properties["Name"].get_Value());
                // e.g Canoscan 4400F
                //Console.WriteLine(deviceManager.DeviceInfos[i].Properties["Description"].get_Value());
                // e.g \\.\Usbscan0
                //Console.WriteLine(deviceManager.DeviceInfos[i].Properties["Port"].get_Value());
            }
            if (deviceManager.DeviceInfos.Count > 0)
            {
                ListBoxScanner.SelectedIndex = 0;
            }

        }
    }
}
