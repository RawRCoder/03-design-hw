using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DesignHw;
using DesignHw.Graphics;
using DesignHw.Simple;
using DesignHw.Text;

namespace Client
{
    class Program
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
        }

        static void Main(string[] args)
        {
            var clargs = new CommandLineArgs();
            if (!Parser.Default.ParseArguments(args, clargs))
                return;
            
            var pl = new CloudDrawingPipeline<Word>(
                new SimpleWordsCollectionBuilder<Word>(s => new Word(s)),
                new RandomCloudBuilder<Word>(), 
                new SimpleWordRenderer<Word>());

            var img = new Bitmap(clargs.Width, clargs.Height);
            var g = Graphics.FromImage(img);
            pl.DrawCloud(File.ReadAllText(clargs.InputFile, Encoding.UTF8), g);
            img.Save(clargs.OutputFile, ImageFormat.Png);
        }
    }
}
