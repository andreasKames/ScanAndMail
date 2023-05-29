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
        Brush BrushGrey = Brushes.DarkGray;

        public MainWindow()
        {
            InitializeComponent();
            /*
            // Only for Testing
            Uri uri = new Uri(@"E:\Dateien von Andreas\Scans\Testbetrieb.jpg");
            ScanImage.Source = new BitmapImage(uri);
            //ShowMailForms();
            ShowPictureAndMailButtons(@"E:\Dateien von Andreas\Scans\Testbetrieb.jpg");
            MailSendenButton.IsEnabled = true;
            /**/
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = Directory + FileName;
            Console.WriteLine(Directory);
            Console.WriteLine(FileName);
            Console.WriteLine(Directory);

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
                    ConfManager conf = new ConfManager();
                    conf.SetFileName(imagePath);
                    conf.SaveSettingsToDisk();
                    ShowPictureAndMailButtons(imagePath);
                }
            }
            else
            {
                Twain32 twain = new Twain32();
               // twain.CloseDataSource();
               // twain.CloseDSM();
                if (twain.SelectSource())
                {                   
                    twain.OpenDSM();
                    twain.OpenDataSource();
                    twain.Acquire();
                  //  twain.CloseDSM();
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

                      //  Console.WriteLine("\n\n"+imagePath+ "\n\n");
                        ImageClass.CompressImage(tempImage, imagePath, 50);
                        ConfManager conf = new ConfManager();
                        conf.SetScannedFile(imagePath);
                        conf.SaveSettingsToDisk();
                        
                        ShowPictureAndMailButtons(imagePath);      
                        
                    }
                }
            }
        }

        private void ShowPictureAndMailButtons(string imagePath)
        {
            Uri uri = new Uri(imagePath);
            ScanImage.Source = new BitmapImage(uri);
            ReceiverButtons.Visibility = Visibility.Visible;
            ScanButton.Background = BrushGrey;
        }

        private void ShowMailForms()
        {
            MailSendenButton.Visibility = Visibility.Visible;
            MailBlock.Visibility = Visibility.Visible;
            Mail_AOK_Button.Background = BrushGrey;
            Mail_DKV_Button.Background = BrushGrey;
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
                ScanButton.IsEnabled = false;
            }
            else
            {
                if(ScanButton.IsEnabled == false)
                {
                    ScanButton.IsEnabled = true;
                }
                    
            }
        }

        private void Mail_AOK_Button_Click(object sender, RoutedEventArgs e)
        {
            //Mail_AOK_Button.Background = () SystemColors.GradientActiveCaptionBrushKey;
            ReceiverTextBox.Text = ConfManager.Get_AOK_MailAdress();
            SubjectTextBox.Text = ConfManager.Get_AOK_Subject();
            StandardText.Text = ConfManager.Get_AOK_StandardText();
            ShowMailForms();
        }

        private void Mail_DKV_Button_Click(object sender, RoutedEventArgs e)
        {
            ReceiverTextBox.Text = ConfManager.Get_DKV_MailAdress();
            SubjectTextBox.Text = ConfManager.Get_DKV_Subject();
            StandardText.Text = ConfManager.Get_DKV_StandardText();
            ShowMailForms();
        }
    }
}
