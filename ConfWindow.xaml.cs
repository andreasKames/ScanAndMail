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
using WIA;
using System.IO;

namespace ScanAndMail
{
    /// <summary>
    /// Interaktionslogik für ConfWindow.xaml
    /// </summary>
    public partial class ConfWindow : Window
    {
        private ScanningWIA scanningWIA;
        

        public ConfWindow()
        {
            InitializeComponent();
        }

        private void ConfWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //ConfManager conf = new ConfManager();
            // Scannerliste initialisieren
            scanningWIA = new ScanningWIA();
            List<string> listScanner = scanningWIA.ListScanner();
            foreach (string s in listScanner)
            {
                ListBoxScanner.Items.Add(s);
            }

            // Einlesen der Config Werte
            int scannerNumber = ConfManager.GetScannerNumber();
            if (scannerNumber >= 0)
            {
                ListBoxScanner.SelectedIndex = scannerNumber;
            }
            else
            {
                ListBoxScanner.SelectedIndex = 0;
            }

            ListBoxScanner.SelectedIndex = scannerNumber;
            PathTextBox.Text = ConfManager.GetDirectory();
            FileNameTextBox.Text = ConfManager.GetFileName();

            // Mail Teil

            NameTextBox.Text = ConfManager.GetMyName();
            E_MailTextBox.Text = ConfManager.GetMyMailAddress();
            SMTP_ServerTextBox.Text = ConfManager.GetSMTP_Server();
            ReceiverTextBox.Text = ConfManager.GetReceiver();
            SubjectTextBox.Text = ConfManager.GetSubject(); 
            StandardText.Text = ConfManager.GetStandardText();

        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {            
            //String scannerNumber = ListBoxScanner.SelectedIndex.ToString();
            ConfManager conf = new ConfManager();
            if (DateCheckBox.IsChecked == true)
            {
                conf.SetDateAdded(true);
            }
            else
            {
                conf.SetDateAdded(false);
            }
            
            conf.SetScannerNumber(ListBoxScanner.SelectedIndex);            
            conf.SetDiretory(PathTextBox.Text);           
            conf.SetFileName(FileNameTextBox.Text);           
                        
            if (Directory.Exists( ConfManager.GetDirectory() ) )
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Dateipfad nicht gefunden!");
                PathTextBox.Focus();
            }
            
            conf.SetMyName(NameTextBox.Text);
            conf.SetMyMailAddress(E_MailTextBox.Text);
            conf.SetHashedPassword(PasswordBox.SecurePassword);
            conf.SetSMTP_Server(SMTP_ServerTextBox.Text);
            conf.SetReceivery(ReceiverTextBox.Text); 
            conf.SetSubject(SubjectTextBox.Text);
            conf.SetStandardText(StandardText.Text);
            conf.SaveSettingsToDisk();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
