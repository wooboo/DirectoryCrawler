using ImageMagick;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq;

namespace FileSorter.Controllers
{
    public class ThumbnailGenerator
    {
        public void Resize(IFileInfo fileInfo, int? width, int? height, Stream stream)
        {
            using (MagickImageCollection images = new MagickImageCollection())
            {
                MagickReadSettings settings = new MagickReadSettings();
                settings.FrameIndex = 0; // First page
                settings.FrameCount = 1; // Number of pages
                using (var readStream = fileInfo.CreateReadStream())
                {
                    images.Read(readStream, settings);
                }
                MagickGeometry size = new MagickGeometry(width ?? 0, height ?? 0);
                var image = images.First();
                image.Resize(size);
                image.Write(stream, MagickFormat.Jpeg);
            }
        }
    }

}
