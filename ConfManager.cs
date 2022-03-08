using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security;
using System.Windows;

namespace ScanAndMail
{
    internal class ConfManager
    {
        private Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private const String scannerNumberConstant = "scannerNumber";
        private const String directoryConstant = "Directory";
        private const String fileNameConstant = "BaseFileName";
        private const String isDateAddedConstant = "isDateAdded";
        private const String ScannedFileConstant = "ScannedFile";

        private const String MyNameConstant = "MyName";
        private const String MyMailAdressConstant = "MyMailAddress";
        private const String HashedPasswordConstant = "HashedPassword";
        private const String SMTP_ServerConstant = "SMTP_Server";

        private const String ReceiverConstant = "Receiver";
        private const String SubjectConstant = "Subject";
        private const String StandardTextConstant = "StandardText" ;
        private const String appSettingstConstant = "appSettings";
        

        // Setter Methods

        public void SetDiretory(String directory)
        {
            conf.AppSettings.Settings.Remove(directoryConstant);
            conf.AppSettings.Settings.Add(directoryConstant, directory);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetFileName(String fileName)
        {
            conf.AppSettings.Settings.Remove(fileNameConstant);
            conf.AppSettings.Settings.Add(fileNameConstant, fileName);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetScannerNumber(int scannerNumber)
        {
            conf.AppSettings.Settings.Remove(scannerNumberConstant);
            conf.AppSettings.Settings.Add(scannerNumberConstant, scannerNumber.ToString());
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
            //conf.Save();
        }
        public void SetDateAdded(Boolean isDateAdded)
        {
            conf.AppSettings.Settings.Remove(isDateAddedConstant);
            if (isDateAdded == true)
            {
                conf.AppSettings.Settings.Add(isDateAddedConstant, "true");
            }
            else
            {
                conf.AppSettings.Settings.Add(isDateAddedConstant, "false");
            }
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }

        public void SetScannedFile(String str)
        {
            conf.AppSettings.Settings.Remove(ScannedFileConstant);
            conf.AppSettings.Settings.Add(ScannedFileConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        
        
        public void SetMyName(String str)        
        {
            conf.AppSettings.Settings.Remove(MyNameConstant);
            conf.AppSettings.Settings.Add(MyNameConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetMyMailAddress(String str)
        {
            conf.AppSettings.Settings.Remove(MyMailAdressConstant);
            conf.AppSettings.Settings.Add(MyMailAdressConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetHashedPassword(SecureString secString)
        {
            conf.AppSettings.Settings.Remove(HashedPasswordConstant);
            String str = Crypthography.EncryptString(secString);
            conf.AppSettings.Settings.Add(HashedPasswordConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }

        public void SetSMTP_Server(String str)
        {
            conf.AppSettings.Settings.Remove(SMTP_ServerConstant);
            conf.AppSettings.Settings.Add(SMTP_ServerConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetReceivery(String str)
        {
            conf.AppSettings.Settings.Remove(ReceiverConstant);
            conf.AppSettings.Settings.Add(ReceiverConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetSubject(String str)
        {
            conf.AppSettings.Settings.Remove(SubjectConstant);
            conf.AppSettings.Settings.Add(SubjectConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }
        public void SetStandardText(String str)
        {
            conf.AppSettings.Settings.Remove(StandardTextConstant);
            conf.AppSettings.Settings.Add(StandardTextConstant, str);
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }


        //Getter Methods


        public static String GetDirectory()
        {
            return ConfigurationManager.AppSettings.Get(directoryConstant);
        }
        public static String GetFileName()
        {
            return ConfigurationManager.AppSettings.Get(fileNameConstant);
        }

        public static int GetScannerNumber()
        {
            
            //return Convert.ToInt32(conf.AppSettings.);
            return Convert.ToInt32(ConfigurationManager.AppSettings.Get(scannerNumberConstant));
        }

        public static Boolean IsDateAdded()
        {
            if (ConfigurationManager.AppSettings.Get(isDateAddedConstant) == "true")
            {
                return true;
            }
            return false;
        }

        public static String GetScannedFile()
        {
            return ConfigurationManager.AppSettings.Get(ScannedFileConstant);
        }

        public static String GetMyName()
        {
            return ConfigurationManager.AppSettings.Get(MyNameConstant);
        }

        public static String GetMyMailAddress ()
        {
            return ConfigurationManager.AppSettings.Get(MyMailAdressConstant);
        }

        public static String GetHashedPassword()
        {
            String str = ConfigurationManager.AppSettings.Get(HashedPasswordConstant);
            return Crypthography.ToInsecureString( Crypthography.DecryptString(str));
        }

        public static String GetSMTP_Server()
        {
            return ConfigurationManager.AppSettings.Get(SMTP_ServerConstant);
        }

        public static String GetReceiver()
        {
            return ConfigurationManager.AppSettings.Get(ReceiverConstant);
        }

        public static String GetSubject()
        {
            return ConfigurationManager.AppSettings.Get(SubjectConstant);
        }

        public static String GetStandardText()
        {
            return ConfigurationManager.AppSettings.Get(StandardTextConstant);
        }

        
    }
}
