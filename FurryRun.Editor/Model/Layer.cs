using System.Collections.Generic;
using FurryRun.Editor.Infrastructure;

namespace FurryRun.Editor.Model
{
    public class Layer
    {
        public int ZIndex { get; set; }
        public string Name { get; set; }
        public List<Filter> Filters { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        //TODO Add Signposts
        //TODO add player to middleground
        //TODO Pathfinding

        public SerializableSortedList<int, LayerItem> Items { get; set; }

        public bool Visible { get; set; }
    }
}