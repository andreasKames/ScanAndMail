using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanAndMail
{
    internal class Configuaratiion
    {
        private int scannerNumber { get; set; }
        private String path { get; set; }
        private String fileName  { get; set; }


        public void setConf()
        {
            Console.WriteLine(string.Format("'{0}' project is created in '{1}' language ", 1, 2));
        }

        public void ReadConfFromFile()
        {
            scannerNumber = Convert.ToInt32( ConfigurationManager.AppSettings["ScannerNumber"]);
            path = ConfigurationManager.AppSettings["Path"];
            fileName = ConfigurationManager.AppSettings["FileName"];
        }

        public  void SaveConfToFile()
        {

        }
        

    }
}
