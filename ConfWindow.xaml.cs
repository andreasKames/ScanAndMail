﻿using System;
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
using Saraff.Twain;

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
            if ((bool)TwainIsChosen.IsChecked)
            {
                ListScannerLabel.Visibility = Visibility.Hidden;
                ListBoxScanner.Visibility   = Visibility.Hidden;
            }
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

            AOK_MailTextBox.Text = ConfManager.Get_AOK_MailAdress();
            AOK_SubjectTextBox.Text = ConfManager.Get_AOK_Subject();
            AOK_StandardText.Text = ConfManager.Get_AOK_StandardText();

            DKV_MailTextBox.Text = ConfManager.Get_DKV_MailAdress();
            DKV_SubjectTextBox.Text = ConfManager.Get_DKV_Subject();
            DKV_StandardText.Text = ConfManager.Get_DKV_StandardText();
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

           
            if (TwainIsChosen.IsChecked == true)
            {
                conf.SetScanApiType(ConfManager.ScanApiType.twain);
            }
            else {
                conf.SetScanApiType(ConfManager.ScanApiType.wia);
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
            conf.SetSMTP_Server(SMTP_ServerTextBox.Text);

            conf.Set_AOK_MailAdress(AOK_MailTextBox.Text);
            conf.Set_AOK_Subject(AOK_SubjectTextBox.Text);
            conf.Set_AOK_StandardText(AOK_StandardText.Text);

            conf.Set_DKV_MailAdress(DKV_MailTextBox.Text);
            conf.Set_DKV_Subject(DKV_SubjectTextBox.Text);
            conf.Set_DKV_StandardText(DKV_StandardText.Text);
            
            conf.SaveSettingsToDisk();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TwainIsChosen_Checked(object sender, RoutedEventArgs e)
        {
            if (ListScannerLabel != null) // Falls ConfWindow noch nicht geladen war, ansonten gibt es eine NullPointerException
            {
                ListScannerLabel.Visibility = Visibility.Hidden;
                ListBoxScanner.Visibility = Visibility.Hidden;
            }
            
        }

        private void WIAIsChosen_Checked(object sender, RoutedEventArgs e)
        {
            if (ListScannerLabel != null)
            {
                ListScannerLabel.Visibility = Visibility.Visible;
                ListBoxScanner.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_Changed(object sender, RoutedEventArgs e)
        {
            ConfManager conf = new ConfManager();
            conf.SetHashedPassword(PasswordBox.SecurePassword);
            conf.SaveSettingsToDisk();
        }
    }
}
