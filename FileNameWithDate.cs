using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ScanAndMail
{
    internal class FileNameWithDate
    {
        static public String AddDate(String fileName)
        {
            DateTime date = DateTime.Today;
            String fileWithDate = Path.GetDirectoryName(fileName)+ @"\" 
                 + Path.GetFileNameWithoutExtension(fileName)
                 + date.Date.Year + "-" + date.Date.Month + "-" + date.Date.Day
                 + Path.GetExtension(fileName);
            return fileWithDate;
        }

        public static String FindFileName(String fileName)
        {
            int postfix = 1;
            String baseFileName = Path.GetFileNameWithoutExtension(fileName);
            String tempFileName =  fileName;
            while (File.Exists(tempFileName))
            {
                tempFileName = baseFileName + "." + Convert.ToString(postfix) +Path.GetExtension(fileName);                 
                postfix++;

            }
            tempFileName = Path.GetDirectoryName(fileName) + @"\" +  tempFileName;            
            return tempFileName;
        }
    }
}
