using System.Configuration;
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
        private ScanningWIA scanningWIA;
        

        public ConfWindow()
        {
            InitializeComponent();
        }

        private void ConfWindow_Loaded(object sender, RoutedEventArgs e)
        {   
            // Scannerliste initialisieren
            scanningWIA = new ScanningWIA();
            List<string> listScanner = scanningWIA.ListScanner();
            foreach (string s in listScanner)
            {
                ListBoxScanner.Items.Add(s);
            }

            // Einlesen der Config Werte
            int scannerNumber = Convert.ToInt32(ConfigurationManager.AppSettings.Get("scannerNumger"));
            if (scannerNumber >= 0)
            {
                ListBoxScanner.SelectedIndex = scannerNumber;
            }
            else
            {
                ListBoxScanner.SelectedIndex = 0;
            }

            ListBoxScanner.SelectedIndex = scannerNumber;
            PathTextBox.Text = ConfigurationManager.AppSettings.Get("PDF_Dir");
            FileNameTextBox.Text = ConfigurationManager.AppSettings.Get("PDF_FileName");

        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationManager.AppSettings.Set("ScannerNumber", ListBoxScanner.SelectedIndex.ToString());
            ConfigurationManager.AppSettings.Set("path", PathTextBox.Text);
            ConfigurationManager.AppSettings.Set("fileName", FileNameTextBox.Text);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
