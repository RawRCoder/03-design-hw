using System;
using System.Collections.Generic;
using DesignHw.Adapters;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw
{
    public class CloudDrawingPipeline
    {
        public WordsCollectionBuilder WordsCollectionBuilder { get; }
        public CloudBuilder CloudBuilder { get; }
        public WordRenderer Renderer { get; }

        public CloudDrawingPipeline(WordsCollectionBuilder wordsCollectionBuilder, CloudBuilder cloudBuilder, WordRenderer renderer)
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
        public void DrawCloud(IEnumerable<string> words, IRenderTarget target)
        {
            foreach (var word in words)
                WordsCollectionBuilder.TryRegister(word);
            
            var wordsSorted = WordsCollectionBuilder.Build();
            var g = target.GetGraphics();
            var cloud = CloudBuilder.Build(wordsSorted, Renderer, g);
            Renderer.Render(cloud, g);
            target.Close(g);
        }
    }
}
