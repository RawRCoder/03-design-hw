using System.Collections.Generic;
using System.Linq;

namespace DesignHw.Text
{
    public delegate WordsCollection WordsCollectionBuilder(WordNormalizator normalizator, IEnumerable<string> words);
    public static class DefaultWordsCollectionBuilder
    {
        public static WordsCollection BuildWordCollection(WordNormalizator normalizator, IEnumerable<string> words)
            => new WordsCollection(words.Select(normalizator.Normalize)
                .Where(normalizator.IsWordSuitable)
                .GroupBy(s => s)
                .Select(g => new Word(g.Key) {Weight = g.Count()}));
    }
}