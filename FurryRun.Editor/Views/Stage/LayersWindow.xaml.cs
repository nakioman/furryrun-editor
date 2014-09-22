using System.Windows;
using System.Windows.Input;

namespace FurryRun.Editor.Views.Stage
{
    /// <summary>
    /// Interaction logic for LayersWindow.xaml
    /// </summary>
    public partial class LayersWindow : Window
    {
        public LayersWindow()
        {
            InitializeComponent();
            Closing += Window_Closing;
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                (sender, args) => Close()));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }
    }
}
