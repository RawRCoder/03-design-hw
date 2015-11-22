using System;
using System.Drawing;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class SimpleWordRenderer<T> : WordRenderer<T>
        where T : Word
    {
        static readonly Random R = new Random();
        public int MinFontSize { get; set; } = 6;
        public int MaxFontSize { get; set; } = 36;
        public Func<Color> BasicColor { get; set; } = () => Color.FromArgb(R.Next(125, 255), R.Next(125, 255), R.Next(125, 255));
        public FontFamily BasicFont { get; set; } = FontFamily.GenericSansSerif;

        public override Color GetColorFor(T word)
            => Color.FromArgb((int) (word.PercantageWeight*255), BasicColor());
        public override Font GetFontFor(T word)
            => new Font(BasicFont, (int) (word.PercantageWeight*(MaxFontSize - MinFontSize) + MinFontSize));
        public override SizeF CalculateSize(T word, Graphics g)
        {
            return g.MeasureString(word.Text, GetFontFor(word));
        }
    }
}