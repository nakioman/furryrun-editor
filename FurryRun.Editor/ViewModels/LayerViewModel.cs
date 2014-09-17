using System.Collections.Generic;
using Caliburn.Micro;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.ViewModels
{
    public class LayerViewModel : Conductor<IScreen>.Collection.AllActive
    {
        private readonly Layer _layer;

        public LayerViewModel(Layer layer)
        {
            _layer = layer;
            foreach (var layerItem in layer.Items.Values)
            {
                var viewModel = new LayerItemViewModel(layerItem);
                ActivateItem(viewModel);
            }
        }

        public string Name
        {
            get { return _layer.Name; }
            set
            {
                _layer.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public int ZIndex
        {
            get { return _layer.ZIndex; }
            set
            {
                _layer.ZIndex = value;
                NotifyOfPropertyChange(() => ZIndex);
            }
        }

        public int Height
        {
            get { return _layer.Height; }
            set
            {
                _layer.Height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        public int Width
        {
            get { return _layer.Width; }
            set
            {
                _layer.Height = value;
                NotifyOfPropertyChange(() => Width);
            }
        }
    }
}