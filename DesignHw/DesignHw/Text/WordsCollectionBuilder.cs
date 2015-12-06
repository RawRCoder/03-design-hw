using System.Collections.Generic;
using System.Linq;

namespace DesignHw.Text
{
    public class WordsCollectionBuilder
    {
        protected WordsCollectionBuilder(WordNormalizator normalizator)
        {
            Normalizator = normalizator;
        }

        public WordNormalizator Normalizator { get; }
        public uint TotalWords { get; private set; }
        private Dictionary<string, Word> Collection { get; } = new Dictionary<string, Word>();

        public bool TryRegister(string word)
        {
            if (!Normalizator.IsWordSuitable(word))
                return false;

            ++TotalWords;

            var w = Collection.SafeGet(word);
            if (w == null)
            {
                w = new Word(word);
                Collection.Add(word, w);
            }
            ++w.Weight;
            return true;
        }
        public bool TryRegister(IEnumerable<string> words)
            => words.Count(TryRegister) > 0;

        public virtual WordsCollection Build()
            => new WordsCollection(Collection.Values);
        public WordsCollection Build(IEnumerable<string> words)
        {
            TryRegister(words);
            return Build();
        }
    }
}