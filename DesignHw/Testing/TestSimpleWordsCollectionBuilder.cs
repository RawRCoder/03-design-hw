using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using DesignHw.Simple;
using DesignHw.Text;
using NHunspell;
using Ninject;

namespace Testing
{
    [TestFixture]
    public class TestSimpleWordsCollectionBuilder
    {
        private WordsCollectionBuilder builder;
        [SetUp]
        public void Init()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            Environment.CurrentDirectory = path;

            var asm = Assembly.GetAssembly(typeof(Hunspell));
            var type = asm.GetType("NHunspell.MarshalHunspellDll", true);
            var field = type.GetField("nativeDLLPath", BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(null, path);

            var di = new StandardKernel();
            di.Bind<Func<string, Word>>().ToConstant<Func<string, Word>>(s => new Word(s));
            di.Bind<WordsCollectionBuilder>().To<SimpleWordsCollectionBuilder>();
            di.Bind<Hunspell>().ToConstant(new Hunspell("ru_RU.aff", "ru_RU.dic"));

            builder = di.Get<WordsCollectionBuilder>();
        }

        [TestCase("", ExpectedResult = null, TestName = "Empty string")]
        [TestCase(" ", ExpectedResult = null)]
        [TestCase("Kek", ExpectedResult = "KEK")]
        [TestCase("азАзА", ExpectedResult = "АЗАЗА")]
        [TestCase("азАзА.", ExpectedResult = "АЗАЗА")]
        [TestCase("азАзА46.", ExpectedResult = "АЗАЗА46")]
        [TestCase("http://microsoft.com", ExpectedResult = "HTTPMICROSOFTCOM")]
        public string TestNormalization(string word)
            => builder.Normalize(word);

        [TestCase(null, ExpectedResult = false)]
        [TestCase("ЛАЛ", ExpectedResult = true)]
        public bool TestSuitable(string word)
            => builder.IsWordSuitable(word);
        
        [TestCase("ЛАЛ", new[] { "ЛАЛ" }, ExpectedResult = false)]
        [TestCase("ЛАЛ", new[] { "ЛАЛА" }, ExpectedResult = true)]
        [TestCase("ЛАЛА", new [] { "ЛАЛ"}, ExpectedResult = true)]
        public bool TestSuitable(string word, params string[] restricted)
        {
            builder.RestrictedWords.Clear();
            builder.RestrictedWords.UnionWith(restricted);
            var result = builder.IsWordSuitable(word);
            return result;
        }
    }
}
