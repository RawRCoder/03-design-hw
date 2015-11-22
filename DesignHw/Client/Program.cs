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
using DesignHw.Simple;
using DesignHw.Text;
using NHunspell;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var clargs = new CommandLineArgs();
            if (!Parser.Default.ParseArguments(args, clargs))
                return;
            if (string.IsNullOrWhiteSpace(clargs.InputFile))
            {
                Console.WriteLine(clargs.GetUsage());
                return;
            }

            if (string.IsNullOrWhiteSpace(clargs.OutputFile))
                clargs.OutputFile = Path.ChangeExtension(clargs.InputFile, ".png");

            try
            {
                using (var hunspell = new Hunspell(clargs.AffFile, clargs.DicFile))
                {
                    var wordsCollectionBuilder = new SimpleWordsCollectionBuilder<Word>(s => new Word(s), hunspell);
                    wordsCollectionBuilder.RestrictedWords.UnionWith("а из у от не и то это да ни как так но также тоже или то это либо толи да за с он она в на что его".ToUpperInvariant().Split());
                    var pl = new CloudDrawingPipeline<Word>(
                        wordsCollectionBuilder,
                        new RandomCloudBuilder<Word>(),
                        new SimpleWordRenderer<Word>());
                    //
                    var img = new Bitmap(clargs.Width, clargs.Height);
                    var g = Graphics.FromImage(img);
                    pl.DrawCloud(File.ReadAllText(clargs.InputFile, Encoding.UTF8), g);
                    img.Save(clargs.OutputFile, ImageFormat.Png);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден - '{ex.FileName}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
