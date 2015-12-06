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
    public class TestSimpleNormalizator
    {
        private WordNormalizator _normalizator;
        [SetUp]
        public void Init()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            Environment.CurrentDirectory = path;

            var asm = Assembly.GetAssembly(typeof(Hunspell));
            var type = asm.GetType("NHunspell.MarshalHunspellDll", true);
            var field = type.GetField("nativeDLLPath", BindingFlags.NonPublic | BindingFlags.Static);
            // ReSharper disable once PossibleNullReferenceException
            field.SetValue(null, path);

            var di = new StandardKernel();
            di.Bind<Func<string, Word>>().ToConstant<Func<string, Word>>(s => new Word(s));
            di.Bind<WordNormalizator>().To<SimpleNormalizator>();
            di.Bind<Hunspell>().ToConstant(new Hunspell("ru_RU.aff", "ru_RU.dic"));

            _normalizator = di.Get<WordNormalizator>();
        }

        [TestCase("", ExpectedResult = null, TestName = "Empty string")]
        [TestCase(" ", ExpectedResult = null)]
        [TestCase("Kek", ExpectedResult = "KEK")]
        [TestCase("азАзА", ExpectedResult = "АЗАЗА")]
        [TestCase("азАзА.", ExpectedResult = "АЗАЗА")]
        [TestCase("азАзА46.", ExpectedResult = "АЗАЗА46")]
        [TestCase("http://microsoft.com", ExpectedResult = "HTTPMICROSOFTCOM")]
        public string TestNormalization(string word)
            => _normalizator.Normalize(word);

        [TestCase(null, ExpectedResult = false)]
        [TestCase("ЛАЛ", ExpectedResult = true)]
        public bool TestSuitable(string word)
            => _normalizator.IsWordSuitable(word);
        
        [TestCase("ЛАЛ", new[] { "ЛАЛ" }, ExpectedResult = false)]
        [TestCase("ЛАЛ", new[] { "ЛАЛА" }, ExpectedResult = true)]
        [TestCase("ЛАЛА", new [] { "ЛАЛ"}, ExpectedResult = true)]
        public bool TestSuitable(string word, params string[] restricted)
        {
            _normalizator.RestrictedWords.Clear();
            _normalizator.RestrictedWords.UnionWith(restricted);
            var result = _normalizator.IsWordSuitable(word);
            return result;
        }
    }
}
