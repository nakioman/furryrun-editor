using System.Windows;
using System.Windows.Input;

namespace FurryRun.Editor.Views.Layer
{
    /// <summary>
    /// Interaction logic for LayersItemWindow.xaml
    /// </summary>
    public partial class LayersItemWindow : Window
    {
        public LayersItemWindow()
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
