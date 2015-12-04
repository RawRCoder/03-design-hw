using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DesignHw.Text
{
    public class WordsCollection : IEnumerable<Word> 
    {
        public WordsCollection(IEnumerable<Word> words)
        {
            var coll = words.ToList();
            coll.Sort((x, y) => -x.CompareTo(y));
            Collection = coll.ToArray();
            TotalWords = (uint)Collection.Length;

            foreach (var w in Collection)
            {
                w.PercantageWeight = (double) (w.Weight/coll[0].Weight);
            }
        }

        protected Word[] Collection { get; }
        public uint TotalWords { get; }

        public Word this[uint id]
            => Collection[id];

        public IEnumerator<Word> GetEnumerator()
            => Collection.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }
}
