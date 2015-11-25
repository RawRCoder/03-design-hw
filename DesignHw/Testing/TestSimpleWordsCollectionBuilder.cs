using NUnit.Framework;
using System;
using DesignHw.Simple;
using DesignHw.Text;
using NHunspell;
using Ninject;

namespace Testing
{
    [TestFixture]
    public class TestSimpleWordsCollectionBuilder
    {
        private WordsCollectionBuilder<Word> builder;
        [SetUp]
        public void Init()
        {
            var di = new StandardKernel();
            di.Bind<Func<string, Word>>().ToConstant<Func<string, Word>>(s => new Word(s));
            di.Bind<WordsCollectionBuilder<Word>>().To<SimpleWordsCollectionBuilder<Word>>();
            di.Bind<Hunspell>().ToConstant(new Hunspell("ru_RU.aff", "ru_RU.dic"));

            builder = di.Get<WordsCollectionBuilder<Word>>();
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
            builder.RestrictedWords.UnionWith(restricted);
            var result = builder.IsWordSuitable(word);
            builder.RestrictedWords.Clear();
            return result;
        }
    }
}
