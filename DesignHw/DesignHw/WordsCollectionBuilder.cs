﻿using System;
using System.Collections.Generic;

namespace DesignHw
{
    public abstract class WordsCollectionBuilder<T> where T:Word
    {
        protected WordsCollectionBuilder(Func<string, T> wordConstructor)
        {
            WordConstructor = wordConstructor;
        }

        public virtual Func<string, T> WordConstructor { get; }
        private Dictionary<string, T> Collection { get; } = new Dictionary<string, T>();
        
        public abstract string Normalize(string word);
        public abstract bool IsAGoodWord(string word);
        public abstract void OnEncounterWord(Word word);

        public uint TotalWords { get; private set; }


        public bool Register(string word)
        {
            word = Normalize(word);
            if (!IsAGoodWord(word))
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

        public virtual WordsCollection<T> Build()
            => new WordsCollection<T>(Collection.Values);
    }
}