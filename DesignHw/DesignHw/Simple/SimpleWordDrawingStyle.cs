using System;
using System.Drawing;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class SimpleWordDrawingStyle : WordDrawingStyle
    {
        static readonly Random R;
        static SimpleWordDrawingStyle()
        {
            R = new Random();
        }

        public int MinFontSize { get; set; } = 6;
        public int MaxFontSize { get; set; } = 72;
        public Func<Color> BasicColor { get; set; } = 
            () => Extensions.FromAHSB(255, R.NextSingle(0, 360), 1f, R.NextSingle(0.5f, 0.75f));
        public FontFamily BasicFont { get; set; } = FontFamily.GenericSansSerif;

        public override Color GetColorFor(Word word)
            => Color.FromArgb((int) (Math.Sqrt(word.PercantageWeight )* 255), BasicColor());
        public override Font GetFontFor(Word word)
            => new Font(BasicFont, (int) (word.PercantageWeight*(MaxFontSize - MinFontSize) + MinFontSize));
        public override SizeF CalculateSize(Word word, Graphics g) 
            => g.MeasureString(word.Text, GetFontFor(word));
    }
}