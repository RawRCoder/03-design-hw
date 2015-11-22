using System.Drawing;
using DesignHw.Text;

namespace DesignHw.Rendering
{
    public abstract class WordRenderer<TWord> where TWord : Word
    {
        public abstract SizeF CalculateSize(TWord word, Graphics g);
        public abstract Color GetColorFor(TWord word);
        public abstract Font GetFontFor(TWord word);
        public virtual Brush GetBrushFor(TWord word) 
            => new SolidBrush(GetColorFor(word));

        public virtual void DrawWord(TWord word, Graphics g, PointF position)
        {
            g.DrawString(word.Text, GetFontFor(word), GetBrushFor(word), position);
        }
    }
}
