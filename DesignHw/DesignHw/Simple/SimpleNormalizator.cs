using System.Linq;
using DesignHw.Text;
using NHunspell;

namespace DesignHw.Simple
{
    public class SimpleNormalizator : WordNormalizator
    {
        public Hunspell Hunspell { get; }
        public SimpleNormalizator(Hunspell hunspell, params string[] restricted)
            : base(restricted)
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
            => !string.IsNullOrWhiteSpace(word)&&!RestrictedWords.Contains(word);
    }
}