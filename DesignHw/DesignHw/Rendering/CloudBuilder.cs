using System.Drawing;
using DesignHw.Text;

namespace DesignHw.Rendering
{
    public abstract class CloudBuilder
    {
        public abstract Cloud Build(WordsCollection words, WordRenderer renderer, Graphics g);
    }
}
