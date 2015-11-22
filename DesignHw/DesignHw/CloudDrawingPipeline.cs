using System;
using System.Collections.Generic;
using DesignHw.Graphics;
using DesignHw.Text;

namespace DesignHw
{
    public class CloudDrawingPipeline<TWord>
        where TWord : Word
    {
        public WordsCollectionBuilder<TWord> WordsCollectionBuilder { get; }
        public CloudBuilder<TWord> CloudBuilder { get; }
        public WordRenderer<TWord> Renderer { get; }

        public CloudDrawingPipeline(WordsCollectionBuilder<TWord> wordsCollectionBuilder, CloudBuilder<TWord> cloudBuilder, WordRenderer<TWord> renderer)
        {
            if (wordsCollectionBuilder == null)
                throw new ArgumentNullException(nameof(wordsCollectionBuilder));
            if (cloudBuilder == null)
                throw new ArgumentNullException(nameof(cloudBuilder));
            if (renderer == null)
                throw new ArgumentNullException(nameof(renderer));

            WordsCollectionBuilder = wordsCollectionBuilder;
            CloudBuilder = cloudBuilder;
            Renderer = renderer;
        }

        public void DrawCloud(string text, System.Drawing.Graphics target)
        {
            var words = text.Split(new [] {" ", "\n", "\r", "\t"}, StringSplitOptions.RemoveEmptyEntries);
            DrawCloud(words, target);
        }
        public void DrawCloud(IEnumerable<string> words, System.Drawing.Graphics target)
        {
            foreach (var word in words)
                WordsCollectionBuilder.Register(word);
            
            var wordsSorted = WordsCollectionBuilder.Build();
            var cloud = CloudBuilder.Build(wordsSorted, Renderer, target);
            cloud.Render(target, Renderer);
        }
    }
}
