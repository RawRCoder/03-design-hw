using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using CommandLine;
using DesignHw;
using DesignHw.Rendering;
using DesignHw.Simple;
using DesignHw.Text;
using NHunspell;
using Ninject;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var clargs = new CommandLineArgs();
            if (!Parser.Default.ParseArguments(args, clargs))
                return;

            if (string.IsNullOrWhiteSpace(clargs.OutputFile))
                clargs.OutputFile = Path.ChangeExtension(clargs.InputFile, ".png");

            var restrictedWordsString = "";
            try
            {
                restrictedWordsString = File.ReadAllText(clargs.RestrictedFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось загрузить файл '{clargs.RestrictedFileName}' - {ex.Message}");
            }
            var restrictedWords = restrictedWordsString.ToUpperInvariant().Split();


            var di = new StandardKernel();
            di.Bind<Func<string, Word>>().ToConstant<Func<string, Word>>(s => new Word(s));
            di.Bind<WordsCollectionBuilder<Word>>().To<SimpleWordsCollectionBuilder<Word>>().WithPropertyValue("RestrictedWords", new HashSet<string>(restrictedWords));
            di.Bind<CloudBuilder<Word>>().To<PackingCloudBuilder<Word>>();
            di.Bind<WordRenderer<Word>>().To<SimpleWordRenderer<Word>>();
            di.Bind<Hunspell>().ToConstant(new Hunspell(clargs.AffFile, clargs.DicFile));

            try
            {
                using (di.Get<Hunspell>())
                {
                    var pl = di.Get<CloudDrawingPipeline<Word>>();
                    
                    var img = new Bitmap(clargs.Width, clargs.Height);
                    var g = Graphics.FromImage(img);
                    g.Clear(Color.Black);
                    Console.WriteLine("Drawing ...");
                    pl.DrawCloud(File.ReadAllText(clargs.InputFile, Encoding.UTF8), g);
                    Console.WriteLine("Saving ...");
                    img.Save(clargs.OutputFile, ImageFormat.Png);
                    Console.WriteLine("Done!");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Файл не найден - '{ex.FileName}'");
            }
            catch (DllNotFoundException ex)
            {
                Console.WriteLine($"Библиотека не найдена - '{ex.Message}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
