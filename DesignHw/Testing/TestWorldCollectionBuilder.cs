using System.Linq;
using DesignHw.Text;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class TestWorldCollectionBuilder
    {
        class FakeNormailzer : WordNormalizator
        {
            public override string Normalize(string word) => word.ToUpperInvariant();
            public override bool IsWordSuitable(string word) => !string.IsNullOrWhiteSpace(word);
        }
        WordsCollectionBuilder _builder;
        [SetUp]
        public void Init()
        {
            _builder = new WordsCollectionBuilder(new FakeNormailzer());
        }


        [Test]
        public void TestEmpty()
        {
            var result = _builder.Build(new string[0]);
            Assert.AreNotEqual(null, result);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TestSingle()
        {
            var result = _builder.Build(new[] { "Lol" }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreNotEqual(null, result[0]);
            Assert.AreEqual("LOL", result[0].Text);
            Assert.AreEqual((decimal)1, result[0].Weight);
        }
        [Test]
        public void TestPairOfSame()
        {
            var result = _builder.Build(new[] { "Lol", "Lol" }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreNotEqual(null, result[0]);
            Assert.AreEqual("LOL", result[0].Text);
            Assert.AreEqual((decimal)2, result[0].Weight);
        }
        [Test]
        public void TestFull()
        {
            var result = _builder.Build(new[] { "Lol", "Kek", "lol" }).ToArray();
            Assert.AreEqual(2, result.Length);
            Assert.AreNotEqual(null, result[0]);
            Assert.AreEqual("LOL", result[0].Text);
            Assert.AreEqual((decimal)2, result[0].Weight);
            Assert.AreNotEqual(null, result[1]);
            Assert.AreEqual("KEK", result[1].Text);
            Assert.AreEqual((decimal)1, result[1].Weight);
        }
    }
}
