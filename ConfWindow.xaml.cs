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
            //Scannerliste initialisieren
            scanningWIA = new ScanningWIA();
            List<string> listScanner = scanningWIA.ListScanner();
            foreach (string s in listScanner)
            {
                ListBoxScanner.Items.Add(s);
            }

            if (listScanner.Count == 0 )
            {
                ListBoxScanner.SelectedIndex = 0;
            }


            //

        }

        private void ConfWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConfigurationManager.AppSettings.Set("ScannerNumber", ListBoxScanner.SelectedIndex.ToString());
            ConfigurationManager.AppSettings.Set("path", PathTextBox.Text);
            ConfigurationManager.AppSettings.Set("fileName", FileNameTextBox.Text);
        }
    }
}
