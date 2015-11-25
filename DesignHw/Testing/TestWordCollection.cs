using System;
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
            var kek = new Word("KEK") { Weight = 20 };
            var lol = new Word("LOL") { Weight = 10 };
            var wc = new WordsCollection<Word>(new[] { lol, kek });
            var coll = wc.ToArray();
            Assert.AreEqual(2, coll.Length);
            Assert.AreSame(kek, coll.First());
            Assert.AreSame(lol, coll.Last());
            Assert.AreEqual(1.00, kek.PercantageWeight);
            Assert.AreEqual(0.50, lol.PercantageWeight);
        }
    }
}
