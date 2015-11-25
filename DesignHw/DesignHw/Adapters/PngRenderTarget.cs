using System.Drawing;
using System.Drawing.Imaging;

namespace DesignHw.Adapters
{
    public class PngRenderTarget : ImageFileRenderTarget
    {
        public PngRenderTarget(string fileName, Image image) 
            : base(fileName, image, ImageFormat.Png)
        {
        }

        public PngRenderTarget(string fileName, ushort width, ushort height, PixelFormat format = PixelFormat.Format32bppArgb) 
            : base(fileName, width, height, format, ImageFormat.Png)
        {
        }
    }
}