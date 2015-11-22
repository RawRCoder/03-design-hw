using System;
using System.Collections.Generic;
using System.Drawing;
using DesignHw.Graphics;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class RandomCloudBuilder<TWord> : CloudBuilder<TWord> 
        where TWord : Word
    {
        readonly Random _r = new Random();
        public override Cloud<TWord> Build(WordsCollection<TWord> words, WordRenderer<TWord> renderer, System.Drawing.Graphics g) 
            => new Cloud<TWord>(ContinousBuild(words, renderer, g));

        private IEnumerable<Block<TWord>> ContinousBuild(IEnumerable<TWord> words, WordRenderer<TWord> renderer, System.Drawing.Graphics g)
        {
            foreach (var word in words)
            {
                var sz = renderer.CalculateSize(word, g);
                var region = new RectangleF(g.VisibleClipBounds.Location, g.VisibleClipBounds.Size - sz);
                yield return new Block<TWord>(_r.RandomPoint(region), sz) {Data = word};
            }
        }
    }
}