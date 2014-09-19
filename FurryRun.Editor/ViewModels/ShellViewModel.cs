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
                var stageViewModel = new StageViewModel(stage);
                ActivateItem(stageViewModel);
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

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}