using System.Collections.Generic;

namespace FurryRun.Editor.Model
{
    public class Layer
    {
        public int Height { get; set; }
        public int ZIndex { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public IList<Filter> Filters { get; set; } 

        //TODO Add Signposts
        //TODO add player to middleground
        //TODO Pathfinding

        public IList<LayerItem> Items { get; set; } 
    }
}