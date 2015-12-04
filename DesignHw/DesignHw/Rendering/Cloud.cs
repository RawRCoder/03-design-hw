using System.Collections;
using System.Collections.Generic;

namespace DesignHw.Rendering
{
    public class Cloud: IEnumerable<Block> 
    {
        private List<Block> Blocks { get; }

        public Cloud(IEnumerable<Block> blocks)
        {
            Blocks = new List<Block>(blocks);
        }

        public IEnumerator<Block> GetEnumerator()
            => Blocks.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }
}