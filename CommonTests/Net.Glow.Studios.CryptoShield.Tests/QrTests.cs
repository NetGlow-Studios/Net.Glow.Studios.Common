using IronSoftware.Drawing;
using Ngs.Common.CryptoShield.QR;

namespace Net.Glow.Studios.CryptoShield.Tests;

public class QrTests
{
    [Fact]
    public void QrEncodeTest()
    {
        var qr = new QrEncoder("https://youtube.com");
        qr.Color = Color.Blue;
        
        File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "test.png"), qr.GenerateQr());
    }
}