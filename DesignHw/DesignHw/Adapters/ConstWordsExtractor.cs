using System.Collections.Generic;

namespace DesignHw.Adapters
{
    class ConstWordsExtractor : IWordsExtractor
    {
        public ConstWordsExtractor(params string[] words)
        {
            Words = words;
        }
        
        public IEnumerable<string> Words { get; }
    }
}