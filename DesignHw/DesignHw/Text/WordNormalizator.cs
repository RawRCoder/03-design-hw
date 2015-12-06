using System.Collections.Generic;

namespace DesignHw.Text
{
    public abstract class WordNormalizator
    {
        public HashSet<string> RestrictedWords { get; set; }

        protected WordNormalizator(params string[] restricted)
        {
            RestrictedWords = new HashSet<string>(restricted);
        }
        protected WordNormalizator(HashSet<string> restricted)
        {
            RestrictedWords = restricted;
        }
        public abstract string Normalize(string word);

        public virtual bool IsWordSuitable(string word)
        {
            var nWord = Normalize(word);
            if (string.IsNullOrEmpty(nWord))
                return false;
            return !RestrictedWords.Contains(nWord);
        }
    }
}