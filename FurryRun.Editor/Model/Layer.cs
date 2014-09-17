using System.Collections.Generic;

namespace FurryRun.Editor.Model
{
    public class Layer
    {
        public int ZIndex { get; set; }
        public string Name { get; set; }
        public IList<Filter> Filters { get; set; } 

        //TODO Add Signposts
        //TODO add player to middleground
        //TODO Pathfinding

        public SortedList<int, LayerItem> Items { get; set; } 
    }
}