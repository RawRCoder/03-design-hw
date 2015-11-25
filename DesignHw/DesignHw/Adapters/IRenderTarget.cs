using System.Drawing;

namespace DesignHw.Adapters
{
    public interface IRenderTarget
    {
        Graphics GetGraphics();
        void Close(Graphics g);
    }
}