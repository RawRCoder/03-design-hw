using System;
using System.Collections.Generic;
using System.Drawing;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class RandomCloudBuilder : CloudBuilder
    {
        readonly Random _r = new Random();
        public override Cloud Build(WordsCollection words, WordRenderer renderer, Graphics g) 
            => new Cloud(ContinousBuild(words, renderer, g));

        private IEnumerable<Block> ContinousBuild(IEnumerable<Word> words, WordRenderer renderer, Graphics g)
        {
            foreach (var word in words)
            {
                var sz = renderer.CalculateSize(word, g);
                var region = new RectangleF(g.VisibleClipBounds.Location, g.VisibleClipBounds.Size - sz);
                yield return new Block(_r.RandomPoint(region), sz) {Data = word};
            }
        }
    }
}