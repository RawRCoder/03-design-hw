using System.Drawing;
using DesignHw.Text;

namespace DesignHw.Rendering
{
    public delegate Cloud CloudBuilder(WordsCollection words, WordRenderer renderer, Graphics g);
}
