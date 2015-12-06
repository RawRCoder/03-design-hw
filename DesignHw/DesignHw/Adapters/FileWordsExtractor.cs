using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesignHw.Adapters
{
    public class FileWordsExtractor : IWordsExtractor
    {
        public FileWordsExtractor(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
        public IEnumerable<string> Words 
            => File.ReadLines(FileName, Encoding.UTF8).SelectMany(s => s.Split());
        
    }
}