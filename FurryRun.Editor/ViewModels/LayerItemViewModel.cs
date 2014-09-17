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
                NotifyOfPropertyChange(() => Margin);
                NotifyOfPropertyChange(() => CanvasHeight);
            }
        }

        public int Width
        {
            get { return _layerItem.Width; }
            set
            {
                _layerItem.Width = value;
                NotifyOfPropertyChange(() => Width);
                NotifyOfPropertyChange(() => Margin);
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
                return String.Format("C:\\Projects\\glitch_assets\\CAT422-glitch-location-viewer-master\\img\\scenery\\{0}.png", SpriteClass);
            }
        }

        public Thickness Margin
        {
            get
            {
                var x = X - (Width/2);
                var y = Y - (CanvasHeight/2);
                return new Thickness(x, y, 0, 0);
            }
        }

        public int CanvasHeight
        {
            get { return Height*2; }
        }

        public Thickness ImageMargin
        {
            get
            {
                //TODO handle flip!
                return new Thickness(0,0,0,0);
            }
        }

        public int ZIndex
        {
            get { return _layerItem.ZIndex; }
            set
            {
                _layerItem.ZIndex = value;
                NotifyOfPropertyChange(() => ZIndex);
            }
        }

        public LayerItemViewModel(LayerItem layerItem)
        {
            _layerItem = layerItem;
        }
    }
}