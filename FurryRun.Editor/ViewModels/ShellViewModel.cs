using System;
using System.ComponentModel;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
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
        private Window _layerItemsWindow;


        public ShellViewModel(IFileManipulationService fileManipulationService, ICustomWindowManager windowManager)
        {
            _sliderValue = 1;
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
                _stageViewModel.PropertyChanged += _stageViewModel_PropertyChanged;
                ActivateItem(_stageViewModel);
                AutoAdjustZoom();
                Layers();
                LayerItems();
            }
        }

        void _stageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                if (_layerItemsWindow != null)
                {
                    _layerItemsWindow.DataContext = _stageViewModel.SelectedItem;
                    _layerItemsWindow.Title = Application.Current.Resources["Images"].ToString();
                }
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
                    settings.Owner = this.GetView();
                    settings.TopMost = true;
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

        public void LayerItems()
        {
            if (LayerItemsVisible && _stageViewModel != null)
            {
                if (_layerItemsWindow != null)
                {
                    _layerItemsWindow.Show();
                }
                else
                {
                    dynamic settings = new ExpandoObject();
                    settings.Owner = this.GetView();
                    settings.TopMost = true;
                    settings.Title = Application.Current.Resources["Images"].ToString();
                    _layerItemsWindow = _windowManager.GetWindow(_stageViewModel.SelectedItem, "LayersItemWindow", settings);
                    _layerItemsWindow.Closing += LayerItemsWindowOnClosing;
                    _layerItemsWindow.Show();
                }
            }
            else if (_layerItemsWindow != null)
            {
                _layerItemsWindow.Hide();
            }
        }

        private void LayerItemsWindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            LayerItemsVisible = !LayerItemsVisible;
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

        public bool LayerItemsVisible
        {
            get { return _options.LayersItemWindowVisible; }
            set
            {
                _options.LayersItemWindowVisible = value;
                _fileManipulationService.SaveOptions(_options);
                NotifyOfPropertyChange(() => LayerItemsVisible);
                LayerItems();
            }
        }

        public override void CanClose(Action<bool> callback)
        {
            Exit();
            callback(true);
        }

        private double _sliderValue;
        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                _sliderValue = value;
                NotifyOfPropertyChange(() => SliderValue);
            }
        }

        public void AutoAdjustZoom()
        {
            if (_stageViewModel != null)
            {
                var window = (Window)GetView();
                var scrollViewer = (ScrollViewer)window.FindName("StageViewer");
                if (_stageViewModel.Height > _stageViewModel.Width)
                {
                    SliderValue = scrollViewer.ViewportHeight / _stageViewModel.Height;
                }
                SliderValue = scrollViewer.ViewportWidth / _stageViewModel.Width;
            }
        }
    }
}