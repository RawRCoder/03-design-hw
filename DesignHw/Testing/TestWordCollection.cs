using System.Linq;
using DesignHw.Text;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class TestWordCollection
    {
        [Test]
        public void ShouldBeEmptyOnEmptyInput()
        {
            var wc = new WordsCollection<Word>(new Word[] {});
            Assert.IsEmpty(wc);
        }

        [Test]
        public void ShouldReturnTheOnlyInputedWordAndCalculateItsPercantageWeight()
        {
            var kek = new Word("KEK") {Weight = 100500};
            var wc = new WordsCollection<Word>(new [] {kek});
            var coll = wc.ToArray();
            Assert.AreEqual(1, coll.Length);
            Assert.AreSame(kek, coll.First());
            Assert.AreEqual(1.00, kek.PercantageWeight);
            Assert.AreEqual(100500, kek.Weight);
        }

        [Test]
        public void ShouldSortAndCalculatePercantageWeight()
        {
            var word1 = new Word("KEK") { Weight = 20 };
            var word2 = new Word("LOL") { Weight = 10 };
            var wc = new WordsCollection<Word>(new[] { word2, word1 });
            var coll = wc.ToArray();
            Assert.AreEqual(2, coll.Length);
            Assert.AreSame(word1, coll.First());
            Assert.AreSame(word2, coll.Last());
            Assert.AreEqual(1.00, word1.PercantageWeight);
            Assert.AreEqual(0.50, word2.PercantageWeight);
        }
    }
}
