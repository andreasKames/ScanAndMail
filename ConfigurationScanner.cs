using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAndMail
{
    internal class ConfigurationScanner : ConfigurationElement
    {
        [ConfigurationProperty("ScannerNumber", DefaultValue = 1, IsRequired = true)]
        public int ScannerNumber
        {
            get
            {
                return (int)this["ScannerNumber"];
            }
            set
            {
                value = (int)this["ScannerNumber"];
            }
        }

        [ConfigurationProperty("Path", DefaultValue = @"C:\Eigeene Dateien\Dokumente\", IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)this["Path"];
            }
            set
            {
                value = (string)this["Path"];
            }
        }

        [ConfigurationProperty("FileName", DefaultValue = "Scan", IsRequired = true)]
        public string FileName
        {
            get
            {
                return (string)this["FileName"];
            }
            set
            {
                value = (string)this["FileName"];
            }
        }
    }
}