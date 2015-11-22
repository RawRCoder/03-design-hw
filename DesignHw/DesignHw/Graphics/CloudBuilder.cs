using DesignHw.Text;

namespace DesignHw.Graphics
{
    public abstract class CloudBuilder<TWord>
        where TWord : Word
    {
        public abstract Cloud<TWord> Build(WordsCollection<TWord> words, WordRenderer<TWord> renderer, System.Drawing.Graphics g);
    }
}
