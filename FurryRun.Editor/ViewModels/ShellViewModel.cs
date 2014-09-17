using System;
using System.Windows;
using Caliburn.Micro;
using FurryRun.Editor.Services;
using Microsoft.Win32;

namespace FurryRun.Editor.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IFileManipulationService _fileManipulationService;

        public ShellViewModel(IFileManipulationService fileManipulationService)
        {
            _fileManipulationService = fileManipulationService;
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

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}