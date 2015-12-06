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

        public abstract bool IsWordSuitable(string word);
    }
}