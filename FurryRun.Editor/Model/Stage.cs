using System.Collections.Generic;
using System.Windows.Media;

namespace FurryRun.Editor.Model
{
    public class Stage
    {
        public string Label { get; set; }
        public string Id { get; set; }
        public int Right { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Width { get { return Right - Left; } }
        public int Height { get { return Bottom - Top; } }
        public Color TopGradientColor { get; set; }
        public Color BottomGradientColor { get; set; }
        public SortedList<int, Layer> Layers { get; set; }
    }
}