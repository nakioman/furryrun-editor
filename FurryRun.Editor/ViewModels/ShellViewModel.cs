using System;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;

namespace FurryRun.Editor.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
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
                var filename = dlg.FileName;
                MessageBox.Show(filename);
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}