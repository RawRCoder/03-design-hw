using System.Drawing;
using DesignHw.Text;

namespace DesignHw.Rendering
{
    public class WordRenderer
    {
        public WordRenderer(WordDrawingStyle style)
        {
            Style = style;
        }
        public WordDrawingStyle Style { get; set; }

        public SizeF CalculateSize(Word word, Graphics g)
            => Style.CalculateSize(word, g);
        protected virtual void DrawWord(Word word, Graphics g, PointF position)
        {
            g.DrawString(word.Text, Style.GetFontFor(word), Style.GetBrushFor(word), position);
        }
        public virtual void Render(Cloud cloud, Graphics g)
        {
            foreach (var block in cloud)
            {
                DrawWord(block.Data, g, block.LeftTop);
            }
        }
    }

    public abstract class WordDrawingStyle
    {
        public abstract SizeF CalculateSize(Word word, Graphics g);
        public abstract Color GetColorFor(Word word);
        public abstract Font GetFontFor(Word word);
        public virtual Brush GetBrushFor(Word word)
            => new SolidBrush(GetColorFor(word));
    }
}
