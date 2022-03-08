using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ScanAndMail
{
    internal class ImageClass
    {
        // Klasse ist auschliesslich dazu da, um Bilder (JPEGs) zu komprimieren
        public static void CompressImage(string inputFile, string outputFile, int quality)
        {
            using (Bitmap bmp1 = new Bitmap(inputFile))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);

                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(outputFile, jpgEncoder, myEncoderParameters);
            }
        }


        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }

}
