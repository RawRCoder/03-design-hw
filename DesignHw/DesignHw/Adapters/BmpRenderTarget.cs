using System.Drawing;
using System.Drawing.Imaging;

namespace DesignHw.Adapters
{
    public class BmpRenderTarget : ImageFileRenderTarget
    {
        public BmpRenderTarget(string fileName, Image image)
            : base(fileName, image, ImageFormat.Bmp)
        {
        }

        public BmpRenderTarget(string fileName, ushort width, ushort height, PixelFormat format = PixelFormat.Format32bppArgb)
            : base(fileName, width, height, format, ImageFormat.Bmp)
        {
        }
    }
}