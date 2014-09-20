using System;
using System.Dynamic;
using System.Windows;
using Caliburn.Micro;
using FurryRun.Editor.Infrastructure;
using FurryRun.Editor.Model;
using FurryRun.Editor.Services;
using Microsoft.Win32;

namespace FurryRun.Editor.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IFileManipulationService _fileManipulationService;
        private readonly ICustomWindowManager _windowManager;
        private StageViewModel _stageViewModel;
        private readonly Options _options;
        private Window _layersWindow;


        public ShellViewModel(IFileManipulationService fileManipulationService, ICustomWindowManager windowManager)
        {
            _options = FileManipulationService.LoadOptions();
            _fileManipulationService = fileManipulationService;
            _windowManager = windowManager;
            base.DisplayName = Application.Current.Resources["AppTitle"].ToString();
        }

        public void LoadLocation()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*"
            };
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                var stage = _fileManipulationService.ImportGlitchLocationFile(dlg.FileName);
                _stageViewModel = new StageViewModel(stage);
                ActivateItem(_stageViewModel);
                Layers();
            }
        }

        public void Options()
        {

            dynamic settings = new ExpandoObject();
            var optionsViewModel = new OptionsViewModel(_options);
            settings.WindowStyle = WindowStyle.ToolWindow;
            _windowManager.ShowDialog(optionsViewModel, null, settings);
            _fileManipulationService.SaveOptions(_options);
        }

        public void Layers()
        {
            if (LayersVisible && _stageViewModel != null)
            {
                if (_layersWindow != null)
                {
                    _layersWindow.Show();
                }
                else
                {
                    dynamic settings = new ExpandoObject();
                    settings.WindowStyle = WindowStyle.ToolWindow;
                    settings.ShowInTaskbar = false;
                    settings.Title = Application.Current.Resources["Layers"].ToString();
                    _layersWindow = _windowManager.GetWindow(_stageViewModel, "LayersWindow", settings);
                    _layersWindow.Closing += _layersWindow_Closing;
                    _layersWindow.Show();
                }
            }
            else if (_layersWindow != null)
            {
                _layersWindow.Hide();
            }
        }

        void _layersWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LayersVisible = !LayersVisible;
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public bool LayersVisible
        {
            get { return _options.LayersWindowVisible; }
            set
            {
                _options.LayersWindowVisible = value;
                _fileManipulationService.SaveOptions(_options);
                NotifyOfPropertyChange(() => LayersVisible);
                Layers();
            }
        }

        public override void CanClose(Action<bool> callback)
        {
            Exit();
            callback(true);
        }
    }
}