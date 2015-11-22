namespace DesignHw
{
    public class Word
    {
        public Word(string text)
        {
            Text = text;
        }

        public string Text { get; }
        public decimal Weight { get; set; }
    }
}