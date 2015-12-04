using System;
using System.Collections.Generic;
using System.Linq;
using DesignHw.Adapters;

namespace DesignHw.Text
{
    public abstract class WordsCollectionBuilder
    {
        protected WordsCollectionBuilder(params string[] restricted)
        {
            RestrictedWords = new HashSet<string>(restricted);
        }
        
        private Dictionary<string, Word> Collection { get; } = new Dictionary<string, Word>();
        
        public abstract string Normalize(string word);
        public abstract bool IsWordSuitable(string word);
        protected abstract void OnEncounterWord(Text.Word word);
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
                w = new Word(word);
                Collection.Add(word, w);
            }
            OnEncounterWord(w);
            return true;
        }
        public bool TryRegister(IWordsExtractor extractor) 
            => extractor.Words.Count(TryRegister) > 0;

        public virtual WordsCollection Build()
            => new WordsCollection(Collection.Values);
    }
}