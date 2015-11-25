using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DesignHw.Adapters
{
    public class ImageRenderTarget : IRenderTarget
    {
        public Image Image { get; }
        private Lazy<Graphics> LazyGraphics { get; }

        public ImageRenderTarget(Image image)
        {
            Image = image;
            LazyGraphics = new Lazy<Graphics>(() => Graphics.FromImage(Image));
        }
        public ImageRenderTarget(ushort width, ushort height, PixelFormat format = PixelFormat.Format32bppArgb)
            :this(new Bitmap(width, height, format))
        {
        }

        public Graphics GetGraphics()
            => LazyGraphics.Value;
        public virtual void Close(Graphics g) { }
    }
}