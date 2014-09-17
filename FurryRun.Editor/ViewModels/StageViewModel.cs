using System.Windows.Media;
using Caliburn.Micro;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.ViewModels
{
    public class StageViewModel : Screen
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
        }
    }
}