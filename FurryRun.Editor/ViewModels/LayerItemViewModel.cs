using System;
using System.Windows;
using Caliburn.Micro;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.ViewModels
{
    public class LayerItemViewModel : Screen
    {
        private readonly LayerItem _layerItem;

        public int X
        {
            get { return _layerItem.X; }
            set
            {
                _layerItem.X = value;
                NotifyOfPropertyChange(() => X);
                NotifyOfPropertyChange(() => Margin);
            }
        }

        public int Y
        {
            get { return _layerItem.Y; }
            set
            {
                _layerItem.Y = value;
                NotifyOfPropertyChange(() => Y);
                NotifyOfPropertyChange(() => Margin);
            }
        }

        public int Height
        {
            get { return _layerItem.Height; }
            set
            {
                _layerItem.Height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        public int Width
        {
            get { return _layerItem.Width; }
            set
            {
                _layerItem.Width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        public string SpriteClass
        {
            get { return _layerItem.SpriteClass; }
            set
            {
                _layerItem.SpriteClass = value;
                NotifyOfPropertyChange(() => SpriteClass);
                NotifyOfPropertyChange(() => SpriteFullPath);
            }
        }

        public string SpriteFullPath
        {
            get
            {
                return String.Format("D:\\Projects\\CAT422-glitch-location-viewer\\img\\scenery\\{0}.png", SpriteClass);
            }
        }

        public Thickness Margin
        {
            get { return new Thickness(X, Y, 0, 0); }
        }

        public LayerItemViewModel(LayerItem layerItem)
        {
            _layerItem = layerItem;
        }
    }
}