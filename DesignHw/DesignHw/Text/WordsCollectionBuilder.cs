using System.Collections.Generic;
using System.Linq;

namespace DesignHw.Text
{
    public class WordsCollectionBuilder
    {
        public WordsCollectionBuilder(WordNormalizator normalizator)
        {
            Normalizator = normalizator;
        }

        public WordNormalizator Normalizator { get; }

        public WordsCollection Build(IEnumerable<string> words)
            => new WordsCollection(words.Select(Normalizator.Normalize)
                .Where(Normalizator.IsWordSuitable)
                .GroupBy(s => s)
                .Select(g => new Word(g.Key) {Weight = g.Count()}));
    }
}