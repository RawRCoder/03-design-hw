using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DesignHw
{
    public class WordsCollection<T> : IEnumerable<T> 
        where T : Word
    {
        public WordsCollection(IEnumerable<T> words)
        {
            var coll = words.ToList();
            coll.Sort();
            Collection = coll.ToArray();
            TotalWords = (uint)Collection.Length;
        }

        protected T[] Collection { get; }
        public uint TotalWords { get; }

        public T this[uint id]
            => Collection[id];

        public IEnumerator<T> GetEnumerator() 
            => Collection.Select(v => v).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }
}
