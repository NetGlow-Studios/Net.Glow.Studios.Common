using IronBarCode;
using Color = IronSoftware.Drawing.Color;

namespace Ngs.Common.CryptoShield.QR;

public class QrEncoder(string data)
{
    public string Data { get; private set; } = data;
    public Color Color { get; set; } = Color.Black;
    public (int, int) Dimensions { get; set; } = new(500, 500);
    public QRCodeLogo Logo { get; private set; } = new();

    public void AddLogo(string filePath, int size = 0, float roundedCornerRadius = 0.0F)
    {
        Logo = new QRCodeLogo(filePath, size, roundedCornerRadius);
    }

    public byte[] GenerateQr()
    {
        var qr = QRCodeWriter.CreateQrCodeWithLogo(Data, Logo);
        qr.ResizeTo(Dimensions.Item1, Dimensions.Item2).SetMargins(10).ChangeBarCodeColor(Color);
        return qr.ToPngBinaryData();
    }
    
    public static byte[] GenerateQr(string data, Color color, QRCodeLogo logo)
    {
        var qr = QRCodeWriter.CreateQrCodeWithLogo(data, logo);
        qr.ResizeTo(500, 500).SetMargins(10).ChangeBarCodeColor(color);
        return qr.ToPngBinaryData();
    }

    public static byte[] GenerateQr(string data) 
        => GenerateQr(data, IronSoftware.Drawing.Color.Black, new QRCodeLogo());
    
    public static byte[] GenerateQr(string data, IronSoftware.Drawing.Color color) 
        => GenerateQr(data, color, new QRCodeLogo());

    public static byte[] GenerateQr(string data, QRCodeLogo logo) =>
        GenerateQr(data, IronSoftware.Drawing.Color.Black, logo);

    public override string ToString()
    {
        return Data;
    }
}