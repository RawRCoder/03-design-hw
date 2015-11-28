using System;
using System.Collections.Generic;
using System.Drawing;
using DesignHw.Adapters;
using DesignHw.Rendering;
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
        
        public void DrawCloud(IWordsExtractor extractor, IRenderTarget target)
        {
            DrawCloud(extractor.Words, target);
        }
        public void DrawCloud(string text, IRenderTarget target)
        {
            var words = text.Split(new [] {" ", "\n", "\r", "\t"}, StringSplitOptions.RemoveEmptyEntries);
            DrawCloud(words, target);
        }
        public void DrawCloud(IEnumerable<string> words, IRenderTarget target)
        {
            foreach (var word in words)
                WordsCollectionBuilder.TryRegister(word);
            
            var wordsSorted = WordsCollectionBuilder.Build();
            var g = target.GetGraphics();
            var cloud = CloudBuilder.Build(wordsSorted, Renderer, g);
            cloud.Render(g, Renderer);
            target.Close(g);
        }
    }
}
