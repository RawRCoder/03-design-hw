using System;

namespace DesignHw.Text
{
    public class Word : IComparable<Word>
    {
        public Word(string text)
        {
            Text = text;
        }

        public string Text { get; }
        public decimal Weight { get; set; }
        public double PercantageWeight { get; set; }
        public int CompareTo(Word other) 
            => Weight.CompareTo(other.Weight);

        public override string ToString()
            => $"{Text} ({Weight})";
    }
}