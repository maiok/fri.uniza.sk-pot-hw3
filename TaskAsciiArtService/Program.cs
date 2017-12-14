using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TaskAsciiArtService.ServiceReference1;

namespace TaskAsciiArtService
{
    class Program
    {
        private static void Main(string[] args)
        {
            String basePath = "D:\\mySHAREPOINT\\OneDrive - University of Zilina\\uniza\\ING-2-Z\\POT\\DomaceUlohy\\InstantMessaging\\data\\";
            Image newImage = Image.FromFile(basePath + "fri-logo.gif");

            byte[] newImageByteArray = ImageToByteArray(newImage);

            AsciiArtServiceClient aasc = new AsciiArtServiceClient();

            File.WriteAllText(basePath + "fri-logo-ascii.txt", aasc.ImageToAscii(newImageByteArray));
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
