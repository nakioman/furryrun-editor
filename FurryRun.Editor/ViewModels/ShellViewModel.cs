using System;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls.Primitives;
using Caliburn.Micro;
using FurryRun.Editor.Services;
using Microsoft.Win32;

namespace FurryRun.Editor.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IFileManipulationService _fileManipulationService;
        private readonly IWindowManager _windowManager;
        private StageViewModel _stageViewModel;

        public ShellViewModel(IFileManipulationService fileManipulationService, IWindowManager windowManager)
        {
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
            var options = FileManipulationService.LoadOptions();
            dynamic settings = new ExpandoObject();
            var optionsViewModel = new OptionsViewModel(options);
            settings.WindowStyle = WindowStyle.ToolWindow; 
            _windowManager.ShowDialog(optionsViewModel, null, settings);
            _fileManipulationService.SaveOptions(options);
        }

        public void Layers()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            _windowManager.ShowWindow(_stageViewModel, "LayersWindow", settings);
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}