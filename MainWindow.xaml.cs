using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIA;

namespace ScanAndMail
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int scannerNumber;
        private string PDF_Dir;
        private string PDF_FileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            String imageFileName = "Temp.jpg";
            var deviceManager = new DeviceManager();
            var scanner = deviceManager.DeviceInfos[scannerNumber].Connect();
            var scannerItem = scanner.Items[1];
            var imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatJPEG);


            imageFile.SaveFile(PDF_Dir + imageFileName);
            Uri uri = new Uri(PDF_Dir + PDF_FileName);
            ScanImage.Source = new BitmapImage(uri);
            weiterButton.IsEnabled = true; 
        }

        private void WeiterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EinstellungenButton_Click(object sender, RoutedEventArgs e)
        {
            ConfWindow confWindow = new ConfWindow();
            confWindow.Show();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Programmiert von\nAndreas Kames");
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Activated");
            this.scannerNumber = Convert.ToInt32( ConfigurationManager.AppSettings.Get("scannerNumger") );
            this.PDF_Dir = ConfigurationManager.AppSettings.Get("PDF_Dir");
            this.PDF_FileName = ConfigurationManager.AppSettings.Get("PDF_FileName");

            Console.WriteLine("ScannerNr: " + scannerNumber);
            // Wenn Scanner nicht erkannt wurde Scan Button deaktivieren

            if (scannerNumber == -1)
            {
                scanButton.IsEnabled = false;
            }
        }
    }
}
