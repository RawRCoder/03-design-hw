using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw.Simple
{
    public class PackingCloudBuilder : CloudBuilder
    {
        public override Cloud Build(WordsCollection words, WordRenderer renderer, Graphics g)
        {
            var blocks = BuildBlocks(words, renderer, g).ToList();
            Pack(blocks, g);
            return new Cloud(blocks);
        }

        private void Pack(IEnumerable<Block> blocks, Graphics g)
        {
            var maxX = (int) Math.Ceiling(g.VisibleClipBounds.Width) - 1;
            var level = new int[maxX + 1];
            foreach (var bl in blocks)
                PackBlock(bl, maxX, level, g);
        }

        private void PackBlock(Block bl, int maxX, int[] level, Graphics g)
        {
            TryPlaceBlockRightToLeft(bl, maxX, level);
            TryPlaceBlockLeftToRight(bl, maxX, level);

            if (g.VisibleClipBounds.Height - bl.LeftTop.Y < bl.Size.Height)
            {
                bl.LeftTop = new PointF(-bl.Size.Width, -bl.Size.Height);
                return;
            }
            AddBlockToLevel(bl, level);
        }

        private void TryPlaceBlockRightToLeft(Block bl, int maxX, int[] level)
        {
            var height = level[maxX];
            var width = 1;
            for (var x = maxX - 1; x >= 0; --x)
            {
                if (level[x] > height)
                    height = level[x];
                ++width;
                if (bl.Size.Width <= width)
                {
                    bl.LeftTop = new PointF(maxX - width, height);
                    break;
                }
            }
        }
        private void TryPlaceBlockLeftToRight(Block bl, int maxX, int[] level)
        {
            var height = level[0];
            var width = 1;
            var startX = 0;
            for (var x = 1; x <= maxX; ++x)
            {
                if (level[x] < bl.LeftTop.Y && height > bl.LeftTop.Y)
                {
                    width = 1;
                    startX = x;
                    height = level[x];
                }

                if (level[x] > height)
                    height = level[x];
                ++width;
                if (bl.Size.Width <= width && height <= bl.LeftTop.Y)
                {
                    bl.LeftTop = new PointF(startX, height);
                    break;
                }
            }
        }
        private void AddBlockToLevel(Block bl, int[] level)
        {
            for (var x = 0; x < (int)bl.Size.Width; ++x)
                level[(int)bl.LeftTop.X + x] = (int)(bl.LeftTop.Y + bl.Size.Height);
            
        }

        private IEnumerable<Block> BuildBlocks(IEnumerable<Word> words, WordRenderer renderer, Graphics g)
        {
            foreach (var word in words)
            {
                var sz = renderer.CalculateSize(word, g);
                if (sz.Width > g.VisibleClipBounds.Width || sz.Height > g.VisibleClipBounds.Height)
                    continue;
                yield return new Block(new PointF(-sz.Width, -sz.Height), sz) { Data = word };
            }
        }
    }
}