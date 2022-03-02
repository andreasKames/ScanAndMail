using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;

namespace ScanAndMail
{
    internal class PDF
    {
        static public void storePDF(String path, String fileName)
        {
            // Initialize new PDF document
            Document doc = new Document();

            // Add empty page in empty document
            Page page = doc.Pages.Add();
            Aspose.Pdf.Image image = new Aspose.Pdf.Image();
            image.File = (path);

            // Add image on a page
            page.Paragraphs.Add(image);

            // Save output PDF file
            doc.Save(path + fileName);
        }

    }
}
