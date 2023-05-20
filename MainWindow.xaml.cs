using System;
//using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using Saraff.Twain;

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
        private enum apiType {wia, twain};

        public MainWindow()
        {
            InitializeComponent();

            // Only for Testing
            Uri uri = new Uri(@"E:\Dateien von Andreas\Scans\Testbetrieb.jpg");
            ScanImage.Source = new BitmapImage(uri);
            MailSendenButton.IsEnabled = true;

            /*
            ReceiverTextBox.Text = ConfManager.GetReceiver();
            SubjectTextBox.Text = ConfManager.GetSubject();
            StandardText.Text = ConfManager.GetStandardText(); 
            */
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = Directory + FileName;
            if (ConfManager.IsDateAdded())
            {                
                imagePath = FileNameWithDate.AddDate(imagePath);
            }
            imagePath = FileNameWithDate.FindFileName(imagePath);
            //Console.WriteLine("imagePath: " + imagePath);

            if (ConfManager.GetScanApiType() == ConfManager.ScanApiType.wia)
            {
                var scanningWIA = new ScanningWIA();
                ImageFile imageFile = scanningWIA.ScanImage(scannerNumber);

                if (imageFile != null)
                {
                    //createImageAndShow(imageFile);
                    var tempImage = "tempScanImage.jpg";
                    if (File.Exists(tempImage))
                    {
                        File.Delete(tempImage);
                    }
                    imageFile.SaveFile(tempImage);


                    ImageClass.CompressImage(tempImage, imagePath, 50);
                    // Anzeige im MainWindow
                    ShowPictureAndMailButtons(imagePath);
                }
            }
            else
            {
                Twain32 twain = new Twain32();
                if (twain.SelectSource())
                {
                    twain.OpenDSM();
                    twain.OpenDataSource();
                    twain.Acquire();
                    System.Drawing.Image bmpImage = null;
                    try
                    {
                        bmpImage = twain.GetImage(0);
                    }
                    catch (ArgumentOutOfRangeException) { }
                    if (bmpImage != null)
                    {
                        //createImageAndShow(imageFile);
                        var tempImage = "tempScanImage.jpg";
                        if (File.Exists(tempImage))
                        {
                            File.Delete(tempImage);
                        }
                        bmpImage.Save(tempImage);


                        ImageClass.CompressImage(tempImage, imagePath, 50);
                        ShowPictureAndMailButtons(imagePath);
                    }
                }
            }
        }

        private void ShowPictureAndMailButtons(string imagePath)
        {
            Uri uri = new Uri(imagePath);
            ScanImage.Source = new BitmapImage(uri);

            Mail_AOK_Button.Visibility = Visibility.Visible;
            Mail_DKV_Button.Visibility = Visibility.Visible;
        }

        private void ShowMailForms()
        {
            MailSendenButton.IsEnabled = true;
            ReceiverLabel.Visibility = Visibility.Visible;
            ReceiverTextBox.Visibility = Visibility.Visible;
            SubjectLabel.Visibility = Visibility.Visible;
            SubjectTextBox.Visibility = Visibility.Visible;
            StandardText.Visibility = Visibility.Visible;
        }

        private void CreateImageAndShow()
        {
            throw new NotImplementedException();
        }

        private void MailSendenButton_Click(object sender, RoutedEventArgs e)
        {            
            Mail.Send(ReceiverTextBox.Text, SubjectTextBox.Text, StandardText.Text);
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
            this.IsEnabled = true;
            this.scannerNumber = ConfManager.GetScannerNumber();
            this.Directory = ConfManager.GetDirectory();
            this.FileName = ConfManager.GetFileName();

            //Console.WriteLine("ScannerNr: " + scannerNumber);
            // Wenn Scanner nicht erkannt wurde Scan Button deaktivieren

            if (scannerNumber == -1  && ConfManager.GetScanApiType() == ConfManager.ScanApiType.wia)
            {
                scanButton.IsEnabled = false;
            }
            else
            {
                scanButton.IsEnabled = true;                
            }
        }

        private void Mail_AOK_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mail_DKV_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
