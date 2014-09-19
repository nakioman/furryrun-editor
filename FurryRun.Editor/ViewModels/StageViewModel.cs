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
                ActivateItem(viewModel);
            }
        }

        public IObservableCollection<LayerViewModel> Layers
        {
            get { return (IObservableCollection<LayerViewModel>) Items.Cast<LayerViewModel>(); }
        }
    }
}