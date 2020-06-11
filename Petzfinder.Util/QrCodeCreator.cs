using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Petzfinder.Util
{
    public class QrCodeCreator
    {
        public static MemoryStream CreateQr(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q); ;
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            var bitmap = qrCode.GetGraphic(2);
            return new MemoryStream(bitmap);
        }
    }
}
