using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class RandomCloudBuilder
    {
        public static Cloud BuildCloud(WordsCollection words, WordRenderer renderer, Graphics g)
            => new RandomCloudBuilder().Build(words, renderer, g);

        readonly Random _r = new Random();
        public Cloud Build(WordsCollection words, WordRenderer renderer, Graphics g) 
            => new Cloud(ContinousBuild(words, renderer, g));

        private IEnumerable<Block> ContinousBuild(IEnumerable<Word> words, WordRenderer renderer, Graphics g) 
            => from word in words
               let sz = renderer.CalculateSize(word, g)
               let region = new RectangleF(g.VisibleClipBounds.Location, g.VisibleClipBounds.Size - sz)
               select new Block(_r.RandomPoint(region), sz) {Data = word};
    }
}