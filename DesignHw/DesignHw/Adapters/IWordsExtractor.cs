using System.Collections.Generic;

namespace DesignHw.Adapters
{
    public interface IWordsExtractor
    {
        IEnumerable<string> Words { get; }
    }
}