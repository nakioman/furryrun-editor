using System.Collections.Generic;
using System.Windows.Media;

namespace FurryRun.Editor.Model
{
    public class Stage
    {
        public string Label { get; set; }
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color TopGradientColor { get; set; }
        public Color BottomGradientColor { get; set; }
        public SortedList<int, Layer> Layers { get; set; }
    }
}