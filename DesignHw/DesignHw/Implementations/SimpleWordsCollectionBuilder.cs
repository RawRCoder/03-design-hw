using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignHw.Implementations
{
    public class SimpleWordsCollectionBuilder<T> : WordsCollectionBuilder<T> where T : Word
    {
        public HashSet<string> RestrictedWords { get; } = new HashSet<string>();
        public SimpleWordsCollectionBuilder(Func<string, T> wordConstructor)
            : base(wordConstructor) { }

        public override string Normalize(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return null;

            // TODO: Word normalization / stemming should be nice here

            word = word.Trim().ToUpperInvariant();
            word = new string(word.Where(char.IsLetterOrDigit).ToArray());

            return string.IsNullOrWhiteSpace(word) ? null : word;
        }

        public override bool IsAGoodWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            if (RestrictedWords.Contains(word))
                return false;

            return true;
        }

        public override void OnEncounterWord(Word word)
        {
            ++word.Weight;
        }
    }
}