using System.Reflection;
using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Converters;

public class ImageConverter
{
    public StorageImageFile Image { get; set; }

    public ImageConverter(StorageImageFile image)
    {
        Image = image;
    }
    
    public StorageImageFile ToJpeg() => Convert("jpeg");
    public StorageImageFile ToJpg() => Convert("jpg");
    public StorageImageFile ToPng() => Convert("png");
    public StorageImageFile ToBmp() => Convert("bmp");
    public StorageImageFile ToGif() => Convert("gif");
    public StorageImageFile ToTiff() => Convert("tiff");
    public StorageImageFile ToSvg() => Convert("svg");
    public StorageImageFile ToWebp() => Convert("webp");
    public StorageImageFile ToIco() => Convert("ico");
    public StorageImageFile ToPsd() => Convert("psd");
    
    
    private StorageImageFile Convert(string to)
    {
        var from = Image.Extension.Replace(".", "").ToLower();
        var bytes = Image.ConvertToBytes();
        
        var converterType = typeof(Ngs.Common.Tools.Image.ImageConverter).GetNestedTypes()
            .FirstOrDefault(x => x.Name.Equals(from, StringComparison.CurrentCultureIgnoreCase));

        if (converterType is null) throw new Exception($"No converter found. Unsupported format '{from}'.");
        
        var method = converterType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.Name.Equals("To" + to, StringComparison.CurrentCultureIgnoreCase));

        if (method is null) throw new ArgumentException($"Unable to convert '{from}' to '{to}'.");
        
        Image.UpdateContent((byte[])method.Invoke(null, [bytes])!);
        Image.ChangeExtension(to);
        
        return Image;
    }
    
    private async Task<StorageImageFile> ConvertAsync(string to)
    {
        var from = Image.Extension.Replace(".", "").ToLower();
        var bytes = await Image.ConvertToBytesAsync();
        
        var converterType = typeof(Ngs.Common.Tools.Image.ImageConverter).GetNestedTypes()
            .FirstOrDefault(x => x.Name.Equals(from, StringComparison.CurrentCultureIgnoreCase));

        if (converterType is null) throw new ArgumentException($"No converter found for '{from}'.");
        
        var method = converterType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.Name.Equals("To" + to + "Async", StringComparison.CurrentCultureIgnoreCase));

        if (method is null) throw new ArgumentException($"No conversion method found for '{to}'.");
        
        Image.UpdateContent((byte[])method.Invoke(null, [bytes])!);
        Image.ChangeExtension(to);
        
        return Image;
    }
}