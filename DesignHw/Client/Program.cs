﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using CommandLine;
using DesignHw;
using DesignHw.Adapters;
using DesignHw.Rendering;
using DesignHw.Simple;
using DesignHw.Text;
using NHunspell;
using Ninject;

namespace Client
{
    class Program
    {
        static void DoBindings(StandardKernel di, IEnumerable<string> restrictedWords, CommandLineArgs clargs)
        {
            di.Bind<WordsCollectionBuilder>().To<SimpleWordsCollectionBuilder>()
                .WithConstructorArgument("restricted", restrictedWords);
            di.Bind<CloudBuilder>().To<PackingCloudBuilder>();
            di.Bind<WordDrawingStyle>().To<SimpleWordDrawingStyle>();
            di.Bind<IWordsExtractor>().To<FileWordsExtractor>().WithConstructorArgument(typeof(string), clargs.InputFile);
            di.Bind<IRenderTarget>().To<ImageFileRenderTarget>()
                .WithConstructorArgument(typeof(ImageFormat), ImageFormat.Png)
                .WithConstructorArgument(typeof(string), clargs.OutputFile)
                .WithConstructorArgument("width", clargs.Width)
                .WithConstructorArgument("height", clargs.Height);
            di.Bind<Hunspell>().ToConstant(new Hunspell(clargs.AffFile, clargs.DicFile));
        }
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
            DoBindings(di, restrictedWords, clargs);

            try
            {
                Console.WriteLine("Drawing ...");
                var pl = di.Get<CloudDrawingPipeline>();
                pl.DrawCloud(di.Get<IWordsExtractor>(), di.Get<IRenderTarget>());
                Console.WriteLine("Done!");
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
