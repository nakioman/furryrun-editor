using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Caliburn.Micro;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.ViewModels
{
    public class StageViewModel : Conductor<IScreen>.Collection.AllActive
    {
        private readonly Stage _stage;

        public int Width
        {
            get { return _stage.Width; }
        }

        public int Height { get { return _stage.Height; } }

        public Color TopGradientColor
        {
            get { return _stage.TopGradientColor; }
            set
            {
                _stage.TopGradientColor = value;
                NotifyOfPropertyChange(() => TopGradientColor);
            }
        }

        public Color BottomGradientColor
        {
            get { return _stage.BottomGradientColor; }
            set
            {
                _stage.BottomGradientColor = value;
                NotifyOfPropertyChange(() => BottomGradientColor);
            }
        }

        public StageViewModel()
        {
            _stage = new Stage();
            TopGradientColor = Color.FromRgb(100, 100, 100);
            BottomGradientColor = Color.FromRgb(255, 255, 100);
        }

        public StageViewModel(Stage stage)
        {
            _stage = stage;
            foreach (var layer in stage.Layers.Values)
            {
                var viewModel = new LayerViewModel(layer);
                viewModel.PropertyChanged += viewModel_PropertyChanged;
                ActivateItem(viewModel);
            }
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelectedItem")
            {
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public IObservableCollection<IScreen> Layers
        {
            get { return Items; }
        }

        public IScreen SelectedItem
        {
            get
            {
                var items = Items.Cast<LayerViewModel>().Where(x => x.IsSelectedItem);
                if (items.Count() != 1)
                {
                    if (items.Count() > 1)
                    {
                        foreach (var layerItem in items
                            .SelectMany(layerViewModel => layerViewModel.Items.Cast<LayerItemViewModel>()
                                .Where(x => x.IsSelectedItem)))
                        {
                            layerItem.IsSelectedItem = false;
                        }
                    }
                    return new LayerViewModel();
                }
                return items.First();
            }
        }

        public Stage Stage { get { return _stage; } }
    }
}