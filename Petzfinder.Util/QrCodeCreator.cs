using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Petzfinder.Util
{
    public class QrCodeCreator
    {
        public static Byte[] CreateQrArray(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q); ;
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            var bitmap = qrCode.GetGraphic(2);
            //var stream = new MemoryStream(bitmap);
            return bitmap;
        }
    }
}
