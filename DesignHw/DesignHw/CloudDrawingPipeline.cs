using System;
using System.Collections.Generic;
using DesignHw.Adapters;
using DesignHw.Rendering;
using DesignHw.Text;

namespace DesignHw
{
    public static class CloudDrawingPipeline
    {
        public static void DrawCloud(WordsCollectionBuilder wcbuilder, WordNormalizator normalizator, IEnumerable<string> words, CloudBuilder cloudBuilder, WordRenderer renderer, RenderTarget target)
        {
            if (wcbuilder == null)
                throw new ArgumentNullException(nameof(wcbuilder));
            if (normalizator == null)
                throw new ArgumentNullException(nameof(normalizator));

            var wordsSorted = wcbuilder(normalizator, words);
            DrawCloud(wordsSorted, cloudBuilder, renderer, target);
            
        }
        public static void DrawCloud(WordsCollection words, CloudBuilder cloudBuilder, WordRenderer renderer, RenderTarget target)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));
            if (cloudBuilder == null)
                throw new ArgumentNullException(nameof(cloudBuilder));
            if (renderer == null)
                throw new ArgumentNullException(nameof(renderer));

            target.Render(g =>
                renderer.Render(cloudBuilder(words, renderer, g), g)
                );
        }
    }

    
}
