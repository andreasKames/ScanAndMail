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
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            String fileName = FileNameTextBox.Text;
            //String scannerNumber = ListBoxScanner.SelectedIndex.ToString();
            
            if (DateCheckBox.IsChecked == true)
            {
                ConfManager.SetDateAdded(true);
            }
            else
            {
                ConfManager.SetDateAdded(false);
            }
            
            ConfManager.SetScannerNumber(ListBoxScanner.SelectedIndex);            
            ConfManager.SetDiretory(PathTextBox.Text);           
            ConfManager.SetFileName(fileName);           
                        
            if (Directory.Exists( ConfManager.GetDirectory() ) )
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Dateipfad nicht gefunden!");
                PathTextBox.Focus();
            }         
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
