using System;
using System.Linq;
using DesignHw.Adapters;
using DesignHw.Text;
using NHunspell;

namespace DesignHw.Simple
{
    public class SimpleWordsCollectionBuilder<T> : WordsCollectionBuilder<T> where T : Word
    {
        public Hunspell Hunspell { get; }
        public SimpleWordsCollectionBuilder(Func<string, T> wordConstructor, Hunspell hunspell, params string[] restricted)
            : base(wordConstructor, restricted)
        {
            Hunspell = hunspell;
        }

        public override string Normalize(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return null;
            
            var stemmedWord = Hunspell.Stem(word).FirstOrDefault() ?? word;

            stemmedWord = stemmedWord.Trim().ToUpperInvariant();
            stemmedWord = new string(stemmedWord.Where(char.IsLetterOrDigit).ToArray());

            return string.IsNullOrWhiteSpace(stemmedWord) ? null : stemmedWord;
        }

        public override bool IsWordSuitable(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            if (RestrictedWords.Contains(word))
                return false;

            return true;
        }

        protected override void OnEncounterWord(Word word)
        {
            ++word.Weight;
        }
    }
}