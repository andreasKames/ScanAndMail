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

namespace ScanAndMail
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int scannerNumber;
        private string path;
        private string fileName;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            
            


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
            this.scannerNumber = Convert.ToInt32( ConfigurationManager.AppSettings.Get("scannerNumger"));
            this.path = ConfigurationManager.AppSettings.Get("path");
            this.fileName = ConfigurationManager.AppSettings.Get("fileName");

        }
    }
}
