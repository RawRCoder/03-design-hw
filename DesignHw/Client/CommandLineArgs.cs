using CommandLine;
using CommandLine.Text;

namespace Client
{
    class CommandLineArgs
    {
        [Option('s', "src", HelpText = "Source file name (text-file)")]
        public string InputFile { get; set; }
        [Option('t', "target", HelpText = "Target file name (image)")]
        public string OutputFile { get; set; }
        [Option('w', "width", HelpText = "Image width in pixels", DefaultValue = (ushort)800)]
        public ushort Width { get; set; }
        [Option('h', "height", HelpText = "Image height in pixels", DefaultValue = (ushort)600)]
        public ushort Height { get; set; }
        [Option('a', "aff", HelpText = "AFF file for text normalization", DefaultValue = "ru_RU.aff")]
        public string AffFile { get; set; }
        [Option('d', "dic", HelpText = "DIC file for text normalization", DefaultValue = "ru_RU.dic")]
        public string DicFile { get; set; }
        [Option('r', "restrict", HelpText = "Text file with restricted words in it", DefaultValue = "restricted.txt")]
        public string RestrictedFileName { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}