using System;
using System.Drawing;
using DesignHw.Graphics;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class SimpleWordRenderer<T> : WordRenderer<T>
        where T : Word
    {
        public int MinFontSize { get; set; } = 6;
        public int MaxFontSize { get; set; } = 36;
        public Func<Color> BasicColor { get; set; } = () => Color.Red;
        public FontFamily BasicFont { get; set; } = FontFamily.GenericSansSerif;

        public override Color GetColorFor(T word)
            => Color.FromArgb((int) (word.PercantageWeight*255), BasicColor());
        public override Font GetFontFor(T word)
            => new Font(BasicFont, (int) (word.PercantageWeight*(MaxFontSize - MinFontSize) + MinFontSize));
        public override SizeF CalculateSize(T word, System.Drawing.Graphics g)
        {
            return g.MeasureString(word.Text, GetFontFor(word));
        }
    }
}