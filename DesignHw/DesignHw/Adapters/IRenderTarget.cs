using System;
using System.Drawing;

namespace DesignHw.Adapters
{
    public abstract class RenderTarget
    {
        public void Render(Action<Graphics> graphicsUsageFunc)
        {
            var g = GetGraphics();
            graphicsUsageFunc(g);
            Close(g);
        }
        public abstract Graphics GetGraphics();
        public abstract void Close(Graphics g);
    }
}