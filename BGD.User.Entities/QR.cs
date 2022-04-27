using System;
using System.Drawing;
using System.IO;
using QRCoder;

namespace BGD.User.Entities
{
    public class QR
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Tenant { get; set; }
        
        public int Table { get; set; }

        public QR()
        {
            Id = Nanoid.Nanoid.Generate( "1234567890abcdefghijklmno-", 40);
            Url = "localhost:5002";
        }

        public Bitmap QRGenerator()
        {
            string payLoad = new PayloadGenerator.Url($"https://{Url}/redirect/{Tenant}/{Id}").ToString();
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(payLoad, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }
    }
}