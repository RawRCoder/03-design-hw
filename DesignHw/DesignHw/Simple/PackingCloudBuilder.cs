using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class PackingCloudBuilder<TWord> : CloudBuilder<TWord> where TWord : Word
    {
        public override Cloud<TWord> Build(WordsCollection<TWord> words, WordRenderer<TWord> renderer, Graphics g)
        {
            var blocks = Prepare(words, renderer, g).ToList();
           // blocks.Sort((a, b) => a.Size.Height.CompareTo(b.Size.Height));
            Pack(blocks, g);
            return new Cloud<TWord>(blocks);
        }

        private void Pack(IEnumerable<Block<TWord>> blocks, Graphics g)
        {
            var maxx = (int) Math.Ceiling(g.VisibleClipBounds.Width) - 1;
            var level = new int[maxx+1];
            foreach (var bl in blocks)
            {
                var height = level[maxx];
                var width = 1;
                for (var x = maxx-1; x >= 0; --x)
                {
                    if (level[x] > height)
                    {
                        height = level[x];
                    }
                    ++width;
                    if (bl.Size.Width <= width)
                    {
                        bl.LeftTop = new PointF(maxx - width, height);
                        break;
                    }
                }

                height = level[0];
                width = 1;
                var startX = 0;
                for (var x = 1; x <= maxx; ++x)
                {
                    if (level[x] < bl.LeftTop.Y && height > bl.LeftTop.Y)
                    {
                        width = 1;
                        startX = x;
                        height = level[x];
                    }

                    if (level[x] > height)
                    {
                        height = level[x];
                    }
                    ++width;
                    if (bl.Size.Width <= width && height <= bl.LeftTop.Y)
                    {
                        bl.LeftTop = new PointF(startX, height);
                        break;
                    }
                }

                if (g.VisibleClipBounds.Height - height < bl.Size.Height)
                {
                    bl.LeftTop = new PointF(-bl.Size.Width, -bl.Size.Height);
                    continue;
                }

                for (var x = 0; x < (int)bl.Size.Width; ++x)
                {
                    level[(int) bl.LeftTop.X + x] = (int) (bl.LeftTop.Y + bl.Size.Height);
                }
            }
        }

        private IEnumerable<Block<TWord>> Prepare(IEnumerable<TWord> words, WordRenderer<TWord> renderer, Graphics g)
        {
            foreach (var word in words)
            {
                var sz = renderer.CalculateSize(word, g);
                if (sz.Width > g.VisibleClipBounds.Width || sz.Height > g.VisibleClipBounds.Height)
                    continue;
                yield return new Block<TWord>(new PointF(-sz.Width, -sz.Height), sz) { Data = word };
            }
        }
    }
}