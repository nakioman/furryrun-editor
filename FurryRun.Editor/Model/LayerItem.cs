﻿namespace FurryRun.Editor.Model
{
    public class LayerItem
    {
        public int ZIndex { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool Flip { get; set; }
        public int Rotate { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public string SpriteClass { get; set; }
        public bool Visible { get; set; }
    }
}