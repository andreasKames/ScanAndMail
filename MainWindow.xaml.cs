using System;
//using System.Collections.Generic;
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
        private string Directory;
        private string FileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            var scanningWIA = new ScanningWIA();
            var imagePath = Directory + FileName;
            if (ConfManager.IsDateAdded())
            {                
                imagePath = FileNameWithDate.AddDate(imagePath);
             }
            imagePath = FileNameWithDate.FindFileName(imagePath);
            //Console.WriteLine("imagePath: " + imagePath);
            var imageFile =  scanningWIA.ScanImage(scannerNumber);
            if (imageFile != null)
            {
                imageFile.SaveFile(imagePath);
                Uri uri = new Uri(imagePath);
                ScanImage.Source = new BitmapImage(uri);

                weiterButton.IsEnabled = true;
            }             
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
            
            this.scannerNumber = ConfManager.GetScannerNumber();
            this.Directory = ConfManager.GetDirectory();
            this.FileName = ConfManager.GetFileName();

            //Console.WriteLine("ScannerNr: " + scannerNumber);
            // Wenn Scanner nicht erkannt wurde Scan Button deaktivieren

            if (scannerNumber == -1)
            {
                scanButton.IsEnabled = false;
            }
            else
            {
                scanButton.IsEnabled = true;
            }
        }
    }
}
