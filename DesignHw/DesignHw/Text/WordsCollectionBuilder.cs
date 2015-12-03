using System;
using System.Collections.Generic;
using System.Linq;
using DesignHw.Adapters;

namespace DesignHw.Text
{
    public abstract class WordsCollectionBuilder<T> where T:Word
    {
        protected WordsCollectionBuilder(Func<string, T> wordConstructor, params string[] restricted)
        {
            WordConstructor = wordConstructor;
            RestrictedWords = new HashSet<string>(restricted);
        }

        public virtual Func<string, T> WordConstructor { get; }
        private Dictionary<string, T> Collection { get; } = new Dictionary<string, T>();
        
        public abstract string Normalize(string word);
        public abstract bool IsWordSuitable(string word);
        protected abstract void OnEncounterWord(Word word);
        public HashSet<string> RestrictedWords { get; set; }

        public uint TotalWords { get; private set; }


        public bool TryRegister(string word)
        {
            word = Normalize(word);
            if (!IsWordSuitable(word))
                return false;

            ++TotalWords;

            var w = Collection.SafeGet(word);
            if (w == null)
            {
                w = WordConstructor(word);
                Collection.Add(word, w);
            }
            OnEncounterWord(w);
            return true;
        }
        public bool TryRegister(IWordsExtractor extractor) 
            => extractor.Words.Count(TryRegister) > 0;

        public virtual WordsCollection<T> Build()
            => new WordsCollection<T>(Collection.Values);
    }
}