using System.Drawing;
using DesignHw.Text;

namespace DesignHw.Rendering
{
    public abstract class CloudBuilder<TWord>
        where TWord : Word
    {
        public abstract Cloud<TWord> Build(WordsCollection<TWord> words, WordRenderer<TWord> renderer, Graphics g);
    }
}
