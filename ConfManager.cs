using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security;

namespace ScanAndMail
{
    internal class ConfManager
    {
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
        private const String StandardTextConstant = "StandardText";
        
        

        // Setter Methods

        public static void SetDiretory(String directory)
        {
            ConfigurationManager.AppSettings.Set(directoryConstant, directory);
        }
        public static void SetFileName(String fileName)
        {
            ConfigurationManager.AppSettings.Set(fileNameConstant, fileName);            
        }
        public static void SetScannerNumber(int scannerNumber)
        {
            ConfigurationManager.AppSettings.Set(scannerNumberConstant, scannerNumber.ToString());
        }
        public static void SetDateAdded(Boolean isDateAdded)
        {   
            if (isDateAdded == true)
            {
                ConfigurationManager.AppSettings.Set(isDateAddedConstant, "true");
            }
            else
            {
                ConfigurationManager.AppSettings.Set(isDateAddedConstant, "false");
            }
            
        }

        public static void SetScannedFile(String str)
        {
            ConfigurationManager.AppSettings.Set(ScannedFileConstant, str);
        }
        
        
        public static void SetMyName(String str)        
        {
            ConfigurationManager.AppSettings.Set(MyNameConstant, str);
        }
        public static void SetMyMailAddress(String str)
        {
            ConfigurationManager.AppSettings.Set(MyMailAdressConstant, str);
        }
        public static void SetHashedPassword(SecureString secString)
        {
            String str = Crypthography.EncryptString(secString);
            ConfigurationManager.AppSettings.Set(HashedPasswordConstant, str);
        }

        public static void SetSMTP_Server(String str)
        {
            ConfigurationManager.AppSettings.Set(SMTP_ServerConstant, str);
        }
        public static void SetReceivery(String str)
        {
            ConfigurationManager.AppSettings.Set(ReceiverConstant, str);
        }
        public static void SetSubject(String str)
        {
            ConfigurationManager.AppSettings.Set(SubjectConstant, str);
        }
        public static void SetStandardText(String str)
        {
            ConfigurationManager.AppSettings.Set(StandardTextConstant, str);
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

        public static SecureString GetHashedPassword()
        {
            String str = ConfigurationManager.AppSettings.Get(HashedPasswordConstant);
            return Crypthography.DecryptString(str);
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
