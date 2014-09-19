using System.Windows.Forms;
using FurryRun.Editor.Model;
using Application = System.Windows.Application;
using Screen = Caliburn.Micro.Screen;

namespace FurryRun.Editor.ViewModels
{
    public class OptionsViewModel : Screen
    {
        private readonly Options _options;

        public OptionsViewModel(Options options)
        {
            _options = options;
            base.DisplayName = Application.Current.Resources["Options"].ToString();
        }

        public string AssetsFolder
        {
            get { return _options.AssetsFolder; }
            set
            {
                _options.AssetsFolder = value;
                NotifyOfPropertyChange(() => AssetsFolder);
            }
        }

        public void SelectFolder()
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                AssetsFolder = dialog.SelectedPath;
            }
        }
    }
}