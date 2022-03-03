using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAndMail
{
    internal class ConfManager
    {
        private const String scannerNumberKey  = "scannerNumber";
        private const String directoryKey = "Directory";
        private const String fileNameKey = "FileName";
        private const String isDateAddedKey = "isDateAdded";

        //Getter Methods

        public static String GetDirectory()
        {
            return ConfigurationManager.AppSettings.Get(directoryKey);
        }

        public static String GetFileName()
        {
            return ConfigurationManager.AppSettings.Get(fileNameKey);
        }
        
        public static int GetScannerNumber()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings.Get(scannerNumberKey));
        }

        public static Boolean IsDateAdded()
        {
            if (ConfigurationManager.AppSettings.Get(isDateAddedKey) == "true")
            { 
                return true; 
            }
            return false;
        }
        // Setter Methods

        public static void SetDiretory(String directory)
        {
            ConfigurationManager.AppSettings.Set(directoryKey, directory);
        }

        public static void SetFileName(String fileName)
        {
            ConfigurationManager.AppSettings.Set(fileNameKey, fileName);            
        }

        public static void SetScannerNumber(int scannerNumber)
        {
            ConfigurationManager.AppSettings.Set(scannerNumberKey, scannerNumber.ToString());
        }

        public static void SetDateAdded(Boolean isDateAdded)
        {   
            if (isDateAdded == true)
            {
                ConfigurationManager.AppSettings.Set(isDateAddedKey, "true");
            }
            else
            {
                ConfigurationManager.AppSettings.Set(isDateAddedKey, "false");
            }
            
        }
    }
}
