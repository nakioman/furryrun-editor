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
            get { return Rotate != 0 ? _layerItem.Height * 2 : _layerItem.Height; }
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
                NotifyOfPropertyChange(() => CanvasWidth);
            }
        }

        public int Rotate
        {
            get { return _layerItem.Rotate; }
            set
            {
                _layerItem.Rotate = value;
                NotifyOfPropertyChange(() => Rotate);
                NotifyOfPropertyChange(() => CanvasWidth);
                NotifyOfPropertyChange(() => CanvasHeight);
                NotifyOfPropertyChange(() => RotateCanvas);
                NotifyOfPropertyChange(() => TranslateX);
                NotifyOfPropertyChange(() => TranslateY);
                NotifyOfPropertyChange(() => ImageMargin);
                NotifyOfPropertyChange(() => Height);
            }
        }

        public int CanvasWidth
        {
            get
            {
                if (Rotate != 0)
                {
                    return (int)Math.Sqrt(Math.Pow(Height * 2, 2) + Math.Pow(Height, 2));
                }
                return _layerItem.Width;
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
            get
            {
                var x = 0;
                var y = 0;
                if (_layerItem.Layer.Name == "middleground")
                {
                    x = X - (CanvasWidth / 2) + (_layerItem.Layer.Width / 2);
                    y = Y - (CanvasHeight / 2) + (_layerItem.Layer.Height / 2);
                    return new Thickness(x, y, 0, 0);
                }
                x = X - (CanvasWidth / 2);
                y = Y - (CanvasHeight / 2);
                return new Thickness(x, y, 0, 0);
            }
        }

        public int CanvasHeight
        {
            get
            {
                if (Rotate != 0)
                {
                    return (int)Math.Sqrt(Math.Pow(Height * 2, 2) + Math.Pow(Height, 2));
                }
                return Height * 2;
            }
        }

        public Thickness ImageMargin
        {
            get
            {
                if (Rotate != 0)
                {
                    return new Thickness(-(Width / 2), -Height, 0, 0);
                }
                return _layerItem.Flip
                    ? new Thickness(-_layerItem.Width, 0, 0, 0)
                    : new Thickness(0, 0, 0, 0);
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

        public int ScaleX
        {
            get { return _layerItem.Flip ? -1 : 1; }
        }

        public int TranslateX
        {
            get { return Rotate != 0 ? CanvasWidth / 2 : 0; }
        }

        public int TranslateY
        {
            get { return Rotate != 0 ? CanvasHeight / 2 : 0; }
        }

        public double RotateCanvas
        {
            get { return (Math.PI / 180 * Rotate); }
        }

        public bool Flip
        {
            get { return _layerItem.Flip; }
            set
            {
                _layerItem.Flip = value;
                NotifyOfPropertyChange(() => Flip);
                NotifyOfPropertyChange(() => ImageMargin);
                NotifyOfPropertyChange(() => ScaleX);
            }
        }
    }
}