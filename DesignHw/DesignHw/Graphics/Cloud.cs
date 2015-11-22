using System.Collections;
using System.Collections.Generic;
using DesignHw.Text;

namespace DesignHw.Graphics
{
    public class Cloud<TWord> : IEnumerable<Block<TWord>> 
        where TWord : Word
    {
        private List<Block<TWord>> Blocks { get; }

        public Cloud(IEnumerable<Block<TWord>> blocks)
        {
            Blocks = new List<Block<TWord>>(blocks);
        }

        public virtual void Render(System.Drawing.Graphics g, WordRenderer<TWord> renderer)
        {
            foreach (var block in Blocks)
            {
                renderer.DrawWord(block.Data, g, block.LeftTop);
            }
        }

        public IEnumerator<Block<TWord>> GetEnumerator()
            => Blocks.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }
}