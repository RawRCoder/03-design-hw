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
        FakeNormailzer _normalizer;
        [SetUp]
        public void Init()
        {
            _normalizer = new FakeNormailzer();
        }


        [Test]
        public void TestEmpty()
        {
            var result = DefaultWordsCollectionBuilder.BuildWordCollection(_normalizer, new string[0]);
            Assert.AreNotEqual(null, result);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void TestSingle()
        {
            var result = DefaultWordsCollectionBuilder.BuildWordCollection(_normalizer, new[] { "Lol" }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreNotEqual(null, result[0]);
            Assert.AreEqual("LOL", result[0].Text);
            Assert.AreEqual((decimal)1, result[0].Weight);
        }
        [Test]
        public void TestPairOfSame()
        {
            var result = DefaultWordsCollectionBuilder.BuildWordCollection(_normalizer, new[] { "Lol", "Lol" }).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreNotEqual(null, result[0]);
            Assert.AreEqual("LOL", result[0].Text);
            Assert.AreEqual((decimal)2, result[0].Weight);
        }
        [Test]
        public void TestFull()
        {
            var result = DefaultWordsCollectionBuilder.BuildWordCollection(_normalizer, new[] { "Lol", "Kek", "lol" }).ToArray();
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
