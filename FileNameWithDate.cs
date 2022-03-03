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
            String fileWithDate = Path.GetFileNameWithoutExtension(fileName)
                 + date.Date.Year + "-" + date.Date.Month + "-" + date.Date.Day
                 + Path.GetExtension(fileName);
            return fileWithDate;
        }
    }
}
