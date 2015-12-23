using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesignHw.Adapters
{
    public static class FileWordsExtractor
    {
        public static IEnumerable<string> GetWordsFromFile(string fileName) 
            => File.ReadLines(fileName, Encoding.UTF8).SelectMany(s => s.Split());
        
    }
}