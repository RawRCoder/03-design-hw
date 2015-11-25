using System.Drawing;
using System.Drawing.Imaging;

namespace DesignHw.Adapters
{
    public class ImageFileRenderTarget : ImageRenderTarget
    {
        public ImageFormat Format { get; }
        public string FileName { get; }
        public ImageFileRenderTarget(string fileName, Image image, ImageFormat format = null) : base(image)
        {
            FileName = fileName;
            Format = format ?? ImageFormat.Png;
        }

        public ImageFileRenderTarget(string fileName, ushort width, ushort height, PixelFormat pformat = PixelFormat.Format32bppArgb, ImageFormat iformat = null) 
            : base(width, height, pformat)
        {
            FileName = fileName;
            Format = iformat;
        }

        public override void Close(Graphics g)
        {
            Image.Save(FileName, Format);
        }
    }
}