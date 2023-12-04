using ImageMagick;

namespace Ngs.Common.File.Converters;

public static class ImageConverter
{
    private static byte[] Convert(Stream stream, MagickFormat imageOutFormat)
    {
        using var memStream = new MemoryStream();
        using var image = new MagickImage(stream);

        image.Format = imageOutFormat;
        image.Write(memStream);

        return memStream.ToArray();
    }

    private static byte[] Convert(byte[] bytes, MagickFormat imageOutFormat)
    {
        using var memStream = new MemoryStream();
        using var image = new MagickImage(bytes);

        image.Format = imageOutFormat;
        image.Write(memStream);

        return memStream.ToArray();
    }

    private static async Task<byte[]> ConvertAsync(Stream stream, MagickFormat imageOutFormat)
    {
        using var memStream = new MemoryStream();
        using var image = new MagickImage(stream);

        image.Format = imageOutFormat;
        await image.WriteAsync(memStream);

        return memStream.ToArray();
    }

    private static async Task<byte[]> ConvertAsync(byte[] bytes, MagickFormat imageOutFormat)
    {
        using var memStream = new MemoryStream();
        using var image = new MagickImage(bytes);

        image.Format = imageOutFormat;
        await image.WriteAsync(memStream);

        return memStream.ToArray();
    }

    public static class Png
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToSvg(byte[] bytes) => Convert(bytes, MagickFormat.Svg);
        public static byte[] ToSvg(Stream stream) => Convert(stream, MagickFormat.Svg);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);


        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToSvgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Svg);
        public static async Task<byte[]> ToSvgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Svg);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);
    }

    public static class Jpg
    {
        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);

        public static byte[] ToRaw(byte[] bytes) => Convert(bytes, MagickFormat.Raw);
        public static byte[] ToRaw(Stream stream) => Convert(stream, MagickFormat.Raw);

        public static byte[] ToPsd(byte[] bytes) => Convert(bytes, MagickFormat.Psd);
        public static byte[] ToPsd(Stream stream) => Convert(stream, MagickFormat.Psd);

        public static byte[] ToEps(byte[] bytes) => Convert(bytes, MagickFormat.Eps);
        public static byte[] ToEps(Stream stream) => Convert(stream, MagickFormat.Eps);

        public static byte[] ToExr(byte[] bytes) => Convert(bytes, MagickFormat.Exr);
        public static byte[] ToExr(Stream stream) => Convert(stream, MagickFormat.Exr);

        public static byte[] ToHdr(byte[] bytes) => Convert(bytes, MagickFormat.Hdr);
        public static byte[] ToHdr(Stream stream) => Convert(stream, MagickFormat.Hdr);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);

        public static async Task<byte[]> ToRawAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Raw);
        public static async Task<byte[]> ToRawAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Raw);

        public static async Task<byte[]> ToPsdAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Psd);
        public static async Task<byte[]> ToPsdAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Psd);

        public static async Task<byte[]> ToEpsAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Eps);
        public static async Task<byte[]> ToEpsAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Eps);

        public static async Task<byte[]> ToExrAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Exr);
        public static async Task<byte[]> ToExrAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Exr);

        public static async Task<byte[]> ToHdrAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Hdr);
        public static async Task<byte[]> ToHdrAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Hdr);
    }

    public static class Bmp
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToSvg(byte[] bytes) => Convert(bytes, MagickFormat.Svg);
        public static byte[] ToSvg(Stream stream) => Convert(stream, MagickFormat.Svg);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);


        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToSvgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Svg);
        public static async Task<byte[]> ToSvgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Svg);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);
    }

    public static class Gif
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToSvg(byte[] bytes) => Convert(bytes, MagickFormat.Svg);
        public static byte[] ToSvg(Stream stream) => Convert(stream, MagickFormat.Svg);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);


        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToSvgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Svg);
        public static async Task<byte[]> ToSvgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Svg);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);
    }

    public static class Tiff
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToSvg(byte[] bytes) => Convert(bytes, MagickFormat.Svg);
        public static byte[] ToSvg(Stream stream) => Convert(stream, MagickFormat.Svg);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);


        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToSvgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Svg);
        public static async Task<byte[]> ToSvgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Svg);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);
    }

    public static class Webp
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);


        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);
    }

    public static class Svg
    {
        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);
    }

    public static class Ico
    {
        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);
    }

    public static class Heif
    {
        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);
    }

    public static class Avif
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToRaw(byte[] bytes) => Convert(bytes, MagickFormat.Raw);
        public static byte[] ToRaw(Stream stream) => Convert(stream, MagickFormat.Raw);

        public static byte[] ToPsd(byte[] bytes) => Convert(bytes, MagickFormat.Psd);
        public static byte[] ToPsd(Stream stream) => Convert(stream, MagickFormat.Psd);

        public static byte[] ToEps(byte[] bytes) => Convert(bytes, MagickFormat.Eps);
        public static byte[] ToEps(Stream stream) => Convert(stream, MagickFormat.Eps);

        public static byte[] ToExr(byte[] bytes) => Convert(bytes, MagickFormat.Exr);
        public static byte[] ToExr(Stream stream) => Convert(stream, MagickFormat.Exr);

        public static byte[] ToHdr(byte[] bytes) => Convert(bytes, MagickFormat.Hdr);
        public static byte[] ToHdr(Stream stream) => Convert(stream, MagickFormat.Hdr);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToRawAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Raw);
        public static async Task<byte[]> ToRawAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Raw);

        public static async Task<byte[]> ToPsdAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Psd);
        public static async Task<byte[]> ToPsdAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Psd);

        public static async Task<byte[]> ToEpsAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Eps);
        public static async Task<byte[]> ToEpsAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Eps);

        public static async Task<byte[]> ToExrAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Exr);
        public static async Task<byte[]> ToExrAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Exr);

        public static async Task<byte[]> ToHdrAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Hdr);
        public static async Task<byte[]> ToHdrAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Hdr);
    }

    public static class Raw
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToPsd(byte[] bytes) => Convert(bytes, MagickFormat.Psd);
        public static byte[] ToPsd(Stream stream) => Convert(stream, MagickFormat.Psd);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);


        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToPsdAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Psd);
        public static async Task<byte[]> ToPsdAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Psd);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);
    }

    public static class Psd
    {
        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);
    }
    
    public static class Eps
    {
        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);
        
        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);
        
        public static byte[] ToPsd(byte[] bytes) => Convert(bytes, MagickFormat.Psd);
        public static byte[] ToPsd(Stream stream) => Convert(stream, MagickFormat.Psd);
        
        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);
        
        public static async Task<byte[]> ToPsdAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Psd);
        public static async Task<byte[]> ToPsdAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Psd);
    }

    public static class Exr
    {
        public static byte[] ToJpeg(byte[] bytes) => Convert(bytes, MagickFormat.Jpeg);
        public static byte[] ToJpeg(Stream stream) => Convert(stream, MagickFormat.Jpeg);

        public static byte[] ToJpg(byte[] bytes) => Convert(bytes, MagickFormat.Jpg);
        public static byte[] ToJpg(Stream stream) => Convert(stream, MagickFormat.Jpg);

        public static byte[] ToPng(byte[] bytes) => Convert(bytes, MagickFormat.Png);
        public static byte[] ToPng(Stream stream) => Convert(stream, MagickFormat.Png);

        public static byte[] ToBmp(byte[] bytes) => Convert(bytes, MagickFormat.Bmp);
        public static byte[] ToBmp(Stream stream) => Convert(stream, MagickFormat.Bmp);

        public static byte[] ToGif(byte[] bytes) => Convert(bytes, MagickFormat.Gif);
        public static byte[] ToGif(Stream stream) => Convert(stream, MagickFormat.Gif);

        public static byte[] ToTiff(byte[] bytes) => Convert(bytes, MagickFormat.Tiff);
        public static byte[] ToTiff(Stream stream) => Convert(stream, MagickFormat.Tiff);

        public static byte[] ToWebp(byte[] bytes) => Convert(bytes, MagickFormat.WebP);
        public static byte[] ToWebp(Stream stream) => Convert(stream, MagickFormat.WebP);

        public static byte[] ToIco(byte[] bytes) => Convert(bytes, MagickFormat.Ico);
        public static byte[] ToIco(Stream stream) => Convert(stream, MagickFormat.Ico);

        public static byte[] ToHeif(byte[] bytes) => Convert(bytes, MagickFormat.Heif);
        public static byte[] ToHeif(Stream stream) => Convert(stream, MagickFormat.Heif);

        public static byte[] ToRaw(byte[] bytes) => Convert(bytes, MagickFormat.Raw);
        public static byte[] ToRaw(Stream stream) => Convert(stream, MagickFormat.Raw);

        public static byte[] ToPsd(byte[] bytes) => Convert(bytes, MagickFormat.Psd);
        public static byte[] ToPsd(Stream stream) => Convert(stream, MagickFormat.Psd);

        public static byte[] ToEps(byte[] bytes) => Convert(bytes, MagickFormat.Eps);
        public static byte[] ToEps(Stream stream) => Convert(stream, MagickFormat.Eps);

        public static byte[] ToAvif(byte[] bytes) => Convert(bytes, MagickFormat.Avif);
        public static byte[] ToAvif(Stream stream) => Convert(stream, MagickFormat.Avif);

        public static byte[] ToHdr(byte[] bytes) => Convert(bytes, MagickFormat.Hdr);
        public static byte[] ToHdr(Stream stream) => Convert(stream, MagickFormat.Hdr);

        public static async Task<byte[]> ToJpegAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpeg);
        public static async Task<byte[]> ToJpegAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpeg);

        public static async Task<byte[]> ToJpgAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Jpg);
        public static async Task<byte[]> ToJpgAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Jpg);

        public static async Task<byte[]> ToPngAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Png);
        public static async Task<byte[]> ToPngAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Png);

        public static async Task<byte[]> ToBmpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Bmp);
        public static async Task<byte[]> ToBmpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Bmp);

        public static async Task<byte[]> ToGifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Gif);
        public static async Task<byte[]> ToGifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Gif);

        public static async Task<byte[]> ToTiffAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Tiff);
        public static async Task<byte[]> ToTiffAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Tiff);

        public static async Task<byte[]> ToWebpAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.WebP);
        public static async Task<byte[]> ToWebpAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.WebP);

        public static async Task<byte[]> ToIcoAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Ico);
        public static async Task<byte[]> ToIcoAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Ico);

        public static async Task<byte[]> ToHeifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Heif);
        public static async Task<byte[]> ToHeifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Heif);

        public static async Task<byte[]> ToRawAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Raw);
        public static async Task<byte[]> ToRawAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Raw);

        public static async Task<byte[]> ToPsdAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Psd);
        public static async Task<byte[]> ToPsdAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Psd);

        public static async Task<byte[]> ToEpsAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Eps);
        public static async Task<byte[]> ToEpsAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Eps);

        public static async Task<byte[]> ToAvifAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Avif);
        public static async Task<byte[]> ToAvifAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Avif);

        public static async Task<byte[]> ToHdrAsync(byte[] bytes) => await ConvertAsync(bytes, MagickFormat.Hdr);
        public static async Task<byte[]> ToHdrAsync(Stream stream) => await ConvertAsync(stream, MagickFormat.Hdr);
    }
}