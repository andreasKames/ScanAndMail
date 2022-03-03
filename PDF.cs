using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using System.IO;

namespace ScanAndMail
{
    internal class PDF
    {
        static public void StorePDF(String ImageDir, String ImageFile, String PDF_Dir, String PDF_FileName)
        {
            // Load input JPG file
            String ImagePath = ImageDir + ImageFile;

            // Initialize new PDF document
            Document doc = new Document();

            // Add empty page in empty document
            Page page = doc.Pages.Add();
            Aspose.Pdf.Image image = new Aspose.Pdf.Image();
            image.File = (ImagePath);
            if (File.Exists(ImagePath))
            {
                File.Delete(ImagePath);
            }

            // Add image on a page
            page.Paragraphs.Add(image);

            // Save output PDF file
            doc.Save(PDF_Dir + PDF_FileName);
            File.Delete(ImagePath);
        }
    }
}
