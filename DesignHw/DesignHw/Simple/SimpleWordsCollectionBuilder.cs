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
            
            word = Hunspell.Stem(word).FirstOrDefault() ?? word;

            word = word.Trim().ToUpperInvariant();
            word = new string(word.Where(char.IsLetterOrDigit).ToArray());

            return string.IsNullOrWhiteSpace(word) ? null : word;
        }

        public override bool IsWordSuitable(string word)
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