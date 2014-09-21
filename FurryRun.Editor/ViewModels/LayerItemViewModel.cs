using System;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using FurryRun.Editor.Model;
using FurryRun.Editor.Services;

namespace FurryRun.Editor.ViewModels
{
    public class LayerItemViewModel : Screen
    {
        private readonly LayerItem _layerItem;
        private readonly string _assetsFolder;

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
            get
            {
                return _layerItem.Height;
            }
            set
            {
                _layerItem.Height = value;
                NotifyOfPropertyChange(() => Height);
                NotifyOfPropertyChange(() => Margin);
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

        public int Rotate
        {
            get { return _layerItem.Rotate; }
            set
            {
                _layerItem.Rotate = value;
                NotifyOfPropertyChange(() => Rotate);
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
                return Path.Combine(_assetsFolder, SpriteClass);
            }
        }

        public Thickness Margin
        {
            get
            {
                return new Thickness(X - (Width / 2), Y - Height, 0, 0);
            }
        }

        public int ScaleX
        {
            get { return _layerItem.Flip ? -1 : 1; }
        }

        public bool Flip
        {
            get { return _layerItem.Flip; }
            set
            {
                _layerItem.Flip = value;
                NotifyOfPropertyChange(() => Flip);
                NotifyOfPropertyChange(() => ScaleX);
                NotifyOfPropertyChange(() => TranslateX);
            }
        }

        public int TranslateX
        {
            get { return Flip ? -Width : 0; }
        }

        public LayerItemViewModel(LayerItem layerItem)
        {
            _layerItem = layerItem;
            var options = FileManipulationService.LoadOptions();
            _assetsFolder = options.AssetsFolder;
        }

        public Visibility Visibility
        {
            get { return _layerItem.Visible ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool Visible
        {
            get { return _layerItem.Visible; }
            set
            {
                _layerItem.Visible = value;
                NotifyOfPropertyChange(() => Visible);
                NotifyOfPropertyChange(() => Visibility);
            }
        }

        public string Name
        {
            get { return _layerItem.Name; }
            set
            {
                _layerItem.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private bool _isSelectedItem;

        public bool IsSelectedItem
        {
            get { return _isSelectedItem; }
            set
            {
                _isSelectedItem = value;
                NotifyOfPropertyChange(() => IsSelectedItem);
                NotifyOfPropertyChange(() => BorderThickness);
            }
        }

        public int BorderThickness
        {
            get { return IsSelectedItem ? 5 : 0; }
        }
    }
}