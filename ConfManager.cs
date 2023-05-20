using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security;
using System.Windows;


namespace   ScanAndMail
{
    internal class ConfManager
    {
        private Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private const String scanApiTypeConstant = "scanApiType";
        private const String scannerNumberConstant = "scannerNumber";
        private const String directoryConstant = "Directory";
        private const String fileNameConstant = "BaseFileName";
        private const String isDateAddedConstant = "isDateAdded";
        private const String ScannedFileConstant = "ScannedFile";

        private const String MyNameConstant = "MyName";
        private const String MyMailAdressConstant = "MyMailAddress";
        private const String HashedPasswordConstant = "HashedPassword";
        private const String SMTP_ServerConstant = "SMTP_Server";

        private const String AOK_MailAdress_Constant = "AOK_MailAdress";
        private const String AOK_SubjectConstant = "AOK_Subject";
        private const String AOK_TextConstant = "AOK_Text";

        private const String DKV_MailAdress_Constant = "DKV_MailAdress";
        private const String DKV_SubjectConstant = "DKV_Subject";
        private const String DKV_TextConstant = "DKV_Text";

        private const String appSettingstConstant = "appSettings";
        public enum ScanApiType { wia, twain };
        

        // Setter Methods
        public void SaveSettingsToDisk()
        {
            conf.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection(appSettingstConstant);
        }

        public void SetScanApiType(ScanApiType scanApiType)
        {
            String apiTypeString = "twain";
            conf.AppSettings.Settings.Remove(scanApiTypeConstant);
            if (scanApiType == ScanApiType.wia)
            {
                Console.WriteLine("wia");
                apiTypeString = "wia";
            }
            conf.AppSettings.Settings.Add(scanApiTypeConstant, apiTypeString);
        }

        public void SetDiretory(String directory)
        {
            conf.AppSettings.Settings.Remove(directoryConstant);
            conf.AppSettings.Settings.Add(directoryConstant, directory);        
        }
        public void SetFileName(String fileName)
        {
            conf.AppSettings.Settings.Remove(fileNameConstant);
            conf.AppSettings.Settings.Add(fileNameConstant, fileName);
        }
        public void SetScannerNumber(int scannerNumber)
        {
            conf.AppSettings.Settings.Remove(scannerNumberConstant);
            conf.AppSettings.Settings.Add(scannerNumberConstant, scannerNumber.ToString());
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
        }

        public void SetScannedFile(String str)
        {
            conf.AppSettings.Settings.Remove(ScannedFileConstant);
            conf.AppSettings.Settings.Add(ScannedFileConstant, str);
        }
        
        
        public void SetMyName(String str)        
        {
            conf.AppSettings.Settings.Remove(MyNameConstant);
            conf.AppSettings.Settings.Add(MyNameConstant, str);
        }

        public void SetMyMailAddress(String str)
        {
            conf.AppSettings.Settings.Remove(MyMailAdressConstant);
            conf.AppSettings.Settings.Add(MyMailAdressConstant, str);
        }

        public void SetHashedPassword(SecureString secString)
        {
            conf.AppSettings.Settings.Remove(HashedPasswordConstant);
            String str = Crypthography.EncryptString(secString);
            conf.AppSettings.Settings.Add(HashedPasswordConstant, str);
        }

        public void SetSMTP_Server(String str)
        {
            conf.AppSettings.Settings.Remove(SMTP_ServerConstant);
            conf.AppSettings.Settings.Add(SMTP_ServerConstant, str);
        }

        //Setter AOK
        public void Set_AOK_MailAdress(String str)
        {
            conf.AppSettings.Settings.Remove(AOK_MailAdress_Constant);
            conf.AppSettings.Settings.Add(AOK_MailAdress_Constant, str);
        }
        public void Set_AOK_Subject(String str)
        {
            conf.AppSettings.Settings.Remove(AOK_SubjectConstant);
            conf.AppSettings.Settings.Add(AOK_SubjectConstant, str);
        }
        public void Set_AOK_StandardText(String str)
        {
            conf.AppSettings.Settings.Remove(AOK_TextConstant);
            conf.AppSettings.Settings.Add(AOK_TextConstant, str);
        }

        public void Set_DKV_MailAdress(String str)
        {
            conf.AppSettings.Settings.Remove(DKV_MailAdress_Constant);
            conf.AppSettings.Settings.Add(DKV_MailAdress_Constant, str);
        }
        public void Set_DKV_Subject(String str)
        {
            conf.AppSettings.Settings.Remove(DKV_SubjectConstant);
            conf.AppSettings.Settings.Add(DKV_SubjectConstant, str);
        }
        public void Set_DKV_StandardText(String str)
        {
            conf.AppSettings.Settings.Remove(DKV_TextConstant);
            conf.AppSettings.Settings.Add(DKV_TextConstant, str);
        }
        //Getter Methods

        public static ScanApiType GetScanApiType()
        {
            string apiType = ConfigurationManager.AppSettings.Get(scanApiTypeConstant);
            Console.WriteLine(apiType);
            if (apiType.Equals("wia"))
                return ScanApiType.wia;
            return ScanApiType.twain;
        }
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


        // Getter AOK
        public static String Get_AOK_MailAdress()
        {
            return ConfigurationManager.AppSettings.Get(AOK_MailAdress_Constant);
        }

        public static String Get_AOK_Subject()
        {
            return ConfigurationManager.AppSettings.Get(AOK_SubjectConstant);
        }

        public static String Get_AOK_StandardText()
        {
            return ConfigurationManager.AppSettings.Get(AOK_TextConstant);
        }


        public static String Get_DKV_MailAdress()
        {
            return ConfigurationManager.AppSettings.Get(DKV_MailAdress_Constant);
        }

        public static String Get_DKV_Subject()
        {
            return ConfigurationManager.AppSettings.Get(DKV_SubjectConstant);
        }

        public static String Get_DKV_StandardText()
        {
            return ConfigurationManager.AppSettings.Get(DKV_TextConstant);
        }
    }
}
